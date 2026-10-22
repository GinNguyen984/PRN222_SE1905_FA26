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
    public class StablePhuMtService : IStablePhuMtService
    {
        private readonly StablePhuMtRepository _repository;

        public StablePhuMtService() => _repository = new StablePhuMtRepository();


        public async Task<List<StablePhuMt>> GetAllAsync()
        {
            try
            {
                return await _repository.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GetAllAsync", ex);
            }
        }

    }
}
