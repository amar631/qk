using System;
using System.Collections.Generic;

namespace Infosys.QuickKart.DAL.Models
{
    public partial class Products
    {
        public Products()
        {
            Cart = new HashSet<Cart>();
            PurchaseDetails = new HashSet<PurchaseDetails>();
            RatingProduct = new HashSet<Rating>();
            RatingProductNameNavigation = new HashSet<Rating>();
        }

        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public byte? CategoryId { get; set; }
        public decimal Price { get; set; }
        public int QuantityAvailable { get; set; }

        public Categories Category { get; set; }
        public ICollection<Cart> Cart { get; set; }
        public ICollection<PurchaseDetails> PurchaseDetails { get; set; }
        public ICollection<Rating> RatingProduct { get; set; }
        public ICollection<Rating> RatingProductNameNavigation { get; set; }
    }
}
