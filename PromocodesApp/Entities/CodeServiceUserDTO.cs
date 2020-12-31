using PromocodesApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PromocodesApp.Entities
{
    public class CodeServiceUserDTO
    {
        public int CodeId { get; set; }

        public int ServiceId { get; set; }

        public string UserId { get; set; }

        public bool Enabled { get; set; }

        [JsonConstructor]
        public CodeServiceUserDTO(int codeId, int serviceId, string userId, bool enabled)
        {
            CodeId = codeId;
            ServiceId = serviceId;
            UserId = userId;
            Enabled = enabled;
        }
        public CodeServiceUserDTO(CodeServiceUser itm)
        {
            CodeId = itm.CodeId;
            ServiceId = itm.ServiceId;
            UserId = itm.UserId;
            Enabled = itm.Enabled;
        }
    }
}
