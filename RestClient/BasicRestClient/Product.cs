using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicRestClient
{
    class Product
    {
        public Product(string productId, string productName, string buyPrice, string description, string quantityInStock, string productVendor)
        {
            this.productId = productId;
            this.productName = productName;
            this.buyPrice = decimal.Parse(buyPrice);
            this.description = description;
            this.quantityInStock = int.Parse(quantityInStock);
            this.productVendor = productVendor;
        }

        public string productId { get; set; }
        public string productName { get; set; }
        public decimal buyPrice { get; set; }
        //public Nullable<decimal> MSRP { get; set; }
        public string description { get; set; }
        public int quantityInStock { get; set; }
        public string productVendor { get; set; }

    }
}
