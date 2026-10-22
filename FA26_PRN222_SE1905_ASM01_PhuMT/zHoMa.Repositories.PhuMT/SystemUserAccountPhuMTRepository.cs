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
    public class SystemUserAccountPhuMTRepository : GenericRepository<SystemUserAccount>
    {
        public SystemUserAccountPhuMTRepository() => _context ??= new RTMSContext();

        public SystemUserAccountPhuMTRepository(RTMSContext context) => _context = context;

        public async Task<SystemUserAccount> GetUserAccount(string userEmail, string password)
        {
            return await _context.SystemUserAccounts.FirstOrDefaultAsync(u => u.Email == userEmail && u.Password == password && u.IsActive);
        }

    }
}
