using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromocodesApp.Authentication
{
    public interface IUserService
    {
        string CurrentUserName();
        Task<LoginDTO> Login(LoginRequest model);
        Task<string> Register(RegisterRequest model);
        Task<string> GetId();
        Task<ApplicationUser> Get(string username);
    }
}
