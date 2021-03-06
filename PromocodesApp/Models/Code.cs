﻿using PromocodesApp.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PromocodesApp.Models
{
    public class Code
    {
        [Key]
        public int CodeId { get; set; }
        public string Name { get; set; }

        public ICollection<CodeServiceUser> CodesServicesUsers { get; set; }

        public Code() { }
        public Code(CodeDTO itm)
        {
            CodeId = itm.Id;
            Name = itm.Name;
        }
    }
}
