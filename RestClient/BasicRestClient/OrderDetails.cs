using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicRestClient
{
    class OrderDetails
    {
        //public OrderDetails(string productName, string productVendor, string description, int quantityOrdered, decimal buyPrice, decimal total)
        //{
        //    this.productName = productName;
        //    this.productVendor = productVendor;
        //    this.description = description;
        //    this.quantityOrdered = quantityOrdered;
        //    this.buyPrice = buyPrice;
        //    this.total = total;
        //}

        ///// <summary>
        ///// Parameterized constructor for OrderDetails.
        ///// Purpose: This will be used when sending an order to API
        ///// </summary>
        ///// <param name="productId"></param>
        ///// <param name="quantityOrdered"></param>
        //public OrderDetails(string productId, int quantityOrdered)
        //{
        //    this.productId = productId;
        //    this.quantityOrdered = quantityOrdered;
        //}

        public string productId {get; set; }
        public string productName { get; set; }
        public string productVendor { get; set; }
        public string description { get; set; }
        public int quantityOrdered { get; set; }
        public decimal buyPrice { get; set; }
        public decimal total { get; set; }
    }
}
