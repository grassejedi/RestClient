using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Windows.Forms;

namespace BasicRestClient
{
    class ServerRequest
    {
        //private RestClient client = new RestClient("http://localhost:54719/");
        //private RestClient client = new RestClient("http://192.168.28.128:8080/");
        private RestClient client; //= new RestClient("https://192.168.28.128/");
        public bool AUTHENTICATED { get; private set; }
        private string TOKEN { get; set; }
        public bool ADMIN { get; private set; }
        public bool CONNECTIONSUCCESS { get; private set; }

        public ServerRequest(string ServerURL)
        {
            client = new RestClient(ServerURL);
        }

        // This was temporarily added to handle the cert error that I would get when the server isn't on the actual ip address that the cert says it is on.
        public static void IgnoreBadCertificate()
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
        }


        /// <summary>
        /// authenticate method.
        /// Purpose:  This method handles establising the initial connection with the server and setting the object values.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        public void authenticate(string user, string pass)
        {
            IgnoreBadCertificate();
            JToken value;           

            // Setup Parameters/Headers
            var request = new RestRequest("api/login", Method.GET);
            request.AddParameter("user", user);
            request.AddParameter("pass", pass);
            request.RequestFormat = DataFormat.Json;

            // execute the request
            IRestResponse response = client.Execute(request);

            if (response.Content != "")  // If this is true, we basically didn't get a response back from the server
            {
                JObject content = JObject.Parse(response.Content);  // Convert data we got back into Json Object
                content.TryGetValue("status", out value);
                if (value != null && value.ToString() == "loggedin")
                {
                    ADMIN = false;
                    AUTHENTICATED = true;
                    TOKEN = content["token"].ToString();
                    CONNECTIONSUCCESS = true;

                    if (content["admin"] != null)
                    {
                        ADMIN = true;
                    }
                }
                else
                {
                    AUTHENTICATED = false;
                    CONNECTIONSUCCESS = true;
                }
            }
            else
            {
                CONNECTIONSUCCESS = false;
                MessageBox.Show(response.ErrorMessage, "Connection Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            }
        }


        /// <summary>
        /// disconnect method.
        /// Purpose:  This method handles disconnecting from the server
        /// </summary>
        /// <returns></returns>
        public bool disconnect()
        {
            // Setup Parameters/Headers
            var request = new RestRequest("api/logout", Method.GET);
            request.AddParameter("token", TOKEN);
            request.RequestFormat = DataFormat.Json;

            // execute the request
            IRestResponse response = client.Execute(request);

            JObject content = JObject.Parse(response.Content); 
    
            if(content["status"].ToString() == "loggedout")
            {
                AUTHENTICATED = false;
                return true;
            }

            return false;
        }


        /// <summary>
        /// getProducts method
        /// Purpose:  Retrieve all the products from the server
        /// </summary>
        /// <returns></returns>
        public JArray getProducts()
        {
            // Setup Parameters/Headers
            var request = new RestRequest("api/products", Method.GET);
            request.RequestFormat = DataFormat.Json;

            // execute the request
            IRestResponse response = client.Execute(request);

            if (response.Content == "")
            {
                return new JArray();
            }
            else
            {
                return JArray.Parse(response.Content);
            } 
        }


        /// <summary>
        /// getAccount method
        /// Purpose: To retrieve the current user's account information
        /// </summary>
        /// <returns></returns>
        public Account getAccount()
        {
            var request = new RestRequest("api/account", Method.GET);
            request.AddParameter("token", TOKEN);
            request.AddParameter("userName", login.USER);
            request.RequestFormat = DataFormat.Json;

            // execute the request
            IRestResponse response = client.Execute(request);

            if (response.Content == "")
            {
                return new Account();
            }
            else
            {
                JArray content = JArray.Parse(response.Content);

                Account acct = JsonConvert.DeserializeObject<Account>(content[0].ToString());
                return acct;
            } 
        }


        /// <summary>
        /// getAllAccounts method
        /// Purpose:  This will get all of the Accounts from the Server.  Only admin can do this.
        /// </summary>
        /// <returns></returns>
        public JArray getAllAccounts()
        {
            // Setup Parameters/Headers
            var request = new RestRequest("api/account", Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("token", TOKEN);

            // execute the request
            IRestResponse response = client.Execute(request);

            if (response.Content == "")
            {
                return new JArray();
            }
            else
            {
                return JArray.Parse(response.Content);
            }          
        }


        /// <summary>
        /// updateAccount method
        /// Purpose: This will allow the user to update their own account info
        /// </summary>
        /// <param name="acct"></param>
        /// <returns></returns>
        public bool updateAccount(Account acct)
        {
            // Setup Parameters/Headers
            var request = new RestRequest("api/account?token={token}", Method.PUT);
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("token", TOKEN, ParameterType.UrlSegment);  // this will be placed in the {token} in the url.  It wasn't working when I tried to add it as a parameter because of the way that RestSharp works
            request.AddBody(acct);

            //// execute the request
            IRestResponse response = client.Execute(request);

            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }

            return false;
        }


        /// <summary>
        /// This updateAccount method is for the admin to use to batch update accounts
        /// </summary>
        /// <param name="accts"></param>
        /// <returns></returns>
        public bool updateAccount(List<Account> accts)
        {
            // Setup Parameters/Headers
            var request = new RestRequest("api/account?token={token}&batch=true", Method.PUT);
            request.RequestFormat = DataFormat.Json;          
            request.AddParameter("token", TOKEN, ParameterType.UrlSegment);  // this will be placed in the {token} in the url.  It wasn't working when I tried to add it as a parameter because of the way that RestSharp works
            request.AddBody(accts);

            //// execute the request
            IRestResponse response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }

            return false;
        }


        /// <summary>
        /// deleteAccount method
        /// Purpose:  Handles the deletion of Accounts.  Only Admin can do this
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        internal bool deleteAccount(string userName)
        {
            // Setup Parameters/Headers
            var request = new RestRequest("api/account", Method.DELETE);
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("token", TOKEN); 
            request.AddParameter("userName", userName);

            //// execute the request
            IRestResponse response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }

            return false;
        }


