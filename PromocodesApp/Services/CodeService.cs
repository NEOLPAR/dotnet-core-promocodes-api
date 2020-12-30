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
    public class CodeService : IService<CodeDTO>
    {
        private readonly PromocodesAppContext _context;

        public CodeService(PromocodesAppContext context) =>_context = context;

        public async Task<IList<CodeDTO>> Get()
        {
            return await _context.Codes
                .Select(x => new CodeDTO(x.Id, x.Name))
                .ToListAsync();
        }

        public async Task<CodeDTO> Get(int id)
        {
            var itm = await _context.Codes.FindAsync(id);

            if (itm == null) return null;

            return new CodeDTO(itm.Id, itm.Name);
        }

        public async Task<CodeDTO> Put(int id, CodeDTO itm)
        {
            if (id != itm.Id) return null;

            var newCode = await _context.Codes.FindAsync(id);
            
            if (newCode == null) return null;

            newCode.Name = itm.Name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!Exists(id))
            {
                return null;
            }

            return new CodeDTO(newCode);
        }

        public bool Exists(int id) => _context.Codes.Any(e => e.Id == id);

        public async Task<CodeDTO> Post(CodeDTO itm)
        {
            var newCode = new Code
            {
                Name = itm.Name
            };

            _context.Codes.Add(newCode);
            await _context.SaveChangesAsync();

            return new CodeDTO(newCode);
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
