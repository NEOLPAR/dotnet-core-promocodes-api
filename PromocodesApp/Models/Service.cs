using System.Collections.Generic;

namespace PromocodesApp.Models
{
    public class Service
    {
        public int ServiceId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Code> Codes { get; set; }
    }
}
