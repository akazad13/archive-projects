using TakeItToTheCloud.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace TakeItToTheCloud.DataAccess
{
    public static class SeedData
    {
        public static async Task SeedDatabase(this IServiceProvider services, IServiceScope scope)
        {
            var userManager = scope.ServiceProvider.GetService<UserManager<User>>();
            var roleManager = scope.ServiceProvider.GetService<RoleManager<Role>>();
            var context = scope.ServiceProvider.GetService<DataContext>();

            await context.Database.MigrateAsync();

            if (!await userManager.Users.AnyAsync())
            {
                // create some roles

                var roles = new List<Role>
                {
                    new Role{Name = "Admin"},
                    new Role{Name = "User"}

                };

                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(role);
                }


                // create admin user
                var adminUser = new User
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    FirstName = "John",
                    LastName = "Doe",
                    About = "Hi, I am admin of this app."
                };

                var result = await userManager.CreateAsync(adminUser, "Password123");
                if (result.Succeeded)
                {
                    await userManager.AddToRolesAsync(adminUser, new[] { "Admin" });

                    context.SaveChanges();
                }
            }
        }
    }
}
