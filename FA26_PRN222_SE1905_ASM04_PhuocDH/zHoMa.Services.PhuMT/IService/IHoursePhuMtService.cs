using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zHoMa.Entities.PhuMT.Models;

namespace zHoMa.Services.PhuMT.Interface
{
    public interface IHoursePhuMtService
    {
        Task<List<HoursePhuMt>> GetAllAsync();
        Task<HoursePhuMt> GetByIdAsync(int id);
        Task<List<HoursePhuMt>> SearchAsync(string name, decimal Weight, string Pedigree);

        //Mutation statements
        Task<HoursePhuMt> Create(HoursePhuMt hoursePhuMt);
        Task<HoursePhuMt> Update(HoursePhuMt hoursePhuMt);
        Task<bool> Delete(int id);
    }
}
