using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromocodesApp.Models
{
    public class Code
    {
        public int CodeId { get; set; }
        public string Name { get; set; }
        
        public ICollection<Service> Services { get; set; }
    }
}
