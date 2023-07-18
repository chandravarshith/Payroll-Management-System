using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Payroll_Management_System.Areas.Identity.Data;
using Payroll_Management_System.Models;

namespace Payroll_Management_System.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly PmsDataContext _context;

        public EmployeesController(PmsDataContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Administrator")]
        // GET: Employees
        public async Task<IActionResult> Index(string searchString)
        {
            if (searchString != null)
            {
                TempData["searchEmp"] = searchString;
            }
            else
            {
                TempData["searchEmp"] = "";
            }
            var emps = await _context.Employee.ToListAsync();
            if (!String.IsNullOrEmpty(searchString))
            {
                emps = await _context.Employee.Where(x => x.MailId.Contains(searchString)).ToListAsync();
            }

            return _context.Employee != null ?
                          View(emps) :
                          Problem("Entity set 'PmsDataContext.Employee'  is null.");
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Employee == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            if(employee.FirstName == "")
            {
                return RedirectToAction("Edit", "Employees", new { id = employee.EmployeeId });
            }

            return View(employee);
        }

        // GET: Employees/Create
        public async Task<IActionResult> Create()
        {
            IEnumerable<Level> LevelList = await _context.Level.ToListAsync();
            IEnumerable<Department> DeptList = await _context.Department.ToListAsync();

            ViewBag.DeptList = DeptList;
            ViewBag.LevelList = LevelList;

            return View();
            //return View();
        }

        // POST: Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee)
        {
            employee.NumOfLeaves = 0;

            if (employee.FirstName == null)
            {
                employee.UserId = TempData.Peek("EmpUserId").ToString();
                employee.MailId = TempData.Peek("EmpMailId").ToString();
                employee.FirstName = "";
                employee.LastName = "";
                employee.Address = "";
                employee.City = "";
                employee.State = "";
                employee.Country = "";
                employee.Zipcode = "";
                employee.BankName = "";
                employee.BankAccountNumber = "";
            }

            _context.Add(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Employee == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }

            if (employee != null)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.EmployeeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                if (User.IsInRole("Employee"))
                    return RedirectToAction("Index", "Home");
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        //Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (_context.Employee == null)
            {
                return Problem("Entity set 'PmsDataContext.Employee'  is null.");
            }
            var employee = await _context.Employee.FindAsync(id);
            if (employee != null)
            {
                _context.Employee.Remove(employee);
            }

            var pendingLeaves = await _context.Leave
                .Where(x => x.EmployeeId == id && x.LeaveStatus == "Pending for approval")
                .ToListAsync();
            if(pendingLeaves != null)
            {
                foreach(var leave in pendingLeaves)
                {
                    _context.Leave.Remove(leave);
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /* POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Employee == null)
            {
                return Problem("Entity set 'PmsDataContext.Employee'  is null.");
            }
            var employee = await _context.Employee.FindAsync(id);
            if (employee != null)
            {
                _context.Employee.Remove(employee);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        */

        public async Task<IActionResult> PaySlip(int? id)
        {
            EmployeePaySlip employeePaySlip = new EmployeePaySlip();

            if (id == null || _context.Employee == null)
            {
                return NotFound();
            }

            employeePaySlip.Emp = await _context.Employee.FindAsync(id);
            if (employeePaySlip.Emp == null)
            {
                return NotFound();
            }

            employeePaySlip.EmpLevel = await _context.Level.FindAsync(employeePaySlip.Emp.LevelId);
            if (employeePaySlip.EmpLevel == null)
            {
                return NotFound();
            }
            if (employeePaySlip.Emp != null)
            {
                var monthYear = DateTime.Now.ToString("MMMM") + " " + DateTime.Now.Year.ToString()+ " "+ employeePaySlip.Emp.EmployeeId; 
                
                employeePaySlip.PaySlip = await _context.Payslip
                .Where(x => x.EmployeeId == employeePaySlip.Emp.EmployeeId && x.Id == monthYear)
                .FirstOrDefaultAsync();
            }
            return View(employeePaySlip);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PaySlip(int id, EmployeePaySlip employeePaySlip)
        {
            employeePaySlip.Emp = await _context.Employee.FindAsync(id);
            if (employeePaySlip.Emp == null)
            {
                return NotFound();
            }

            employeePaySlip.EmpLevel = await _context.Level.FindAsync(employeePaySlip.Emp.LevelId);
            if (employeePaySlip.EmpLevel == null)
            {
                return NotFound();
            }

            employeePaySlip.PaySlip.EmployeeId = id;
            employeePaySlip.PaySlip.Id = employeePaySlip.PaySlip.Month + " " 
                +employeePaySlip.PaySlip.Year + " "
                + employeePaySlip.PaySlip.EmployeeId; // or march2022
            employeePaySlip.PaySlip.LeaveDeduction = 0.0;
            if (employeePaySlip.Emp.NumOfLeaves > employeePaySlip.EmpLevel.MonthlyLeavesPermitted)
            {
                employeePaySlip.PaySlip.LeaveDeduction = (employeePaySlip.Emp.NumOfLeaves -
                                    employeePaySlip.EmpLevel.MonthlyLeavesPermitted) *
                                    employeePaySlip.EmpLevel.LossOfPay;
            }
            employeePaySlip.PaySlip.TotalAllowance = employeePaySlip.EmpLevel.HouseRentAllowance
                                        + employeePaySlip.EmpLevel.MedicalAllowance
                                        + employeePaySlip.EmpLevel.TravelAllowance;

            employeePaySlip.PaySlip.Salary = employeePaySlip.EmpLevel.BasicPay
                                        + employeePaySlip.PaySlip.TotalAllowance
                                        + employeePaySlip.PaySlip.Bonus
                                        - employeePaySlip.PaySlip.LeaveDeduction
                                        - employeePaySlip.EmpLevel.TaxDeductions;


            _context.Add(employeePaySlip.PaySlip);
            await _context.SaveChangesAsync();

            employeePaySlip.Emp.NumOfLeaves = 0;
            _context.Update(employeePaySlip.Emp);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> PaySlipIndex(int? id)
        {
            var payslipStack = await _context.Payslip.ToListAsync();
            if (payslipStack == null)
            {
                return NotFound();
            }

            var tempStack = payslipStack.Where(p => p.EmployeeId == id);
            if (id == null && User.IsInRole("Employee"))
            {
                var emp = await _context.Employee
                    .Where(x => x.MailId == User.Identity.Name.ToString())
                    .FirstOrDefaultAsync();
                if (emp == null)
                {
                    return NotFound();
                }
                id = emp.EmployeeId;
                tempStack = payslipStack.Where(p => p.EmployeeId == id);
            }
            if (tempStack == null)
            {
                 return NotFound();
            }

            payslipStack = tempStack.ToList();

            return _context.Payslip != null ?
                        View(payslipStack) :
                        Problem("Entity set 'PmsDataContext.Payslip'  is null.");
        }

        public async Task<IActionResult> GeneratePaySlip(int? id, string searchString)
        {
            if (searchString != null)
            {
                TempData["searchMonth"] = searchString;
            }
            

            if (_context.Employee == null)
            {
                return NotFound();
            }
            EmployeePaySlip eps = new EmployeePaySlip();
            if (id == null && User.IsInRole("Employee"))
            {
                eps.Emp = await _context.Employee.Where(x => x.MailId == User.Identity.Name.ToString())
                    .FirstOrDefaultAsync();
            }
            else {
                eps.Emp = await _context.Employee.Where(x => x.EmployeeId == id)
                    .FirstOrDefaultAsync();
            }

            if(eps.Emp != null)
            {
                if(searchString == null)
                {
                    searchString = DateTime.Now.ToString("MMMM") + " " + DateTime.Now.Year.ToString() + " " +eps.Emp.EmployeeId;
                    TempData["searchMonth"] = searchString;
                }
                IEnumerable<Payslip> payStack = await _context.Payslip
                    .Where(x => x.EmployeeId == eps.Emp.EmployeeId)
                    .ToListAsync();

                ViewBag.payStack = payStack;
                eps.Emp.FullName = eps.Emp.FirstName + " " + eps.Emp.LastName;
                eps.EmpLevel = await _context.Level.Where(x => x.LevelId == eps.Emp.LevelId)
                    .FirstOrDefaultAsync();
            }

            //DateTime now = DateTime.Now;
            //var monthYear = now.ToString("MMMM") + now.Year.ToString();
            //monthYear = "December2022";
            eps.PaySlip = await _context.Payslip
                .Where(x => x.EmployeeId == eps.Emp.EmployeeId && x.Id == searchString)
                .FirstOrDefaultAsync();

            if(eps.PaySlip != null)
            {
                eps.GrossSalary = eps.EmpLevel.BasicPay
                                            + eps.PaySlip.TotalAllowance
                                            + eps.PaySlip.Bonus;

                eps.Deductions = eps.EmpLevel.TaxDeductions + eps.PaySlip.LeaveDeduction;
            }

            return View(eps);
        }

        private bool EmployeeExists(int id)
        {
          return (_context.Employee?.Any(e => e.EmployeeId == id)).GetValueOrDefault();
        }

    }
}
