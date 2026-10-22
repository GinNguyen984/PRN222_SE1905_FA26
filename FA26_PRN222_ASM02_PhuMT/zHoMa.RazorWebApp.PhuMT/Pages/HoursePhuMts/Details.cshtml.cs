using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using zHoMa.Entities.PhuMT.Models;
using zHoMa.Repositories.PhuMT;

namespace zHoMa.RazorWebApp.PhuMT.Pages_HoursePhuMts
{
    public class DetailsModel : PageModel
    {
        private readonly zHoMa.Repositories.PhuMT.RTMSContext _context;

        public DetailsModel(zHoMa.Repositories.PhuMT.RTMSContext context)
        {
            _context = context;
        }

        public HoursePhuMt HoursePhuMt { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoursephumt = await _context.HoursePhuMts.FirstOrDefaultAsync(m => m.HoursePhuMtid == id);
            if (hoursephumt == null)
            {
                return NotFound();
            }
            else
            {
                HoursePhuMt = hoursephumt;
            }
            return Page();
        }
    }
}