        /// <summary>
        /// createAccount method
        /// Purpose: Allow Admin to send a request to server to create a new Account for a user
        /// </summary>
        /// <param name="acct"></param>
        /// <returns></returns>
        public bool createAccount(Account acct)
        {
            // Setup Parameters/Headers
            var request = new RestRequest("api/account?token={token}", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("token", TOKEN, ParameterType.UrlSegment);  // this will be placed in the {token} in the url.  It wasn't working when I tried to add it as a parameter because of the way that RestSharp works
            request.AddBody(acct);

            //// execute the request
            IRestResponse response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                return true;
            }

            return false;
        }


        /// <summary>
        /// getOrders method
        /// Purpose: Returns orders that were made by the current user
        /// </summary>
        /// <returns></returns>
        public JArray getOrders()
        {
            // Setup Parameters/Headers
            var request = new RestRequest(Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.Resource = "api/order";
            request.AddParameter("token", TOKEN);
            request.AddParameter("userName", login.USER);          

            // execute the request
            IRestResponse response = client.Execute(request);

            if (response.Content == "")
            {
                return new JArray();
            }
            else
            {
                return JArray.Parse(response.Content);
            } 
        }


        /// <summary>
        /// getAllOrders method. 
        /// Purpose: This will be used by an Admin to get all orders to view/edit them
        /// </summary>
        /// <returns></returns>
        public JArray getAllOrders()
        {
            // Setup Parameters/Headers
            var request = new RestRequest(Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.Resource = "api/order";
            request.AddParameter("token", TOKEN);

            // execute the request
            IRestResponse response = client.Execute(request);

            if (response.Content == "")
            {
                return new JArray();
            }
            else
            {
                return JArray.Parse(response.Content);
            } 
        }


        /// <summary>
        /// createOrder method
        /// Pupose: This will allow the user to submit a single order for themselves
        /// </summary>
        /// <param name="comments"></param>
        /// <param name="ordDetails"></param>
        /// <returns></returns>
        public bool createOrder(string comments, List<OrderDetails> ordDetails)
        {
            // Setup Parameters/Headers
            var request = new RestRequest("api/order?token={token}", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("token", TOKEN, ParameterType.UrlSegment);  // this will be placed in the {token} in the url.  It wasn't working when I tried to add it as a parameter because of the way that RestSharp works

            DateTime orderDate = DateTime.Now;
            Order newOrder = new Order(comments);
            newOrder.orderDetails = ordDetails;
            
            request.AddBody(newOrder);

            // execute the request
            IRestResponse response = client.Execute(request);

            if(response.StatusCode ==  System.Net.HttpStatusCode.Created)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// updateOrder method
        /// Purpose: This will allow an Admin to update info on an order
        /// </summary>
        /// <param name="orders"></param>
        /// <returns></returns>
        public bool updateOrder(List<Order> orders)
        {
            // Setup Parameters/Headers
            var request = new RestRequest("api/order?token={token}", Method.PUT);
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("token", TOKEN, ParameterType.UrlSegment);  // this will be placed in the {token} in the url.  It wasn't working when I tried to add it as a parameter because of the way that RestSharp works
            request.AddBody(orders);

            // execute the request
            IRestResponse response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }

            return false;
        }


        /// <summary>
        /// deleteOrder method
        /// Purpose:  This will allow an Admin to delete an order
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public bool deleteOrder(decimal orderNumber)
        {
            // Setup Parameters/Headers
            var request = new RestRequest("api/order", Method.DELETE);
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("token", TOKEN);
            request.AddParameter("orderNumber", orderNumber);

            //// execute the request
            IRestResponse response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }

            return false;
        }


        /// <summary>
        /// updateProducts method
        /// Purpose:  Allow an Admin to update a Product
        /// </summary>
        /// <param name="prods"></param>
        /// <returns></returns>
        public bool updateProducts(List<Product> prods)
        {
            // Setup Parameters/Headers
            var request = new RestRequest("api/products?token={token}", Method.PUT);
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("token", TOKEN, ParameterType.UrlSegment);  // this will be placed in the {token} in the url.  It wasn't working when I tried to add it as a parameter because of the way that RestSharp works
            request.AddBody(prods);

            //// execute the request
            IRestResponse response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }

            return false;
        }


        /// <summary>
        /// createProduct method
        /// Purpose:  This will allow an Admin to create a Product
        /// </summary>
        /// <param name="prods"></param>
        /// <returns></returns>
        public bool createProduct(Product prods)
        {
            // Setup Parameters/Headers
            var request = new RestRequest("api/products?token={token}", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("token", TOKEN, ParameterType.UrlSegment);  // this will be placed in the {token} in the url.  It wasn't working when I tried to add it as a parameter because of the way that RestSharp works
            request.AddBody(prods);

            // execute the request
            IRestResponse response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                return true;
            }

            return false;
        }


        /// <summary>
        /// deleteProducts method
        /// Purpose:  This will allow an Admin to delete a Product
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public bool deleteProducts(string productId)
        {
            // Setup Parameters/Headers
            var request = new RestRequest("api/products", Method.DELETE);
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("token", TOKEN);
            request.AddParameter("productId", productId);

            //// execute the request
            IRestResponse response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }

            return false;
        }


        /// <summary>
        /// createOrderAdmin method
        /// This this the method that Admins can use to create orders for any user.
        /// </summary>
        /// <param name="newOrder"></param>
        /// <returns></returns>
        public bool createOrderAdmin(Order newOrder)
        {
            // Setup Parameters/Headers
            var request = new RestRequest("api/order?token={token}", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("token", TOKEN, ParameterType.UrlSegment);  // this will be placed in the {token} in the url.  It wasn't working when I tried to add it as a parameter because of the way that RestSharp works
            request.AddBody(newOrder);

            // execute the request
            IRestResponse response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
