using Microsoft.EntityFrameworkCore;
using RTMS.Entities.PhuocDH.Models;
using RTMS.Repositories.PhuocDH.Models;
namespace RTMS.Repositories.PhuocDH
{
    public class RaceResultPhuocDhRepository : GenericRepository<RaceResultPhuocDh>
    {
        public RaceResultPhuocDhRepository() => _context ??= new RTMSContext();
        public RaceResultPhuocDhRepository(RTMSContext context) => _context = context;

        public async Task<List<RaceResultPhuocDh>> GetAll() => await _context.RaceResultPhuocDhs.ToListAsync();

    }
}
