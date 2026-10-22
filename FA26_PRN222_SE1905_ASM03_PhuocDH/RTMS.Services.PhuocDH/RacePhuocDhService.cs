using RTMS.Entities.PhuocDH.Models;
using RTMS.Repositories.PhuocDH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTMS.Services.PhuocDH
{
    public class RacePhuocDhService : IRacePhuocDhService
    {
        private readonly RacePhuocDhRepository _repository;
        public RacePhuocDhService() => _repository ??= new RacePhuocDhRepository();

        public async Task<int> CreateAsync(RacePhuocDh race)
        {
            try
            {
                return await Task.Run(() => _repository.CreateAsync(race));
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error creating cash deposit slip: {ex.Message}", ex);
            }
        }

        public async Task<bool> DeleteAsync(int RacePhuocDhid)
        {
            try
            {
                var item = await this.GetByIdAsync(RacePhuocDhid);
                if(item == null)
                {
                    throw new KeyNotFoundException("Error deleting cash deposit slip with id not found");
                }
                return await Task.Run(() => _repository.Remove(item));

            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error deleting cash deposit slip: {ex.Message}", ex);
            }
        }

        public async Task<List<RacePhuocDh>> GetAllAsync()
        {
            try
            {
                return await Task.Run(() => _repository.GetAllAsync());
            } 
            catch(Exception ex)
            {
                throw new ApplicationException($"Error retrieving cash deposit slip list: {ex.Message}", ex);
            }
        }

        public async Task<RacePhuocDh> GetByIdAsync(int RacePhuocDhid)
        {
            try
            {
                return await Task.Run(() => _repository.GetByIdAsync(RacePhuocDhid));

            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error retrieving cash deposit slip by ID: {ex.Message}", ex);
            }
        }

        public async Task<List<RacePhuocDh>> SearchAsync(string RaceName, decimal PrizeEarned, int FinishingPosition)
        {
            try
            {
                return await Task.Run(() => _repository.SearchAsync(RaceName, PrizeEarned, FinishingPosition));
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error retrieving cash deposit slip list: {ex.Message}", ex);
            }
        }

        public async Task<int> UpdateAsync(RacePhuocDh race)
        {
            try
            {
                return await Task.Run(() => _repository.UpdateAsync(race));
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error updating cash deposit slip: {ex.Message}", ex);
            }
        }
    }
}
