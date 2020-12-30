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
    public class ServiceService : IService<ServiceDTO>
    {
        private readonly PromocodesAppContext _context;

        public ServiceService(PromocodesAppContext context) => _context = context;

        public async Task<IList<ServiceDTO>> Get()
        {
            return await _context.Services
                .Select(x => new ServiceDTO(x.Id, x.Name, x.Description))
                .ToListAsync();
        }

        public async Task<ServiceDTO> Get(int id)
        {
            var itm = await _context.Services.FindAsync(id);

            if (itm == null) return null;

            return new ServiceDTO(itm.Id, itm.Name, itm.Description);
        }

        public async Task<ServiceDTO> Put(int id, ServiceDTO itm)
        {
            if (id != itm.Id) return null;

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

            return new ServiceDTO(newItm);
        }

        public bool Exists(int id) => _context.Services.Any(e => e.Id == id);

        public async Task<ServiceDTO> Post(ServiceDTO itm)
        {
            var newItm = new Service
            {
                Name = itm.Name,
                Description = itm.Description
            };

            _context.Services.Add(newItm);
            await _context.SaveChangesAsync();

            return new ServiceDTO(newItm);
        }

        public async Task<bool> Delete(int id)
        {
            var itm = await _context.Codes.FindAsync(id);

            if (itm == null) return false;

            _context.Codes.Remove(itm);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
