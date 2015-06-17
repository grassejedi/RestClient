using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace BasicRestClient
{
    public partial class BasicRestClient : Form
    {
        ServerRequest request; // = new ServerRequest();
        Account acctInfo;  // Account object that is used with the event handler for the btnAcct_Click and btnUpdateInfo_Click
        List<Order> orderHistoryList;


        /// <summary>
        /// Default Constructor
        /// </summary>
        public BasicRestClient()
        {
            InitializeComponent();

            dataGridView1.Visible = false;

            // This is just to hid the tabs on the tabcontrol
            tabControl1.Appearance = TabAppearance.FlatButtons; tabControl1.ItemSize = new Size(0, 1); tabControl1.SizeMode = TabSizeMode.Fixed;
            btnCheckout.Visible = false;
            txtBxComments.Visible = false;
            lblComments.Visible = false;
        }


        /// <summary>
        /// Event Handler for when the Sign In/Sign Out option in the Menu is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void signInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.signInToolStripMenuItem.Text == "Sign in")
            {
                login loginForm = new login();  // using separate form for login
                loginForm.ShowDialog();

                if (login.USER != null && loginForm.PASS != null && loginForm.SERVER != null)
                {
                    request = new ServerRequest(loginForm.SERVER);

                    request.authenticate(login.USER, loginForm.PASS);  // Make sure that we actually got connected

                    if (request.AUTHENTICATED)
                    {
                        MessageBox.Show("Login Successful", "Logged in", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                      
                        this.signInToolStripMenuItem.Text = "Sign out";
                        tabControl1.SelectedIndex = -1;
                        tabControl1.Visible = true;                      
                        this.btnProducts.Enabled = true;
                        this.btnOrders.Enabled = true;
                        this.btnAcct.Enabled = true;
                        this.Refresh();

                        if(request.ADMIN)
                        {
                            this.btnViewEditProducts.Visible = true;
                            this.btnViewEditProducts.Enabled = true;
                            this.btnAllAccounts.Visible = true;
                            this.btnAllAccounts.Enabled = true;
                            this.btnAllOrders.Visible = true;
                            this.btnAllOrders.Enabled = true;
                            this.btnAddProduct.Enabled = true;
                            this.btnAddProduct.Visible = true;
                            this.btnAddOrder.Enabled = true;
                            this.btnAddOrder.Visible = true;
                            this.btnAddAccount.Enabled = true;
                            this.btnAddAccount.Visible = true;                         
                        }
                    }
                    else if (!request.CONNECTIONSUCCESS)
                    {
                      // Don't do anything here.  Thr ServerRequest Object will Display an error message with specific info from the request.
                    }
                    else
                    {
                        MessageBox.Show("Username or Password incorrect", "Unauthenticated!", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                    }
                }              
            }
            else 
            {
                bool loggedOut = request.disconnect();

                if (loggedOut)
                {
                    MessageBox.Show("You are now signed out", "Signed out", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);

                    this.signInToolStripMenuItem.Text = "Sign in";
                    this.Refresh();

                    this.btnProducts.Enabled = false;
                    this.btnOrders.Enabled = false;
                    this.btnAcct.Enabled = false;
                    this.btnAllAccounts.Visible = false;
                    this.btnAllAccounts.Enabled = false;
                    this.btnAllOrders.Visible = false;
                    this.btnAllOrders.Enabled = false;
                    this.btnViewEditProducts.Visible = false;
                    this.btnViewEditProducts.Enabled = false;
                    this.btnAddProduct.Enabled = false;
                    this.btnAddProduct.Visible = false;
                    this.btnAddOrder.Enabled = false;
                    this.btnAddOrder.Visible = false;
                    this.btnAddAccount.Enabled = false;
                    this.btnAddAccount.Visible = false;   
                    this.tabControl1.Visible = false;
                    this.dataGridView1.DataSource = null;
                    this.dataGridViewAllAcounts.DataSource = null;
                    this.dataGridViewAllOrders.DataSource = null;
                    this.dataGridViewEditProducts.DataSource = null;
                    this.dataGridViewOrderDetails.DataSource = null;
                    this.dataGridViewOrderHistory.DataSource = null;
                    this.dataGridViewProducts.DataSource = null;
                    this.dataGridViewProducts2.DataSource = null;
                    this.txtBxAddCity.Text = null;
                    this.txtBxAddCountry.Text = null;
                    this.txtBxAddEmail.Text = null;
                    this.txtBxAddFName.Text = null;
                    this.txtBxAddLName.Text = null;
                    this.txtBxAddPass.Text = null;
                    this.txtBxAddPhone.Text = null;
                    this.txtBxAddPostalCode.Text = null;
                    this.txtBxAddress.Text = null;
                    this.txtBxAddState.Text = null;
                    this.txtBxAddStreet.Text = null;
                    this.txtBxAddUserName.Text = null;
                    this.txtBxCreateDesc.Text = null;
                    this.txtBxCreateMSRP.Text = null;
                    this.txtBxCreateOrderComments.Text = null;
                    this.txtBxCreatePrice.Text = null;
                    this.txtBxCreateProdName.Text = null;
                    this.txtBxCreateQuantity.Text = null;
                    this.txtBxCreateVendor.Text = null;
                }              
            }
        }


        /// <summary>
        /// Event Handler for when the Products button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnProducts_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
            dataGridView1.Visible = true; 
            btnCheckout.Visible = true;
            txtBxComments.Visible = true;
            var prods = request.getProducts();
            List<Product> prodList = new List<Product>();

            if (prods != null)
            {
                foreach (var p in prods)
                {
                    Product productToAdd = JsonConvert.DeserializeObject<Product>(p.ToString());

                    prodList.Add(productToAdd);
                }

                dataGridView1.DataSource = prodList;
            }
            else
            {
                MessageBox.Show("No Products were recieved from the Server.", "No Products", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
        }


        /// <summary>
        /// Event Handler for when the Account button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAcct_Click(object sender, EventArgs e)
        {
            //var acctInfo = request.getAccount()["user"];
            acctInfo = request.getAccount();

            if (acctInfo != null)
            {
                tabControl1.SelectedIndex = 2;
                dataGridView1.Visible = false;

                txtBxFName.Text = acctInfo.userFirstName;
                txtBxLName.Text = acctInfo.userLastName;
                txtBxAddress.Text = acctInfo.addressLine1 + " " + acctInfo.addressLine2;
                txtBxCity.Text = acctInfo.city;
                txtBxState.Text = acctInfo.state;
                txtBxPostalCode.Text = acctInfo.postalCode;
                txtBxCountry.Text = acctInfo.country;
                txtBxPhone.Text = acctInfo.phone.ToString();
                txtBxEmail.Text = acctInfo.email;
                txtBxUser.Text = acctInfo.username;
                btnUpdateInfo.Enabled = true;
            }
            else
            {
                btnUpdateInfo.Enabled = false;
                MessageBox.Show("No Account was recieved from the Server.", "No Account", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
        }


        /// <summary>
        /// Event Handler for when the Update Info button on the Account page is pressed.  Uses acctInfo object will be updated by btnAcct_click method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdateInfo_Click(object sender, EventArgs e)
        {
            if (acctInfo != null)
            {
                Account acct = acctInfo;
                acct.userFirstName = txtBxFName.Text;
                acct.userLastName = txtBxLName.Text;
                acct.addressLine1 = txtBxAddress.Text;
                acct.city = txtBxCity.Text;
                acct.state = txtBxState.Text;
                acct.postalCode = txtBxPostalCode.Text;
                acct.country = txtBxCountry.Text;
                acct.phone = txtBxPhone.Text;
                acct.email = txtBxEmail.Text;
                acct.username = txtBxUser.Text;

                if (txtBxPass.Text != "")
                {
                    acct.pass = txtBxPass.Text;
                }

               bool updated = request.updateAccount(acct);

                if(updated)
                {
                    MessageBox.Show("Information Updated.", "Updated", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("An Error occurred and your information was not updated.", "Error!", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                }
            }
        }


        /// <summary>
        /// Event Handler for when the Order button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOrders_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
            Nullable<DateTime> shipDate;
            dataGridViewOrderHistory.Visible = true;
            //dataGridViewOrderHistory.Columns.Add("orderDetails", "Order Details");
            //DataGridView dataGridViewOrderDetails = new DataGridView();

            var orders = request.getOrders();
            orderHistoryList = new List<Order>();

            if (orders != null)
            {
                foreach (var o in orders)
                {
                    string ordDate = o["orderDate"].ToString();
                    string reqdDate = o["requiredDate"].ToString();
                    string shippedDate = o["shippedDate"].ToString();

                    if (shippedDate == "")
                    {
                        shipDate = null;
                    }
                    else
                    {
                        shipDate = Convert.ToDateTime(shippedDate);
                    }

                    Order ord = JsonConvert.DeserializeObject<Order>(o.ToString());                
                    orderHistoryList.Add(ord);
                }
                // dataGridViewOrderDetails.Visible = true;
                dataGridViewOrderHistory.DataSource = orderHistoryList;
            }
            else
            {
                MessageBox.Show("No Order History was recieved from the Server.", "No Order History", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }        
        }


        /// <summary>
        /// Event Handler for when the View/Edit Accounts button is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAllAccounts_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 4;
            
            dataGridViewAllAcounts.Visible = true;      

            var accounts = request.getAllAccounts();

            if (accounts != null)
            {
                List<Account> acctsList = new List<Account>();

                foreach (var a in accounts)
                {
                    Account act = JsonConvert.DeserializeObject<Account>(a.ToString());
                    acctsList.Add(act);
                }

                dataGridViewAllAcounts.DataSource = acctsList;
            }
            else
            {
                MessageBox.Show("No Account was recieved from the Server.", "No Account", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }

        }


        /// <summary>
        /// Event Handler for when the Update Accounts button is pressed on the View/Edit Accounts page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdateAccounts_Click(object sender, EventArgs e)
        {
            List<Account> accts = new List<Account>();

            foreach (DataGridViewRow row in dataGridViewAllAcounts.Rows)
            {
                if (Convert.ToBoolean(row.Cells["updateAccount"].Value))
                {
                    Account acct = (Account)row.DataBoundItem;
                    accts.Add(acct);
                }
            }

           bool updated = request.updateAccount(accts);

           if (updated)
           {
               MessageBox.Show("Account(s) updated successfully.", "Information Updated", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
           }
           else
           {
               MessageBox.Show("An error occured and the information was not updated.", "Error!", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
           }
        }


        /// <summary>
        /// Event Handler for when the Checkout button is pressed on the Products page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCheckout_Click(object sender, EventArgs e)
        {
            txtBxComments.Visible = true;
            lblComments.Visible = true;
            string comments = "";
            List<OrderDetails> ordDetails = new List<OrderDetails>();
            bool quantityPopulated = true;

           foreach(DataGridViewRow row in dataGridView1.Rows)
           {
             if(Convert.ToBoolean(row.Cells["addToCart"].Value))  // Checking to see which product had the Add To Cart box checked
             {
                 if (row.Cells["Quantity"].Value == null)
                 {
                     MessageBox.Show("The Quantity needs to be entered for the products selected.", "Error!", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                     quantityPopulated = false;
                 }
                 else
                 {
                     int quantity = Convert.ToInt32(row.Cells["Quantity"].Value.ToString());
                     string productId = row.Cells["productId"].Value.ToString();

                     OrderDetails od = new OrderDetails();
                     od.productId = productId;
                     od.quantityOrdered = quantity;

                     ordDetails.Add(od);
                 }                                 
             }
           }

           if (quantityPopulated && ordDetails.Count > 0)
           {
               comments = txtBxComments.Text;
               bool orderPlaced = request.createOrder(comments, ordDetails);

               if(orderPlaced)
               {
                   MessageBox.Show("The order was placed successfully", "Order Placed", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
               }
               else
               {
                   MessageBox.Show("An error occured and Order was not placed", "Error!", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
               }
           }
        }


        /// <summary>
        /// Event Handler for when the View/Edit Orders button is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAllOrders_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 3;
            dataGridViewAllOrders.Visible = true;

            var orders = request.getAllOrders();

            if (orders != null)
            {
                List<Order> orderList = new List<Order>();

                foreach (var o in orders)
                {
                    Order ord = JsonConvert.DeserializeObject<Order>(o.ToString());
                    orderList.Add(ord);
                }

                dataGridViewAllOrders.DataSource = orderList;
            }
            else
            {
                MessageBox.Show("No Order was recieved from the Server.", "No Order Recieved", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            } 
        }


        /// <summary>
        /// Event Handler for when Update Selected Order is pressed on the View\Edit Orders
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdateOrders_Click(object sender, EventArgs e)
        {
            List<Order> orders = new List<Order>();

            foreach (DataGridViewRow row in dataGridViewAllOrders.Rows)
            {
                if (Convert.ToBoolean(row.Cells["updateOrder"].Value))
                {
                    Order order = (Order)row.DataBoundItem;
                    orders.Add(order);
                }
            }

            bool updated = request.updateOrder(orders);

            if (updated)
            {
                MessageBox.Show("Order(s) updated successfully.", "Information Updated", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("An error occured and the information was not updated.", "Error!", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            }
        }


        /// <summary>
        /// Event Handler for when the Delete Selected Order button is clicked on the View\Edit Orders page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            int countDeleted = 0;
            int countNotDeleted = 0;
            List<Order> orders = new List<Order>();

            foreach (DataGridViewRow row in dataGridViewAllOrders.Rows)
            {
                if (Convert.ToBoolean(row.Cells["updateOrder"].Value))
                {
                    Order order = (Order)row.DataBoundItem;
                    orders.Add(order);
                }
            }

            foreach (Order o in orders)
            {
                bool updated = request.deleteOrder(o.orderNumber);

                if(updated)
                {
                    ++countDeleted;
                }
                else
                {
                    ++countNotDeleted;
                }
            }
                MessageBox.Show(countDeleted + " selected order(s) deleted successfully.  " + countNotDeleted + " selected order(s) not deleted.", "Order Deleted", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);         
        }


        /// <summary>
        /// Event Handler for when the Delete Selected Account button is pressed on the View\Edit Accounts page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteAccounts_Click(object sender, EventArgs e)
        {
            int countDeleted = 0;
            int countNotDeleted = 0;
            List<Account> accts = new List<Account>();

            foreach (DataGridViewRow row in dataGridViewAllAcounts.Rows)
            {
                if (Convert.ToBoolean(row.Cells["updateAccount"].Value))
                {
                    Account acct = (Account)row.DataBoundItem;
                    accts.Add(acct);
                }
            }

            foreach (Account a in accts)
            {
                bool updated = request.deleteAccount(a.username);

                if(updated)
                {
                    ++countDeleted;
                }
                else
                {
                    ++countNotDeleted;
                }
            }

            MessageBox.Show(countDeleted + " selected accounts(s) deleted successfully.  " + countNotDeleted + " selected accounts(s) not deleted.", "Order Deleted", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);                
        }


        /// <summary>
        /// Event code for when the View/Edit Products is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnViewEditProducts_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 5;

            var prods = request.getProducts();

            if (prods != null)
            {
                List<Product> prodList = new List<Product>();

                foreach (var p in prods)
                {
                    Product productToAdd = JsonConvert.DeserializeObject<Product>(p.ToString());

                    prodList.Add(productToAdd);
                }

                dataGridViewEditProducts.DataSource = prodList;
            }
            else
            {
                MessageBox.Show("No Product was recieved from the Server.", "No Products", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }   
        }


        /// <summary>
        /// Event Handler for when then Update Selected Products button on the View\Edit Products page is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdateProducts_Click(object sender, EventArgs e)
        {
            List<Product> prods = new List<Product>();

            foreach (DataGridViewRow row in dataGridViewEditProducts.Rows)
            {
                if (Convert.ToBoolean(row.Cells["updateProduct"].Value))  // Check to see if any row has the Update Produc box checked
                {
                    Product prod = (Product)row.DataBoundItem;
                    prods.Add(prod);
                }
            }

            bool updated = request.updateProducts(prods);

            if (updated)
            {
                MessageBox.Show("Product(s) updated successfully.", "Products Updated", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("An error occured and the information was not updated.", "Error!", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            }
        }


        /// <summary>
        /// Event handler when the Delete Selected Products button is pressed on the View\Edit Products
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteProducts_Click(object sender, EventArgs e)
        {
            int countDeleted = 0;
            int countNotDeleted = 0;

            List<Product> prods = new List<Product>();

            foreach (DataGridViewRow row in dataGridViewEditProducts.Rows)
            {
                if (Convert.ToBoolean(row.Cells["updateProduct"].Value))
                {
                    Product prod = (Product)row.DataBoundItem;
                    prods.Add(prod);
                }
            }

            foreach (Product p in prods)
            {
                bool updated = request.deleteProducts(p.productId);

                if (updated)
                {
                    ++countDeleted;
                }
                else
                {
                    ++countNotDeleted;
                }
            }

            MessageBox.Show(countDeleted + " selected product(s) deleted successfully.  " + countNotDeleted + " selected products(s) not deleted.", "Order Deleted", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);     
        }


        /// <summary>
        /// Event Handler for when the Create Product button is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddProducts_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 6;
        }


        /// <summary>
        /// Event Handler for when the Create Order button is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddOrders_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 7;
            dataGridViewProducts2.Visible = true;
  
            var prods = request.getProducts();

            if (prods != null)
            {
                List<Product> prodList = new List<Product>();

                foreach (var p in prods)
                {
                    Product productToAdd = JsonConvert.DeserializeObject<Product>(p.ToString());

                    prodList.Add(productToAdd);
                }

                dataGridViewProducts2.DataSource = prodList;
            }
            else
            {
                MessageBox.Show("No Product was recieved from the Server.  Unable to create an Order.", "No Product", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }   
        }


        /// <summary>
        /// Event Handler for when the Create Account button is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 8;
        }



        /// <summary>
        /// Event Handler for when the Create Product button is clicked on the Create Product page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateProduct_Click(object sender, EventArgs e)
        {
            Product prod = new Product("1", txtBxCreateProdName.Text, txtBxCreatePrice.Text, txtBxCreateDesc.Text, txtBxCreateQuantity.Text, txtBxCreateVendor.Text);

            bool updated = request.createProduct(prod);

            if (updated)
            {
                MessageBox.Show("Product added successfully.", "Product Created", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("An error occured and the product was not created.", "Error!", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            }
        }


        /// <summary>
        /// Event Handler for when the Create Account button is pressed on the Create Account page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateAccount_Click(object sender, EventArgs e)
        {
            Account acct = new Account();
            acct.userFirstName = txtBxAddFName.Text;
            acct.userLastName = txtBxAddLName.Text;
            acct.phone = txtBxAddPhone.Text;
            acct.addressLine1 = txtBxAddStreet.Text;
            acct.city = txtBxAddCity.Text;         
            acct.state = txtBxAddState.Text;
            acct.postalCode = txtBxAddPostalCode.Text;
            acct.country = txtBxAddCountry.Text;
            acct.email = txtBxAddEmail.Text;
            acct.username = txtBxAddUserName.Text;
            acct.pass = txtBxAddPass.Text;

            bool updated = request.createAccount(acct);

            if (updated)
            {
                MessageBox.Show("Account added successfully.", "Account Created", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("An error occured and the account was not created.", "Error!", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            }
        }


        /// <summary>
        /// Event Handler for when the Create Order button is pressed on the Create Order page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateOrder_Click(object sender, EventArgs e)
        {
            string comments = "";
            List<OrderDetails> orderDetails = new List<OrderDetails>();
            bool quantityPopulated = true;

            // Populate ordDetails list with OrderDetails objects (Multiple orderDetails created)
            foreach (DataGridViewRow row in dataGridViewProducts2.Rows)
            {
                if (Convert.ToBoolean(row.Cells["addToCartCreateOrder"].Value))
                {
                    if (row.Cells["QuantityCreateOrder"].Value == null)
                    {
                        MessageBox.Show("The Quantity needs to be entered for the products selected.", "Error!", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                        quantityPopulated = false;
                    }
                    else
                    {
                        int quantity = Convert.ToInt32(row.Cells["QuantityCreateOrder"].Value.ToString());
                        string productId = row.Cells["productId"].Value.ToString();

                        OrderDetails od = new OrderDetails();
                        od.productId = productId;
                        od.quantityOrdered = quantity; 

                        orderDetails.Add(od);
                    }
                }
            }

            if (quantityPopulated && orderDetails.Count > 0)
            {
                // Create and Populate Order (Only 1 order created)
                Order newOrder = new Order();               
                newOrder.orderDate = Convert.ToDateTime(dateTimePickerOrderDate.Text);
                newOrder.requiredDate = Convert.ToDateTime(dateTimePickerReqDate.Text);
                newOrder.shippedDate = Convert.ToDateTime(dateTimePickerShipDate.Text);
                newOrder.status = txtBxStatus.Text;
                newOrder.comments = txtBxCreateOrderComments.Text;
                newOrder.userNumber = Convert.ToDecimal(txtBxUserNumber.Text);
                newOrder.orderDetails = orderDetails;  // orderDetails list was created above

                bool orderPlaced = request.createOrderAdmin(newOrder);

                if (orderPlaced)
                {
                    MessageBox.Show("The order was placed successfully.", "Order Placed", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("An error occured and Order was not placed.", "Error!", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                }
            }
        }


        /// <summary>
        /// Event Handler for when a row in the Order list is created after going to the Create Order page.
        /// It will populated another DataGridView with the actual products that are part of the order.  Those products
        /// are stored in the different orders in the OrderHistoryList
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewOrderHistory_RowStateChanged_1(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (e.StateChanged != DataGridViewElementStates.Selected) 
            {
                return;
            }
            else
            { 
                Decimal orderNumber = Convert.ToDecimal(e.Row.Cells["orderNumber"].Value);

                Order selectedOrder = orderHistoryList.FirstOrDefault(o => o.orderNumber == orderNumber);

                if(selectedOrder != null)
                {
                    dataGridViewOrderDetails.DataSource = selectedOrder.orderDetails;
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
