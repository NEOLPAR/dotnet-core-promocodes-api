using PromocodesApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PromocodesApp.Entities
{
    public class ServiceDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [JsonConstructor]
        public ServiceDTO(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
        public ServiceDTO(Service itm)
        {
            Id = itm.Id;
            Name = itm.Name;
            Description = itm.Description;
        }
    }
}
