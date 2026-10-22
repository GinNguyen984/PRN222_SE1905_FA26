using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using zHoMa.Entities.PhuMT.Models;
using zHoMa.Repositories.PhuMT;
using zHoMa.Services.PhuMT.Interface;

namespace zHoMa.RazorWebApp.PhuMT.Pages_HoursePhuMts
{
    public class IndexModel : PageModel
    {
        //private readonly zHoMa.Repositories.PhuMT.RTMSContext _context;
        private readonly IHoursePhuMtService _hoursePhuMtService;
        //private readonly RTMSContext _context;

        //public IndexModel(zHoMa.Repositories.PhuMT.RTMSContext context)
        //{
        //    _context = context;
        //}
        public IndexModel(IHoursePhuMtService hoursePhuMtService)
                {
                    _hoursePhuMtService = hoursePhuMtService;
                }

        public IList<HoursePhuMt> HoursePhuMt { get;set; } = default!;

        public async Task OnGetAsync()
        {
            HoursePhuMt = await _hoursePhuMtService.GetAllAsync();
        }
    }
}
