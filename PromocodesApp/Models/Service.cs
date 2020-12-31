using PromocodesApp.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PromocodesApp.Models
{
    public class Service
    {
        [Key]
        public int ServiceId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        public ICollection<CodeServiceUser> CodesServicesUsers { get; set; }

        public Service() { }
        public Service(ServiceDTO itm)
        {
            ServiceId = itm.Id;
            Name = itm.Name;
            Description = itm.Description;
        }
    }
}
