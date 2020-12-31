using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PromocodesApp.Authentication;
using PromocodesApp.Entities;
using PromocodesApp.Helpers;
using PromocodesApp.Interfaces;
using PromocodesApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromocodesApp.Services
{
    public class CodeServiceUserService : ICodeServiceUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly PromocodesAppContext _context;
        private readonly IConfiguration _configuration;

        public CodeServiceUserService(PromocodesAppContext context,
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _configuration = configuration;        }

        public async Task<IList<CodeServiceUserDTO>> Get(string authorizationHeader)
        {
            var userItm = await GetUser(authorizationHeader);

            if (userItm == null) return null;

            return await _context.CodesServicesUsers
                .Select(x => new CodeServiceUserDTO
                    (x.CodeId, x.ServiceId, userItm.Id, x.Enabled))
                .ToListAsync();
        }

        public async Task<CodeServiceUserDTO> Get
            (int codeId, int serviceId, string authorizationHeader)
        {
            var userItm = await GetUser(authorizationHeader);

            if (userItm == null) return null;

            var itm = await _context.CodesServicesUsers
                .FindAsync(codeId, serviceId, userItm.Id);

            if (itm == null) return null;

            return new CodeServiceUserDTO
                (itm.CodeId, itm.ServiceId, itm.UserId, itm.Enabled);
        }
        public async Task<CodeServiceUserDTO> Post(CodeServiceUserDTO itm, string authorizationHeader)
        {

            var codeItm = await _context.Codes.FindAsync(itm.CodeId);
            var serviceItm = await _context.Services.FindAsync(itm.ServiceId);
            var userItm = await GetUser(authorizationHeader);

            if (codeItm == null || codeItm.Id != itm.CodeId ||
                serviceItm == null || serviceItm.Id != itm.ServiceId ||
                userItm == null) 
                return null;

            var newItm = new CodeServiceUser
            {
                CodeId = itm.CodeId,
                ServiceId = itm.ServiceId,
                UserId = userItm.Id,
                Enabled = itm.Enabled
            };

            _context.CodesServicesUsers.Add(newItm);
            await _context.SaveChangesAsync();

            return new CodeServiceUserDTO(newItm);
        }

        public async Task<bool> Delete(int codeId, int serviceId, string authorizationHeader)
        {
            var userItm = await GetUser(authorizationHeader);

            if (userItm == null) return false;

            var itm = await _context.CodesServicesUsers.FindAsync(codeId, serviceId, userItm.Id);

            if (itm == null) return false;

            _context.CodesServicesUsers.Remove(itm);
            await _context.SaveChangesAsync();

            return true;
        }

        private async Task<ApplicationUser> GetUser(string authorizationHeader)
        {
            var userService = new UserService(_userManager, _configuration);
            var username = AuthenticationHelper.GetUserFromToken(authorizationHeader, _configuration);

            return await userService.Get(username);
        }
    }
}
