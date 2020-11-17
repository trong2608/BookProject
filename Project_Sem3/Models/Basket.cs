using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_Sem3.Models
{
    public class Basket
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string Picture { get; set; }
        public double Money { get; set; }
    }
}