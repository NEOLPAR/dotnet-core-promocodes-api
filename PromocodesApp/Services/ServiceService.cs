using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PromocodesApp.Authentication;
using PromocodesApp.Entities;
using PromocodesApp.Interfaces;
using PromocodesApp.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PromocodesApp.Services
{
    public class ServiceService : IServiceService<Service>
    {
        private readonly PromocodesAppContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ServiceService(PromocodesAppContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public string CurrentUserName() => _httpContextAccessor.HttpContext.User.Identity.Name;
        public async Task<IList<Service>> Get()
        {
            return await _context.Services
                .Include(r => r.CodesServicesUsers)
                .ThenInclude(r => r.Code)
                .ToListAsync();
        }

        public async Task<Service> Get(int id)
        {
            return await _context.Services
                .Include(r => r.CodesServicesUsers)
                .ThenInclude(r => r.Code)
                .FirstOrDefaultAsync(x => x.ServiceId == id);
        }

        public async Task<Service> Put(int id, Service itm)
        {
            if (id != itm.ServiceId) return null;

            var newItm = await _context.Services.FindAsync(id);
            
            if (newItm == null) return null;

            newItm.Name = itm.Name;
            newItm.Description = itm.Description;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!Exists(id))
            {
                return null;
            }

            return newItm;
        }

        public bool Exists(int id) => _context.Services.Any(e => e.ServiceId == id);

        public async Task<Service> Post(Service newItm)
        {
            _context.Services.Add(newItm);
            await _context.SaveChangesAsync();

            return newItm;
        }

        public async Task<bool> Delete(int id)
        {
            var itm = await _context.Services.FindAsync(id);

            if (itm == null) return false;

            _context.Services.Remove(itm);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Service> GetByName(string name)
        {
            var itm = await _context.Services.Where(x => x.Name == name)
                .Include(r => r.CodesServicesUsers)
                .ThenInclude(r => r.Code)
                .FirstOrDefaultAsync();

            if (itm == null) return null;

            return itm;
        }

        public async Task<IList<Service>> FilterByName(string name) =>
            await _context.Services
                .Where(x => x.Name.ToLower().Contains(name.ToLower()))
                .Include(r => r.CodesServicesUsers)
                .ThenInclude(r => r.Code)
                .ToListAsync();

        public async Task<IList<Service>> FilterByNameInfiniteScroll
            (string name, int page, int elements) =>
            await _context.Services
                .Where(x => x.Name.ToLower().Contains(name.ToLower()))
                .Skip((page - 1) * elements).Take(elements)
                .Include(r => r.CodesServicesUsers)
                .ThenInclude(r => r.Code)
                .ToListAsync();

        public async Task<IList<Service>> GetInfiniteScroll(int page, int elements)
        {
            var startPosition = (page - 1) * elements;
            return await _context.Services
                .Skip(startPosition).Take(elements)
                .Include(r => r.CodesServicesUsers)
                .ThenInclude(r => r.Code)
                .ToListAsync();
        }
    }
}
