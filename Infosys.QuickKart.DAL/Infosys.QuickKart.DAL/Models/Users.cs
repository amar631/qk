using System;
using System.Collections.Generic;

namespace Infosys.QuickKart.DAL.Models
{
    public partial class Users
    {
        public Users()
        {
            Cart = new HashSet<Cart>();
            PurchaseDetails = new HashSet<PurchaseDetails>();
            Rating = new HashSet<Rating>();
        }

        public string EmailId { get; set; }
        public string UserPassword { get; set; }
        public byte? RoleId { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }

        public Roles Role { get; set; }
        public ICollection<Cart> Cart { get; set; }
        public ICollection<PurchaseDetails> PurchaseDetails { get; set; }
        public ICollection<Rating> Rating { get; set; }
    }
}
