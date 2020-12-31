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

        public IList<CodeServiceUserDTO> Codes { get; set; }

        [JsonConstructor]
        public ServiceDTO(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
        public ServiceDTO(Service itm, string username)
        {
            Id = itm.ServiceId;
            Name = itm.Name;
            Description = itm.Description;

            if (itm.CodesServicesUsers != null && itm.CodesServicesUsers.Count > 0)
            {
                Codes = itm.CodesServicesUsers
                    .Where(x => x.ServiceId == itm.ServiceId && x.UserName == username)
                    .Select(x => new CodeServiceUserDTO(x))
                    .ToList();
            }
            else
            {
                Codes = new List<CodeServiceUserDTO>();
            }
        }
    }
}
