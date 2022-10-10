using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Payroll_Management_System.Areas.Identity.Data;
using Payroll_Management_System.Data;
using static System.Net.Mime.MediaTypeNames;

namespace Payroll_Management_System.Areas.Identity.Data;

public class PmsDataContext : IdentityDbContext<PmsUser>
{
    public PmsDataContext(DbContextOptions<PmsDataContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

    public DbSet<Payroll_Management_System.Models.Level>? Level { get; set; }

    public DbSet<Payroll_Management_System.Models.Employee>? Employee { get; set; }

    public DbSet<Payroll_Management_System.Models.Leave>? Leave { get; set; }

    public DbSet<Payroll_Management_System.Models.Department>? Department { get; set; }

    public DbSet<Payroll_Management_System.Models.Payslip>? Payslip { get; set; }

}
