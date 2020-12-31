using PromocodesApp.Authentication;
using PromocodesApp.Entities;
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
        public string UserName { get; set; }
        public ApplicationUser User { get; set; }
        
        public bool Enabled { get; set; }

        public CodeServiceUser()
        { }
        public CodeServiceUser(CodeServiceUserDTO itm)
        {
            CodeId = itm.CodeId;
            ServiceId = itm.ServiceId;
            UserName = itm.UserName;
            Enabled = itm.Enabled;
        }
    }
}
