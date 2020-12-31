using Microsoft.EntityFrameworkCore;
using PromocodesApp.Interfaces;
using PromocodesApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromocodesApp.Services
{
    public class CodeService : IService<Code>
    {
        private readonly PromocodesAppContext _context;

        public CodeService(PromocodesAppContext context) =>_context = context;

        public async Task<IList<Code>> Get()
        {
            return await _context.Codes
                .ToListAsync();
        }

        public async Task<Code> Get(int id)
        {
            return await _context.Codes.FindAsync(id);
        }

        public async Task<Code> Put(int id, Code itm)
        {
            if (id != itm.CodeId) return null;

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

            return newCode;
        }

        public bool Exists(int id) => _context.Codes.Any(e => e.CodeId == id);

        public async Task<Code> Post(Code newCode)
        {
            _context.Codes.Add(newCode);
            await _context.SaveChangesAsync();

            return newCode;
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
