using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using zHoMa.Entities.PhuMT.Models;
using zHoMa.Services.PhuMT.Interface;

namespace zHoMa.MVCWebapp.PhuMT.Controllers
{
    [Authorize] // Bắt buộc phải đăng nhập mới được vào xem bất kỳ trang nào của ngựa
    public class HoursePhuMtsController : Controller
    {
        private readonly IHoursePhuMtService _hoursePhuMtService;
        private readonly IStablePhuMtService _stablePhuMtService;

        public HoursePhuMtsController(IHoursePhuMtService hoursePhuMtService, 
                                      IStablePhuMtService stablePhuMtService) => 
                                    (_hoursePhuMtService, _stablePhuMtService) = 
                                    (hoursePhuMtService, stablePhuMtService);

        // GET / POST: HoursePhuMts
        public async Task<IActionResult> Index(string? Code, string? Name, decimal? Weight, string? WeightOp = ">=")
        {
            var stables = await _stablePhuMtService.GetAllAsync();
            ViewBag.Code = new SelectList(stables, "StableCode", "StableCode");
            ViewBag.Name = new SelectList(stables, "StableCode", "StableCode");
            ViewBag.Weight = new SelectList(stables, "StableCode", "StableCode");

            ViewData["Code"] = Code;
            ViewData["Name"] = Name;
            ViewData["Weight"] = Weight;
            ViewData["WeightOp"] = WeightOp ?? ">=";

            // 1. Lấy toàn bộ danh sách ngựa từ Service
            var items = await _hoursePhuMtService.GetAllAsync();

            // 2. Lọc 3 lớp bằng LINQ trực tiếp trong Controller (Không đụng Entity)
            if (!string.IsNullOrWhiteSpace(Code) || !string.IsNullOrWhiteSpace(Name) || Weight.HasValue)
            {
                var query = items.AsEnumerable();

                // Lớp 1: Search theo ID (chỉ được nhập số, nếu nhập chữ thì báo lỗi)
                if (!string.IsNullOrWhiteSpace(Code))
                {
                    string searchCode = Code.Trim();
                    if (!int.TryParse(searchCode, out int parsedId))
                    {
                        ViewBag.CodeError = "Mã ID chỉ được nhập số!";
                        ModelState.AddModelError("Code", "Mã ID chỉ được nhập số!");
                        query = query.Where(h => false);
                    }
                    else
                    {
                        query = query.Where(h => h.HoursePhuMtid == parsedId);
                    }
                }

                // Lớp 2: Search theo Tên (Contains, không phân biệt hoa thường)
                if (!string.IsNullOrWhiteSpace(Name))
                {
                    string searchName = Name.Trim();
                    query = query.Where(h => h.Name != null && h.Name.Contains(searchName, StringComparison.OrdinalIgnoreCase));
                }

                // Lớp 3: Search theo Cân nặng (Option lớn hơn / nhỏ hơn / bằng)
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

                items = query.ToList();
            }

            return View("Index", items);
        }

        // GET: HoursePhuMts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoursePhuMt = await _hoursePhuMtService.GetByIdAsync(id.Value);
            if (hoursePhuMt == null)
            {
                return NotFound();
            }

            return View(hoursePhuMt);
        }

        // GET: HoursePhuMts/Create
        public async Task<IActionResult> Create()
        {
            var stables = await _stablePhuMtService.GetAllAsync();
            ViewData["StablePhuMtId"] = new SelectList(stables, "StablePhuMtid", "StableCode");

            var item = new HoursePhuMt
            {
                Name = "",
                Weight = 480.00m,
                DateOfBirth = DateOnly.FromDateTime(DateTime.Today.AddYears(-3)),
                IsActive = true,
                Pedigree = ""
            };
            return View(item);
        }

        // POST: HoursePhuMts/Create
        [Authorize(Roles = "1,2")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HoursePhuMtid,Name,Weight,DateOfBirth,IsActive,Pedigree")] HoursePhuMt hoursePhuMt)
        {
            if (ModelState.IsValid)
            {
                await _hoursePhuMtService.Create(hoursePhuMt);
                return RedirectToAction(nameof(Index));
            }

            var stables = await _stablePhuMtService.GetAllAsync();
            ViewData["StablePhuMtId"] = new SelectList(stables, "StablePhuMtid", "StableCode");
            return View(hoursePhuMt);
        }

        // GET: HoursePhuMts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoursePhuMt = await _hoursePhuMtService.GetByIdAsync(id.Value);
            if (hoursePhuMt == null)
            {
                return NotFound();
            }

            var stables = await _stablePhuMtService.GetAllAsync();
            ViewData["StablePhuMtId"] = new SelectList(stables, "StablePhuMtid", "StableCode");
            return View(hoursePhuMt);
        }

        // POST: HoursePhuMts/Edit/5
        [Authorize(Roles = "1,2")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HoursePhuMtid,Name,Weight,DateOfBirth,IsActive,Pedigree")] HoursePhuMt hoursePhuMt)
        {
            if (id != hoursePhuMt.HoursePhuMtid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _hoursePhuMtService.Update(hoursePhuMt);
                }
                catch (Exception)
                {
                    var exists = await _hoursePhuMtService.GetByIdAsync(id);
                    if (exists == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            var stables = await _stablePhuMtService.GetAllAsync();
            ViewData["StablePhuMtId"] = new SelectList(stables, "StablePhuMtid", "StableCode");
            return View(hoursePhuMt);
        }

        // GET: HoursePhuMts/Delete/5
        [Authorize(Roles = "1")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoursePhuMt = await _hoursePhuMtService.GetByIdAsync(id.Value);
            if (hoursePhuMt == null)
            {
                return NotFound();
            }

            return View(hoursePhuMt);
        }

        // POST: HoursePhuMts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id, int? HoursePhuMtid)
        {
            int targetId = id ?? HoursePhuMtid ?? 0;
            if (targetId > 0)
            {
                await _hoursePhuMtService.Delete(targetId);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

