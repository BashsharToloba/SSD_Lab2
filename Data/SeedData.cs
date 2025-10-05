namespace Lab1.Data
{
    using Lab1.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            // Ensure DB is created/migrated
          await context.Database.MigrateAsync();

            // Create roles if missing
            var roles = new[] { "Supervisor", "Employee" };
            foreach (var r in roles)
            {
                if (!await roleManager.RoleExistsAsync(r))
                {
                    await roleManager.CreateAsync(new IdentityRole(r));
                }
            }

            // Supervisor
            string supervisorEmail = "supervisor@example.com";
            var supervisor = await userManager.FindByEmailAsync(supervisorEmail);
            if (supervisor == null)
            {
                supervisor = CreateUser(
                    userName: "supervisor",
                    email: supervisorEmail,
                    firstName: "Sam",
                    lastName: "Supervisor",
                    city: "Hamilton"
                );

                var res = await userManager.CreateAsync(supervisor, "P@ssword1!");
                if (res.Succeeded)
                {
                    await userManager.AddToRoleAsync(supervisor, "Supervisor");
                }
            }

            // Employee 
            string employeeEmail = "employee@example.com";
            var employee = await userManager.FindByEmailAsync(employeeEmail);
            if (employee == null)
            {
                employee = CreateUser(
                    userName: "employee",
                    email: employeeEmail,
                    firstName: "Eve",
                    lastName: "Employee",
                    city: "Hamilton"
                );

                var res = await userManager.CreateAsync(employee, "P@ssword1!");
                if (res.Succeeded)
                {
                    await userManager.AddToRoleAsync(employee, "Employee");
                }
            }

            // Companies 
            if (!await context.Companies.AnyAsync())
            {
                context.Companies.AddRange(
                    new Company
                    {
                        Name = "Acme Corp",
                        YearsInBusiness = 10,
                        Website = "https://acme.example.com",
                        Province = "Ontario"
                    },
                    new Company
                    {
                        Name = "Blue Widgets",
                        YearsInBusiness = 3,
                        Website = "https://bluewidgets.example.com",
                        Province = "Quebec"
                    }
                );
                await context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// to create a new ApplicationUser .
        /// </summary>
        private static ApplicationUser CreateUser(string userName, string email, string firstName, string lastName, string city)
        {
            return new ApplicationUser
            {
                UserName = userName,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                City = city,
                EmailConfirmed = true
            };
        }
    }
}
