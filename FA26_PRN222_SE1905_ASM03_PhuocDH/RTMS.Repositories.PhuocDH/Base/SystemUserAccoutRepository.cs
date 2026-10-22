using Microsoft.EntityFrameworkCore;
using RTMS.Entities.PhuocDH.Models;
using RTMS.Repositories.PhuocDH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTMS.Repositories.PhuocDH.Base
{
    public class SystemUserAccoutRepository : GenericRepository<SystemUserAccount>
    {
        public SystemUserAccoutRepository() => _context ??= new RTMSContext();

        public SystemUserAccoutRepository(RTMSContext context) => _context = context;

        public async Task<SystemUserAccount> GetUserAccout(string username, string password)
        {
            return await _context.SystemUserAccounts.FirstOrDefaultAsync(u => u.Email == username
            && u.Password == password
            && u.IsActive
            );

            //return await _context.SystemUserAccounts.FirstOrDefaultAsync(u => u.Username == username
            //&& u.Password == password
            //&& u.IsActive
            //);

            //return await _context.SystemUserAccounts.FirstOrDefaultAsync(u => u.Phone == username
            //&& u.Password == password
            //&& u.IsActive
            //);

            //return await _context.SystemUserAccounts.FirstOrDefaultAsync(u => u.EmployeeCode == username
            //&& u.Password == password
            //&& u.IsActive
            //);
        }
    }
}
