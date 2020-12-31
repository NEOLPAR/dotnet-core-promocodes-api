using PromocodesApp.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PromocodesApp.Models
{
    public class CodeServiceUser
    {
        [ForeignKey(nameof(Code))]
        public int CodeId { get; set; }
        public Code Code { get; set; }

        [ForeignKey(nameof(Service))]
        public int ServiceId { get; set; }
        public Service Service { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        
        public bool Enabled { get; set; }
        
    }
}
