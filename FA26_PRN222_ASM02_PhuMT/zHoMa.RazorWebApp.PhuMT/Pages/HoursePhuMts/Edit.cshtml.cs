using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using zHoMa.Entities.PhuMT.Models;
using zHoMa.Repositories.PhuMT;

namespace zHoMa.RazorWebApp.PhuMT.Pages_HoursePhuMts
{
    public class EditModel : PageModel
    {
        private readonly zHoMa.Repositories.PhuMT.RTMSContext _context;

        public EditModel(zHoMa.Repositories.PhuMT.RTMSContext context)
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

            var hoursephumt =  await _context.HoursePhuMts.FirstOrDefaultAsync(m => m.HoursePhuMtid == id);
            if (hoursephumt == null)
            {
                return NotFound();
            }
            HoursePhuMt = hoursephumt;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(HoursePhuMt).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HoursePhuMtExists(HoursePhuMt.HoursePhuMtid))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool HoursePhuMtExists(int id)
        {
            return _context.HoursePhuMts.Any(e => e.HoursePhuMtid == id);
        }
    }
}
