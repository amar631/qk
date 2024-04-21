using System;
using System.Collections.Generic;

namespace Infosys.QuickKart.DAL.Models
{
    public partial class CardDetails
    {
        public decimal CardNumber { get; set; }
        public string NameOnCard { get; set; }
        public string CardType { get; set; }
        public decimal Cvvnumber { get; set; }
        public DateTime ExpiryDate { get; set; }
        public decimal? Balance { get; set; }
    }
}
