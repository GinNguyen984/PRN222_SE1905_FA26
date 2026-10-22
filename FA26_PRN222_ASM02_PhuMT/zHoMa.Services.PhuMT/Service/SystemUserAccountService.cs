using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zHoMa.Entities.PhuMT.Models;
using zHoMa.Repositories.PhuMT;
using zHoMa.Services.PhuMT.Interface;

namespace zHoMa.Services.PhuMT.Service
{
    public class SystemUserAccountService : ISystemUserAccountPhuMtService
    {
        private readonly SystemUserAccountPhuMTRepository _repository;

        public SystemUserAccountService() => _repository = new SystemUserAccountPhuMTRepository();

        public async Task<SystemUserAccount?> GetUserAccount(string username, string password)
        {
            try
            {
                return await _repository.GetUserAccount(username, password);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GetUserAccount" + ex.Message);
            }
        }
    }
}   
