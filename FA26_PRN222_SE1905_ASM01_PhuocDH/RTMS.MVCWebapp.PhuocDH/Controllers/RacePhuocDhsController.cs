using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RTMS.Entities.PhuocDH.Models;
using RTMS.Repositories.PhuocDH.Models;
using RTMS.Services.PhuocDH;

namespace RTMS.MVCWebapp.PhuocDH.Controllers
{
    public class RacePhuocDhsController : Controller
    {
        //private readonly RTMSContext _context;
        private readonly IRacePhuocDhService _raceService;
        private readonly IRaceResultPhuocDhService _raceResultService;


        public RacePhuocDhsController(IRacePhuocDhService RaceService, IRaceResultPhuocDhService RaceResultService)
        {
            _raceService = RaceService;
            _raceResultService = RaceResultService;
        }

        // GET: RacePhuocDhs
        public async Task<IActionResult> Index(string RaceName, decimal PrizeEarned, int FinishingPosition)
        {
            List<RacePhuocDh> items;
            if (!string.IsNullOrEmpty(RaceName) || PrizeEarned > 0 || FinishingPosition > 0)
            {
                items = await _raceService.SearchAsync(RaceName, PrizeEarned, FinishingPosition);
            }
            else
            {
                items = await _raceService.GetAllAsync();
            }
            return View(items);
        }

        // GET: RacePhuocDhs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var RaceService = await _raceService.GetByIdAsync(id.Value);
            if (RaceService == null)
            {
                return NotFound();
            }

            return View(RaceService);
        }

            // GET: RacePhuocDhs/Create
        public IActionResult Create()
        {
            var item = new RacePhuocDh
            {
                RaceDate = DateTime.UtcNow,
                IsFinished = false
            };

            return View(item);
        }

        // POST: RacePhuocDhs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RacePhuocDhid,RaceName," +
            "PrizePool,RaceDate,IsFinished")] RacePhuocDh racePhuocDh)
        {
            if (ModelState.IsValid)
            {
                var RaceService = await _raceService.CreateAsync(racePhuocDh);
                return RedirectToAction(nameof(Index));
            }
            return View(racePhuocDh);
        }

        // GET: RacePhuocDhs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var RaceService = await _raceService.GetByIdAsync(id.Value);
            if (RaceService == null)
            {
                return NotFound();
            }
            return View(RaceService);
        }

        // POST: RacePhuocDhs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RacePhuocDh racePhuocDh)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    await _raceService.UpdateAsync(racePhuocDh);
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Update error: " + ex.Message);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(racePhuocDh);
        }

        // GET: RacePhuocDhs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var race = await _raceService.GetByIdAsync(id.Value);
             
            if (race == null)
            {
                return NotFound();
            }

            return View(race);
        }

        // POST: RacePhuocDhs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _raceService.DeleteAsync(id); 
            return RedirectToAction(nameof(Index));
        }

        //private bool RacePhuocDhExists(int id)
        //{
        //    return _context.RacePhuocDhs.Any(e => e.RacePhuocDhid == id);
        //}
    }
}
