using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Payroll_Management_System.Areas.Identity.Data;
using static System.Net.Mime.MediaTypeNames;


var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("PmsDataContextConnection") ?? throw new InvalidOperationException("Connection string 'PmsDataContextConnection' not found.");

builder.Services.AddDbContext<PmsDataContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<PmsUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddRoleManager<RoleManager<IdentityRole>>()
    .AddEntityFrameworkStores<PmsDataContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IUserClaimsPrincipalFactory<PmsUser>,
    UserClaimsPrincipalFactory<PmsUser, IdentityRole>>();

/*builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
    options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
});*/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.UseStaticFiles();

app.Run();
