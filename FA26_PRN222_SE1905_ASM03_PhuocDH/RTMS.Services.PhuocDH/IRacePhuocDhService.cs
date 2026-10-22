using RTMS.Entities.PhuocDH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTMS.Services.PhuocDH
{
    public interface IRacePhuocDhService
    {
        //Queries statement
        Task<List<RacePhuocDh>> GetAllAsync();
        Task<RacePhuocDh> GetByIdAsync(int RacePhuocDhid);
        Task<List<RacePhuocDh>> SearchAsync(string RaceName, decimal PrizeEarned, int FinishingPosition);
        //Mutation statement
        Task<int> CreateAsync(RacePhuocDh race);
        Task<int> UpdateAsync(RacePhuocDh race);
        Task<bool> DeleteAsync(int RacePhuocDhid);
    }
}
