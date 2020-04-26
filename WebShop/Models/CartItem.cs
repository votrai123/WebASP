using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model.EF;

namespace WebShop.Models
{
    [Serializable]
    public class CartItem
    {
        public Product Product { set; get; }
        public int Quantity { set; get; }
        public decimal SubTotal { set; get; }
        public decimal Discount { set; get; }
    }
}