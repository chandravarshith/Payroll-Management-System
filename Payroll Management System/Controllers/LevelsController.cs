using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Payroll_Management_System.Areas.Identity.Data;
using Payroll_Management_System.Models;

namespace Payroll_Management_System.Controllers
{
    public class LevelsController : Controller
    {
        private readonly PmsDataContext _context;

        public LevelsController(PmsDataContext context)
        {
            _context = context;
        }

        // GET: Levels
        public async Task<IActionResult> Index(string searchString)
        {
            if (searchString != null)
            {
                TempData["searchLevel"] = searchString;
            }
            else
            {
                TempData["searchLevel"] = "";
            }

            var levels = await _context.Level.ToListAsync();
            if (!String.IsNullOrEmpty(searchString))
            {
                levels = await _context.Level.Where(x => x.LevelId.ToString() == searchString).ToListAsync();
            }

            return _context.Level != null ? 
                          View(levels) :
                          Problem("Entity set 'PmsDataContext.Level'  is null.");
        }

        // GET: Levels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Level == null)
            {
                return NotFound();
            }

            var level = await _context.Level
                .FirstOrDefaultAsync(m => m.LevelId == id);
            if (level == null)
            {
                return NotFound();
            }

            return View(level);
        }

        // GET: Levels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Levels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LevelId,BasicPay,HouseRentAllowance,TravelAllowance,MedicalAllowance,TaxDeductions,MonthlyLeavesPermitted,LossOfPay")] Level level)
        {
            //if (ModelState.IsValid)
            //{
                
                _context.Add(level);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            //return View(level);
        }

        // GET: Levels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Level == null)
            {
                return NotFound();
            }

            var level = await _context.Level.FindAsync(id);
            if (level == null)
            {
                return NotFound();
            }
            return View(level);
        }

        // POST: Levels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LevelId,BasicPay,HouseRentAllowance,TravelAllowance,MedicalAllowance,TaxDeductions,MonthlyLeavesPermitted,LossOfPay")] Level level)
        {
            if (id != level.LevelId)
            {
                return NotFound();
            }

            if (level != null)
            {
                try
                {
                    _context.Update(level);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LevelExists(level.LevelId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details), new { id = level.LevelId });
            }
            return View(level);
        }

        //Levels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (_context.Level == null)
            {
                return Problem("Entity set 'PmsDataContext.Level'  is null.");
            }
            var level = await _context.Level.FindAsync(id);
            if (level != null)
            {
                _context.Level.Remove(level);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /* POST: Levels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Level == null)
            {
                return Problem("Entity set 'PmsDataContext.Level'  is null.");
            }
            var level = await _context.Level.FindAsync(id);
            if (level != null)
            {
                _context.Level.Remove(level);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        */

        private bool LevelExists(int id)
        {
          return (_context.Level?.Any(e => e.LevelId == id)).GetValueOrDefault();
        }
    }
}
