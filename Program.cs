using Lab1.Data;
using Lab1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));


// for ApplicationUser and IdentityRole.
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // Ensure email confirmation is off for easy local use
    options.SignIn.RequireConfirmedAccount = false;

    

    // Disable lockout during development to prevent repeated failed login attempts
    // from blocking the seeded user, which is a common cause of "failed login."
    options.Lockout.AllowedForNewUsers = false;
    options.Lockout.MaxFailedAccessAttempts = 100; // Larger number to prevent lockout 
})
    // NOTE: .AddRoles<IdentityRole>() is no longer needed here as it's handled by AddIdentity<TUser, TRole>
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders() // Required for password resets/emails
    .AddDefaultUI();           // Ensures Razor Pages (Login, Register) use the correct services


builder.Services.AddControllersWithViews();

var app = builder.Build();

// Seed roles and users (method created below)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedData.InitializeAsync(services);
}

// rest of pipeline
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();
