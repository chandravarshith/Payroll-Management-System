using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payroll_Management_System.Areas.Identity.Data;
using Payroll_Management_System.Models;
using System.Data;
using System.Diagnostics;
using System.Linq;

namespace Payroll_Management_System.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PmsDataContext _context;

        public HomeController(ILogger<HomeController> logger, PmsDataContext context)
        {
            _logger = logger;
            _context = context;
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult AdminHome()
        {
            return View();
        }

        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> EmployeeHome()
        {
            var employee = await _context.Employee
                .FirstOrDefaultAsync(m => m.MailId == User.Identity.Name.ToString());
            if (employee == null)
            {
                return NotFound();
            }
            if (employee.FirstName.Equals(""))
            {
                return RedirectToAction("Edit", "Employees", new { id = employee.EmployeeId });
            }
            return RedirectToAction("Details", "Employees", new { id = employee.EmployeeId });
            
        }

        public IActionResult Index()
        {
            if (User.IsInRole("Employee"))
            {
                return RedirectToAction("EmployeeHome");
            }
            if (User.IsInRole("Administrator")) 
                return RedirectToAction("Index","Employees");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}