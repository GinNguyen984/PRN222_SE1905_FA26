using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zHoMa.Entities.PhuMT.Models;

namespace zHoMa.Services.PhuMT.Interface
{
    public interface IStablePhuMtService
    {
        //querry statements
        
        Task<List<StablePhuMt>> GetAllAsync();

    }
}
