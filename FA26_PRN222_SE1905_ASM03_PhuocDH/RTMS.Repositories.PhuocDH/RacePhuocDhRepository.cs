using Microsoft.EntityFrameworkCore;
using RTMS.Entities.PhuocDH.Models;
using RTMS.Repositories.PhuocDH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTMS.Repositories.PhuocDH
{
    public class RacePhuocDhRepository : GenericRepository<RacePhuocDh>
    {
        public RacePhuocDhRepository() => _context ??= new RTMSContext();
        public RacePhuocDhRepository(RTMSContext context) => _context = context;

        public async Task<List<RacePhuocDh>> GetAllAsync() => await _context.RacePhuocDhs.Include(n=> n.RaceResultPhuocDhs).ToListAsync();

        public async Task<RacePhuocDh> GetByIdAsync(int id) => await _context.RacePhuocDhs.Include(n => n.RaceResultPhuocDhs).FirstOrDefaultAsync(n => n.RacePhuocDhid == id);

        public async Task<List<RacePhuocDh>> SearchAsync(string RaceName, decimal PrizeEarned, int FinishingPosition)
        {
            var query = _context.RacePhuocDhs.Include(r => r.RaceResultPhuocDhs).AsQueryable();
            if(!string.IsNullOrEmpty(RaceName))
            {
                query = query.Where(r => r.RaceName.Contains(RaceName));
            }
            if(PrizeEarned != null && PrizeEarned > 0)
            {
                query = query.Where(r => r.PrizePool >= PrizeEarned);
            }
            if(FinishingPosition > 0 && FinishingPosition != null)
            {
                query = query.Where(r => r.RaceResultPhuocDhs.Any(rr => rr.FinishingPosition == FinishingPosition));
            }
            return await query.ToListAsync();
        }
    }
}
