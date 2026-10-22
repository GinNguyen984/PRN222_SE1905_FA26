using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using zHoMa.Entities.PhuMT.Models;
using zHoMa.Services.PhuMT.Interface;

namespace zHoMa.RazorWebApp.PhuMT.Pages_HoursePhuMts
{
    public class IndexModel : PageModel
    {
        private readonly IHoursePhuMtService _hoursePhuMtService;
        private readonly IStablePhuMtService _stablePhuMtService;

        public IndexModel(IHoursePhuMtService hoursePhuMtService, IStablePhuMtService stablePhuMtService)
        {
            _hoursePhuMtService = hoursePhuMtService;
            _stablePhuMtService = stablePhuMtService;
        }

        public IList<HoursePhuMt> HoursePhuMt { get; set; } = new List<HoursePhuMt>();

        [BindProperty(SupportsGet = true)]
        public string? Code { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Name { get; set; }

        [BindProperty(SupportsGet = true)]
        public decimal? Weight { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? WeightOp { get; set; } = ">=";

        public string? CodeError { get; set; }

        public int TotalCount { get; set; }
        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public decimal AvgWeight { get; set; }

        public async Task OnGetAsync()
        {
            var stables = await _stablePhuMtService.GetAllAsync();
            ViewData["Stables"] = new SelectList(stables, "StableCode", "StableCode");

            var allItems = await _hoursePhuMtService.GetAllAsync();

            // Calculate overall metrics
            TotalCount = allItems.Count;
            ActiveCount = allItems.Count(h => h.IsActive == true);
            InactiveCount = TotalCount - ActiveCount;
            AvgWeight = (allItems.Any(h => h.Weight.HasValue))
                ? allItems.Where(h => h.Weight.HasValue).Average(h => h.Weight!.Value)
                : 0m;

            var query = allItems.AsEnumerable();

            // 3-layer filter
            if (!string.IsNullOrWhiteSpace(Code) || !string.IsNullOrWhiteSpace(Name) || Weight.HasValue)
            {
                // Layer 1: Code (ID numeric)
                if (!string.IsNullOrWhiteSpace(Code))
                {
                    string searchCode = Code.Trim();
                    if (!int.TryParse(searchCode, out int parsedId))
                    {
                        CodeError = "Mã ID chỉ được nhập số!";
                        ModelState.AddModelError("Code", "Mã ID chỉ được nhập số!");
                        query = query.Where(h => false);
                    }
                    else
                    {
                        query = query.Where(h => h.HoursePhuMtid == parsedId);
                    }
                }

                // Layer 2: Name (case-insensitive contains)
                if (!string.IsNullOrWhiteSpace(Name))
                {
                    string searchName = Name.Trim();
                    query = query.Where(h => h.Name != null && h.Name.Contains(searchName, StringComparison.OrdinalIgnoreCase));
                }

                // Layer 3: Weight with Operator
                if (Weight.HasValue)
                {
                    decimal targetWeight = Weight.Value;
                    query = WeightOp switch
                    {
                        ">"  => query.Where(h => h.Weight.HasValue && h.Weight.Value > targetWeight),
                        "<"  => query.Where(h => h.Weight.HasValue && h.Weight.Value < targetWeight),
                        "<=" => query.Where(h => h.Weight.HasValue && h.Weight.Value <= targetWeight),
                        "="  => query.Where(h => h.Weight.HasValue && h.Weight.Value == targetWeight),
                        _    => query.Where(h => h.Weight.HasValue && h.Weight.Value >= targetWeight)
                    };
                }
            }

            HoursePhuMt = query.ToList();
        }
    }
}
