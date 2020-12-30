using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PromocodesApp.Models
{
    public class Code
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<CodeServiceUser> CodesServicesUsers { get; set; }
    }
}
