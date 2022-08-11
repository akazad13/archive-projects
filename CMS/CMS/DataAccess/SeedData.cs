using CMS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CMS.DataAccess
{
    public static class SeedData
    {
        public static void SeedDatabase(this IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<Role>>();
            var context = services.GetRequiredService<DataContext>();

            context.Database.Migrate();

            if (!userManager.Users.Any())
            {
                // create some roles

                var roles = new List<Role>
                {
                    new Role{Name = "Admin"},
                    new Role{Name = "Student"}
                };

                foreach (var role in roles)
                {
                    roleManager.CreateAsync(role).Wait();
                }


                // create admin user
                var adminUser = new User
                {
                    UserName = "admin",
                    Email = "admin@gmail.com",
                    FirstName = "Firstname",
                    LastName = "Lastname"
                };

                var result = userManager.CreateAsync(adminUser, "Password123").Result;
                if (result.Succeeded)
                {
                    var admin = userManager.FindByNameAsync("Admin").Result;
                    userManager.AddToRolesAsync(admin, new[] { "Admin" }).Wait();

                    context.Courses.Add(new Course()
                    {
                        Name = "Programming",
                        Description = "This is course for coding learning",
                        UserId = admin.Id
                    });

                    context.Quizzes.Add(new Quiz()
                    {
                        QuizName = "Programming",
                        CourseId = 1,
                        UserId = admin.Id
                    });

                    context.Quizzes.Add(new Quiz()
                    {
                        QuizName = "Knowledgebase",
                        CourseId = 1,
                        UserId = admin.Id
                    });

                    context.SaveChanges();
                }
            }
        }
    }
}
