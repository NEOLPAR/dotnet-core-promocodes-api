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
        public string CodeName { get; set; }

        public int ServiceId { get; set; }
        public string ServiceName { get; set; }

        public string UserName { get; set; }

        public bool Enabled { get; set; }

        [JsonConstructor]
        public CodeServiceUserDTO(
            int codeId,
            string codeName,
            int serviceId,
            string serviceName,
            string userName,
            bool enabled)
        {
            CodeId = codeId;
            CodeName = codeName;
            ServiceId = serviceId;
            ServiceName = serviceName;
            UserName = userName;
            Enabled = enabled;
        }
        public CodeServiceUserDTO(CodeServiceUser itm)
        {
            CodeId = itm.CodeId;
            CodeName = itm.Code.Name;
            ServiceId = itm.ServiceId;
            ServiceName = itm.Service.Name;
            UserName = itm.UserName;
            Enabled = itm.Enabled;
        }
    }
}
