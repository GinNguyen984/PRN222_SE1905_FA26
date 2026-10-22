using RTMS.Entities.PhuocDH.Models;
using RTMS.Repositories.PhuocDH.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTMS.Services.PhuocDH
{
    public class SystemUserAccountService : ISystemUserAccountService
    {
        private readonly SystemUserAccoutRepository _repository;
        public SystemUserAccountService() => _repository ??= new SystemUserAccoutRepository();
        public Task<SystemUserAccount> GetUserAccount(string username, string password)
        {
            try 
            {
                return _repository.GetUserAccout(username, password);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in get user account ", ex);
            }
        }
    }
}
