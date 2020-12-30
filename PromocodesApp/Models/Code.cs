﻿using System.Collections.Generic;

namespace PromocodesApp.Models
{
    public class Code
    {
        public int CodeId { get; set; }
        public string Name { get; set; }
        
        public ICollection<Service> Services { get; set; }
    }
}
