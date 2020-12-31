using Microsoft.AspNetCore.Mvc;
using PromocodesApp.Authentication;
using PromocodesApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromocodesApp.Interfaces
{
    public interface ICodeServiceUserService
    {
        Task<IList<CodeServiceUserDTO>> Get(string authorizationHeader);
        Task<CodeServiceUserDTO> Get(int codeId, int serviceId, string authorizationHeader);
        Task<CodeServiceUserDTO> Post(CodeServiceUserDTO itm, string authorizationHeader);
        Task<bool> Delete(int codeId, int serviceId, string authorizationHeader);
    }
}
