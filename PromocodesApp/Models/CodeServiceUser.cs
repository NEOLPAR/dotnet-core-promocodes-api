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
        public int CodeId { get; set; }

        [ForeignKey("CodeId")]
        public Code Code { get; set; }

        public int ServiceId { get; set; }

        [ForeignKey("ServiceId")]
        public Service Service { get; set; }
        
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        
        public bool Enabled { get; set; }
        
    }
}
