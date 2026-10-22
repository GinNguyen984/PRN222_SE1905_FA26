using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using zHoMa.Entities.PhuMT.Models;
using zHoMa.Services.PhuMT.Interface;

namespace zHoMa.RazorWebApp.PhuMT.Pages_HoursePhuMts
{
    public class CreateModel : PageModel
    {
        private readonly IHoursePhuMtService _hoursePhuMtService;
        private readonly IStablePhuMtService _stablePhuMtService;

        public CreateModel(IHoursePhuMtService hoursePhuMtService, IStablePhuMtService stablePhuMtService)
        {
            _hoursePhuMtService = hoursePhuMtService;
            _stablePhuMtService = stablePhuMtService;
        }

        [BindProperty]
        public HoursePhuMt HoursePhuMt { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            var stables = await _stablePhuMtService.GetAllAsync();
            ViewData["StablePhuMtId"] = new SelectList(stables, "StablePhuMtid", "StableCode");

            HoursePhuMt = new HoursePhuMt
            {
                Name = "",
                Weight = 480.00m,
                DateOfBirth = DateOnly.FromDateTime(DateTime.Today.AddYears(-3)),
                IsActive = true,
                Pedigree = ""
            };

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

            await _hoursePhuMtService.Create(HoursePhuMt);
            return RedirectToPage("./Index");
        }
    }
}
