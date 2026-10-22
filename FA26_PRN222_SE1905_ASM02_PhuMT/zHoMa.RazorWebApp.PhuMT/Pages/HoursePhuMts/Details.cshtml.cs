using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using zHoMa.Entities.PhuMT.Models;
using zHoMa.Services.PhuMT.Interface;

namespace zHoMa.RazorWebApp.PhuMT.Pages_HoursePhuMts
{
    public class DetailsModel : PageModel
    {
        private readonly IHoursePhuMtService _hoursePhuMtService;

        public DetailsModel(IHoursePhuMtService hoursePhuMtService)
        {
            _hoursePhuMtService = hoursePhuMtService;
        }

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
    }
}
