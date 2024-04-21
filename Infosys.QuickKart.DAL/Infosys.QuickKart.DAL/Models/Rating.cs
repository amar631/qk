using System;
using System.Collections.Generic;

namespace Infosys.QuickKart.DAL.Models
{
    public partial class Rating
    {
        public string EmailId { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public Nullable<byte> ReviewRating { get; set; }
        public string ReviewComments { get; set; }

        public Users Email { get; set; }
        public Products Product { get; set; }
        public Products ProductNameNavigation { get; set; }
    }
}
