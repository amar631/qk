﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infosys.QuickKart.Services.Models
{
    public class Rating
    {
        public string EmailId { get; set; }
        public string ProductId { get; set; }

        public string? ProductName { get; set; }
        public Nullable<byte> ReviewRating { get; set; }
        public string? ReviewComments { get; set; }

    }
}