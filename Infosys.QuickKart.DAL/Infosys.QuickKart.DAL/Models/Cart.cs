using System;
using System.Collections.Generic;

namespace Infosys.QuickKart.DAL.Models
{
    public partial class Cart
    {
        public string ProductId { get; set; }
        public string EmailId { get; set; }
        public short Quantity { get; set; }

        public Users Email { get; set; }
        public Products Product { get; set; }
    }
}
