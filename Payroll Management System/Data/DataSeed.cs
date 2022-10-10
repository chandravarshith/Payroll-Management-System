using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Payroll_Management_System.Areas.Identity.Data;
using Payroll_Management_System.Models;

namespace Payroll_Management_System.Data
{
    public static class DataSeed
    {
        public static string AdminRoleId = Guid.NewGuid().ToString();
        public static string EmployeeRoleId = Guid.NewGuid().ToString();
        public static string AdminUserId = Guid.NewGuid().ToString();

        //To initally seed the admin and roles uncomment the below code
     /*
      * public static void SeedAdmin(this ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = AdminRoleId,
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR".ToUpper()
                },
                new IdentityRole
                {
                    Id = EmployeeRoleId,
                    Name = "Employee",
                    NormalizedName = "EMPLOYEE".ToUpper()
                }
            );

            var hasher = new PasswordHasher<IdentityUser>();

            builder.Entity<PmsUser>().HasData(
                   new PmsUser
                   {
                       Id = AdminUserId, // primary key
                       UserName = "admin@pms.com",
                       NormalizedUserName = "ADMIN@PMS.COM",
                       Email = "admin@pms.com",
                       NormalizedEmail = "ADMIN@PMS.COM",
                       EmailConfirmed = true,
                       PasswordHash = hasher.HashPassword(null, "Pms@123")
                   }
                );

            builder.Entity<IdentityUserRole<string>>().HasData(
                    new IdentityUserRole<string>
                    {
                        RoleId = AdminRoleId,
                        UserId = AdminUserId
                    }
                );
        }

        public static void SeedDepartment(this ModelBuilder builder)
        {
            builder.Entity<Department>().HasData(
                    new Department
                    {
                        Id = 1,
                        Name = "Production"
                    },
                    new Department
                    {
                        Id = 2,
                        Name = "Accounts"
                    },
                    new Department
                    {
                        Id = 3,
                        Name = "Management"
                    }
                );
        }
     */

    }
}
