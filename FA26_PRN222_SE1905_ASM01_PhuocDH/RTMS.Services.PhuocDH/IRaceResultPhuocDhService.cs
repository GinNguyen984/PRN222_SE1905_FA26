using RTMS.Entities.PhuocDH.Models;
using RTMS.Repositories.PhuocDH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTMS.Services.PhuocDH
{
    public interface IRaceResultPhuocDhService
    {
        Task<List<RaceResultPhuocDh>> GetAllAsync();
    }
}
