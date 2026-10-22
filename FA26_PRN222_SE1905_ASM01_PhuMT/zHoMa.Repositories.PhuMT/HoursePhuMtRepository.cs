using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zHoMa.Entities.PhuMT.Models;
using zHoMa.Repositories.PhuMT.Base;

namespace zHoMa.Repositories.PhuMT
{
    public class HoursePhuMtRepository : GenericRepository<HoursePhuMt>
    {
        public HoursePhuMtRepository() => _context ??= new RTMSContext();
        public HoursePhuMtRepository(RTMSContext context) => _context = context;

        public async Task<List<HoursePhuMt>> GetAllAsync()
        {
            return await _context.HoursePhuMts.Include(c => c.StablePhuMts).ToListAsync();
        }
        public new async Task<HoursePhuMt?> GetByIdAsync(int id)
        {
            return await _context.HoursePhuMts.Include(c => c.StablePhuMts).FirstOrDefaultAsync(c => c.HoursePhuMtid == id);
        }
        public async Task<List<HoursePhuMt>> SearchAsync (string? name, decimal? weight, string? pedigree)
        {
            return await _context.HoursePhuMts
                .Include(c => c.StablePhuMts)
                .Where(c =>
                    (string.IsNullOrEmpty(name) || c.Name.Contains(name) || (c.Pedigree != null && c.Pedigree.Contains(name)) || c.HoursePhuMtid.ToString() == name) &&
                    (!weight.HasValue || weight.Value <= 0 || c.Weight == weight.Value) &&
                    (string.IsNullOrEmpty(pedigree) || (c.Pedigree != null && c.Pedigree.Contains(pedigree)))
                )
                .ToListAsync();
        }
    }
}
