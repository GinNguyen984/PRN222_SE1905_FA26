using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using zHoMa.Entities.PhuMT.Models;
using zHoMa.Repositories.PhuMT;
using zHoMa.Services.PhuMT.Interface;

namespace zHoMa.RazorWebApp.PhuMT.Pages_HoursePhuMts
{
    public class CreateModel : PageModel
    {
        //private readonly zHoMa.Repositories.PhuMT.RTMSContext _context;
        private readonly IHoursePhuMtService _hoursePhuMtService;
        private readonly IStablePhuMtService _stablePhuMtService;

        //public CreateModel(zHoMa.Repositories.PhuMT.RTMSContext context)
        //{
        //    _context = context;
        //}

        public CreateModel(IHoursePhuMtService hoursePhuMtService, IStablePhuMtService stablePhuMtService)
        {
            _hoursePhuMtService = hoursePhuMtService;
            _stablePhuMtService = stablePhuMtService;
        }

        public async Task<IActionResult> OnGet()
        {
            var categories = await _stablePhuMtService.GetAllAsync();
            ViewData["StablePhuMtId"] = new SelectList(categories, "Id", "Name");
            var item = new HoursePhuMt()
            {
                HoursePhuMtid = 0,
                Name = "",
                Weight = 120,
                DateOfBirth = DateOnly.FromDateTime(DateTime.Now),
                IsActive = true,
                Pedigree = "VIP"

            };

            return Page();
        }

        [BindProperty]
        public HoursePhuMt HoursePhuMt { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //_context.HoursePhuMts.Add(HoursePhuMt);
            //await _context.SaveChangesAsync();

            var result = await _hoursePhuMtService.Create(HoursePhuMt);

            if (result != null)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                var categories = await _stablePhuMtService.GetAllAsync();
                ViewData["StablePhuMtId"] = new SelectList(categories, "Id", "Name");
                return Page();
            }

           
        }
    }
}
