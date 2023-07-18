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
    public class LeavesController : Controller
    {
        private readonly PmsDataContext _context;

        public LeavesController(PmsDataContext context)
        {
            _context = context;
        }

        // GET: Leaves
        public async Task<IActionResult> Index()
        {
              return _context.Leave != null ? 
                          View(await _context.Leave
                          .Where(x => x.LeaveStatus == "Pending for approval")
                          .ToListAsync()) :
                          Problem("Entity set 'PmsDataContext.Leave'  is null.");
        }

        public async Task<IActionResult> LeavesReport()
        {
            return _context.Leave != null ?
                        View(await _context.Leave
                          .Where(x => x.LeaveStatus != "Pending for approval")
                          .ToListAsync()) :
                        Problem("Entity set 'PmsDataContext.Leave'  is null.");
        }

        // GET: Leaves/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Leave == null)
            {
                return NotFound();
            }

            var leave = await _context.Leave
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leave == null)
            {
                return NotFound();
            }
            return View(leave);
        }

        // GET: Leaves/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Leaves/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Leave leave)
        {
            string email = User.Identity.Name;
            Employee emp = await _context.Employee.FirstOrDefaultAsync(m => m.MailId.Equals(email));
            if (emp == null)
            {
                return NotFound();
            }
            leave.EmployeeId = emp.EmployeeId;
            leave.EmployeeName = emp.FirstName + " " + emp.LastName;

            if (leave.LeaveStatus == null)
            {
                leave.LeaveStatus = "Pending for approval";
            }

            _context.Add(leave);
            await _context.SaveChangesAsync();

            //string query = "Select count(*) from Leave where EmployeeId = {0}";
            emp.NumOfLeaves = await _context.Leave.Where(l => l.EmployeeId == emp.EmployeeId).CountAsync();

            _context.Update(emp);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Leaves/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Leave == null)
            {
                return NotFound();
            }

            var leave = await _context.Leave.FindAsync(id);
            if (leave == null)
            {
                return NotFound();
            }
            return View(leave);
        }

        // POST: Leaves/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Leave leave)
        {
            if (id != leave.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(leave);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeaveExists(leave.Id))
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
            return View(leave);
        }

        // GET: Leaves/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Leave == null)
            {
                return NotFound();
            }

            var leave = await _context.Leave
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leave == null)
            {
                return NotFound();
            }

            return View(leave);
        }

        // POST: Leaves/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Leave == null)
            {
                return Problem("Entity set 'PmsDataContext.Leave'  is null.");
            }
            var leave = await _context.Leave.FindAsync(id);
            if (leave != null)
            {
                _context.Leave.Remove(leave);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeaveExists(int id)
        {
          return (_context.Leave?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> ApproveOrReject(int? id, string? status)
        {
            if (_context.Leave == null)
            {
                return Problem("Entity set 'PmsDataContext.Leave'  is null.");
            }
            var leave = await _context.Leave.FindAsync(id);
            if (leave == null)
            {
                return NotFound();
            }
            
            leave.LeaveStatus = status;

            try
            {
                _context.Update(leave);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeaveExists(leave.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Details), new { id = id });
        }

    }
}
