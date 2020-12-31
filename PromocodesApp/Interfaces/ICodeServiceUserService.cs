using Microsoft.AspNetCore.Mvc;
using PromocodesApp.Authentication;
using PromocodesApp.Entities;
using PromocodesApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromocodesApp.Interfaces
{
    public interface ICodeServiceUserService
    {
        Task<IList<CodeServiceUser>> Get(string authorizationHeader);
        Task<CodeServiceUser> Get(int codeId, int serviceId, string authorizationHeader);
        Task<CodeServiceUser> Post(CodeServiceUser itm, string authorizationHeader);
        Task<bool> Delete(int codeId, int serviceId, string authorizationHeader);
    }
}
