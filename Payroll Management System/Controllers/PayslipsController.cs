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
    public class PayslipsController : Controller
    {
        private readonly PmsDataContext _context;

        public PayslipsController(PmsDataContext context)
        {
            _context = context;
        }

        // GET: Payslips
        public async Task<IActionResult> Index()
        {
              return _context.Payslip != null ? 
                          View(await _context.Payslip.ToListAsync()) :
                          Problem("Entity set 'PmsDataContext.Payslip'  is null.");
        }

        // GET: Payslips/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Payslip == null)
            {
                return NotFound();
            }

            var payslip = await _context.Payslip
                .FirstOrDefaultAsync(m => m.Id == id);
            if (payslip == null)
            {
                return NotFound();
            }

            return View(payslip);
        }

        // GET: Payslips/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Payslips/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Year,Month,EmployeeId,Bonus,TotalAllowance,LeaveDeduction,Salary")] Payslip payslip)
        {
            if (ModelState.IsValid)
            {
                _context.Add(payslip);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(payslip);
        }

        // GET: Payslips/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Payslip == null)
            {
                return NotFound();
            }

            var payslip = await _context.Payslip.FindAsync(id);
            if (payslip == null)
            {
                return NotFound();
            }
            return View(payslip);
        }

        // POST: Payslips/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Year,Month,EmployeeId,Bonus,TotalAllowance,LeaveDeduction,Salary")] Payslip payslip)
        {
            if (id != payslip.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(payslip);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PayslipExists(payslip.Id))
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
            return View(payslip);
        }

        // GET: Payslips/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Payslip == null)
            {
                return NotFound();
            }

            var payslip = await _context.Payslip
                .FirstOrDefaultAsync(m => m.Id == id);
            if (payslip == null)
            {
                return NotFound();
            }

            return View(payslip);
        }

        // POST: Payslips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Payslip == null)
            {
                return Problem("Entity set 'PmsDataContext.Payslip'  is null.");
            }
            var payslip = await _context.Payslip.FindAsync(id);
            if (payslip != null)
            {
                _context.Payslip.Remove(payslip);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PayslipExists(string id)
        {
          return (_context.Payslip?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
