using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using zHoMa.Entities.PhuMT.Models;
using zHoMa.Services.PhuMT.Interface;

namespace zHoMa.RazorWebApp.PhuMT.Pages_HoursePhuMts
{
    public class EditModel : PageModel
    {
        private readonly IHoursePhuMtService _hoursePhuMtService;
        private readonly IStablePhuMtService _stablePhuMtService;

        public EditModel(IHoursePhuMtService hoursePhuMtService, IStablePhuMtService stablePhuMtService)
        {
            _hoursePhuMtService = hoursePhuMtService;
            _stablePhuMtService = stablePhuMtService;
        }

        [BindProperty]
        public HoursePhuMt HoursePhuMt { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _hoursePhuMtService.GetByIdAsync(id.Value);
            if (item == null)
            {
                return NotFound();
            }

            HoursePhuMt = item;
            var stables = await _stablePhuMtService.GetAllAsync();
            ViewData["StablePhuMtId"] = new SelectList(stables, "StablePhuMtid", "StableCode");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var stables = await _stablePhuMtService.GetAllAsync();
                ViewData["StablePhuMtId"] = new SelectList(stables, "StablePhuMtid", "StableCode");
                return Page();
            }

            try
            {
                await _hoursePhuMtService.Update(HoursePhuMt);
            }
            catch (Exception)
            {
                var exists = await _hoursePhuMtService.GetByIdAsync(HoursePhuMt.HoursePhuMtid);
                if (exists == null)
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
    }
}
