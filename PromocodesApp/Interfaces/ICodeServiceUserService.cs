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
        string CurrentUserName();
        Task<IList<CodeServiceUser>> Get();
        Task<CodeServiceUser> Get(int codeId, int serviceId);
        Task<CodeServiceUser> Post(CodeServiceUser itm);
        Task<bool> Delete(int codeId, int serviceId);
    }
}
