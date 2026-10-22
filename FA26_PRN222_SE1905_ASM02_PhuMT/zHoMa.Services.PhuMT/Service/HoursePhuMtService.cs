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
    public class HoursePhuMtService : IHoursePhuMtService
    {
        private readonly HoursePhuMtRepository _repository;

        public HoursePhuMtService() => _repository = new HoursePhuMtRepository();



        public async Task<HoursePhuMt> Create(HoursePhuMt HoursePhuMt)
        {
            try
            {
                await _repository.CreateAsync(HoursePhuMt);
                return HoursePhuMt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating HoursePhuMt: " + ex.Message, ex);
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var item = await this.GetByIdAsync(id);
                if (item == null)
                {
                    throw new KeyNotFoundException("Error deleting horse by ID not found");
                }
                return await _repository.RemoveAsync(item);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Error deleting horse by ID: " + ex.Message, ex);
            }
        }

        public async Task<List<HoursePhuMt>> GetAllAsync()
        {
            try
            {
                return await _repository.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving horse list: " + ex.Message, ex);
            }
        }

        public Task<HoursePhuMt> GetByIdAsync(int id)
        {
            try
            {
                return _repository.GetByIdAsync(id)!;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving horse by ID: " + ex.Message, ex);
            }

        }

        public async Task<List<HoursePhuMt>> SearchAsync(string Name, decimal Weight, string Pedigree)
        {
            try
            {
                return await _repository.SearchAsync(Name, Weight, Pedigree);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving cash deposits slip list: " + ex.Message, ex);
            }
        }

        public async Task<HoursePhuMt> Update(HoursePhuMt HoursePhuMt)
        {
            try
            {
                await _repository.UpdateAsync(HoursePhuMt);
                return HoursePhuMt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving cash deposits slip list: " + ex.Message, ex);
            }
        }
    }
}
