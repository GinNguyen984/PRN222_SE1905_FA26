using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using zHoMa.Entities.PhuMT.Models;
using zHoMa.Services.PhuMT.Interface;

namespace zHoMa.RazorWebApp.PhuMT.Pages_HoursePhuMts
{
    public class DeleteModel : PageModel
    {
        private readonly IHoursePhuMtService _hoursePhuMtService;

        public DeleteModel(IHoursePhuMtService hoursePhuMtService)
        {
            _hoursePhuMtService = hoursePhuMtService;
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            int targetId = id ?? HoursePhuMt?.HoursePhuMtid ?? 0;
            if (targetId > 0)
            {
                await _hoursePhuMtService.Delete(targetId);
            }

            return RedirectToPage("./Index");
        }
    }
}
