using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payroll_Management_System.Migrations
{
    public partial class DataSeedmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "140cbe32-e49f-409a-a497-1b25ecd74364", "55dd38c3-6efc-4a9a-9bd4-def5030385f0", "Employee", "EMPLOYEE" },
                    { "177cac45-6263-4edc-9479-e99e618294fc", "9d9f4d6d-a917-4e01-aa69-1de84e63812e", "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "9f790476-5075-4613-9747-e401574b19e1", 0, "dafcdaf4-4584-430c-bf60-431eb08887f3", "admin@pms.com", true, false, null, "ADMIN@PMS.COM", "ADMIN@PMS.COM", "AQAAAAEAACcQAAAAEDkaEHC+KMabtTbP5XtVHBIh7+DAJguH5MOMjacoD9qbHAo7IBGHPoBocxscFGbemw==", null, false, "8dd8b13b-5759-4d16-bf62-e9a27d7cae0e", false, "admin@pms.com" });

            migrationBuilder.InsertData(
                table: "Department",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Production" },
                    { 2, "Accounts" },
                    { 3, "Management" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "177cac45-6263-4edc-9479-e99e618294fc", "9f790476-5075-4613-9747-e401574b19e1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "140cbe32-e49f-409a-a497-1b25ecd74364");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "177cac45-6263-4edc-9479-e99e618294fc", "9f790476-5075-4613-9747-e401574b19e1" });

            migrationBuilder.DeleteData(
                table: "Department",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Department",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Department",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "177cac45-6263-4edc-9479-e99e618294fc");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9f790476-5075-4613-9747-e401574b19e1");
        }
    }
}
