using PromocodesApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PromocodesApp.Entities
{
    public class CodeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonConstructor]
        public CodeDTO(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public CodeDTO(Code itm)
        {
            Id = itm.CodeId;
            Name = itm.Name;
        }
    }
}
