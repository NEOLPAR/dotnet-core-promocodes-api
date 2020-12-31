using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PromocodesApp.Authentication;
using PromocodesApp.Interfaces;
using PromocodesApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromocodesApp.Services
{
    public class CodeServiceUserService : ICodeServiceUserService
    {
        private readonly PromocodesAppContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CodeServiceUserService(PromocodesAppContext context,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public string CurrentUserName() => _httpContextAccessor.HttpContext.User.Identity.Name;
        public async Task<IList<CodeServiceUser>> Get()
        {
            var username = CurrentUserName();

            if (username == null) return null;

            var services = await _context.CodesServicesUsers
                .Include(r => r.Service)
                .Include(r => r.Code)
                .Where(x => x.UserName == username)
                .ToListAsync();

            return services;
        }

        public async Task<CodeServiceUser> Get(int codeId, int serviceId)
        {
            var username = CurrentUserName();

            if (username == null) return null;

            return await _context.CodesServicesUsers
                .Include(r => r.Service)
                .Include(r => r.Code)
                .FirstOrDefaultAsync(x => x.CodeId == codeId &&
                    x.ServiceId == serviceId &&
                    x.UserName == username);
        }
        public async Task<CodeServiceUser> Post(CodeServiceUser itm)
        {
            var codeItm = await _context.Codes.FindAsync(itm.CodeId);
            var serviceItm = await _context.Services.FindAsync(itm.ServiceId);
            var username = CurrentUserName();

            if (codeItm == null || codeItm.CodeId != itm.CodeId ||
                serviceItm == null || serviceItm.ServiceId != itm.ServiceId ||
                username == null) 
                return null;

            var newItm = new CodeServiceUser
            {
                CodeId = itm.CodeId,
                Code = codeItm,
                ServiceId = itm.ServiceId,
                Service = serviceItm,
                UserName = username,
                Enabled = itm.Enabled
            };

            _context.CodesServicesUsers.Add(newItm);
            await _context.SaveChangesAsync();

            return newItm;
        }

        public async Task<bool> Delete(int codeId, int serviceId)
        {
            var username = CurrentUserName();

            if (username == null) return false;

            var itm = await _context.CodesServicesUsers.FindAsync(codeId, serviceId, username);

            if (itm == null) return false;

            _context.CodesServicesUsers.Remove(itm);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
