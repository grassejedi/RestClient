using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicRestClient
{
    class Order
    {
        public Order()
        {

        }


        /// <summary>
        /// Parameterized Constructor.
        /// Purpose:  This will be used when the Admin is pullin all orders
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <param name="orderDate"></param>
        /// <param name="requiredDate"></param>
        /// <param name="shippedDate"></param>
        /// <param name="status"></param>
        /// <param name="comments"></param>
        /// <param name="userNumber"></param>
        /// <param name="orderTotal"></param>
        public Order(int orderNumber, System.DateTime orderDate, System.DateTime requiredDate, Nullable<System.DateTime> shippedDate, string status, string comments, decimal userNumber, decimal orderTotal)
        {
            this.orderNumber = orderNumber;
            this.orderDate = orderDate;
            this.requiredDate = requiredDate;
            this.shippedDate = shippedDate;
            this.status = status;
            this.comments = comments;
            this.userNumber = userNumber;
            this.orderTotal = orderTotal;
        }


        public Order(int orderNumber, System.DateTime orderDate, System.DateTime requiredDate, Nullable<System.DateTime> shippedDate, string status, string comments, decimal orderTotal)
        {
            this.orderNumber = orderNumber;
            this.orderDate = orderDate;
            this.requiredDate = requiredDate;
            this.shippedDate = shippedDate;
            this.status = status;
            this.comments = comments;
            this.orderTotal = orderTotal;

        }

        /// <summary>
        /// Parameterized constructor
        /// Purpose: To be used when sending an Order to api.  
        /// userNumber will be figured out by the api based on the token if it isn't passed in
        /// Admin would need to pass in the userNumber though if an order was being added for other user
        /// </summary>
        /// <param name="comments"></param>
        public Order(string comments, decimal userNumber = default(decimal))
        {
            this.comments = comments;
            orderDate = DateTime.Now;
            this.userNumber = userNumber;
        }

        public int orderNumber { get; set; }
        public System.DateTime orderDate { get; set; }
        public System.DateTime requiredDate { get; set; }
        public Nullable<System.DateTime> shippedDate { get; set; }
        public string status { get; set; }
        public string comments { get; set; }
        public decimal userNumber { get; set; }
        public decimal orderTotal { get; set; }

        public List<OrderDetails> orderDetails { get; set; }

    }
}
