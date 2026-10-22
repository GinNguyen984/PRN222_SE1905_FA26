using RTMS.Entities.PhuocDH.Models;
using RTMS.Repositories.PhuocDH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTMS.Services.PhuocDH
{
    public class RaceResultPhuocDhService : IRaceResultPhuocDhService
    {
        private readonly RaceResultPhuocDhRepository _repository;
        public RaceResultPhuocDhService() => _repository ??= new RaceResultPhuocDhRepository();
        public Task<List<RaceResultPhuocDh>> GetAllAsync()
        {
            try
            {
                return _repository.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in {nameof(GetAllAsync)}: {ex.Message}", ex);
            }
        }
    }
}
