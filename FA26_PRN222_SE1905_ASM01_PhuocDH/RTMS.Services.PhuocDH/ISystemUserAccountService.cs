using RTMS.Entities.PhuocDH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTMS.Services.PhuocDH
{
    public interface ISystemUserAccountService
    {
        Task<SystemUserAccount> GetUserAccount(string username, string password);
    }
}
