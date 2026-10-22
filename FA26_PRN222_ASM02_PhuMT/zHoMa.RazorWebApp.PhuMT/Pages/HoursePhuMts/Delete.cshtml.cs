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
    public class DeleteModel : PageModel
    {
        private readonly zHoMa.Repositories.PhuMT.RTMSContext _context;

        public DeleteModel(zHoMa.Repositories.PhuMT.RTMSContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoursephumt = await _context.HoursePhuMts.FindAsync(id);
            if (hoursephumt != null)
            {
                HoursePhuMt = hoursephumt;
                _context.HoursePhuMts.Remove(HoursePhuMt);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
