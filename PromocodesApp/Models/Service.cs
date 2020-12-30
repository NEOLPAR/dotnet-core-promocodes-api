using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PromocodesApp.Models
{
    public class Service
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        public ICollection<CodeServiceUser> CodesServicesUsers { get; set; }
    }
}
