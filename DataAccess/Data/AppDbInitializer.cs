using DataAccess.Static;
using Entities.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class AppDbInitializer
    {
		public static void ApplyDatabaseMigrations(IApplicationBuilder applicationBuilder)
		{
			using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
			{
				var dbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
				dbContext.Database.Migrate();
			}
		}

		public static async Task SeedUsersAndRoles(IApplicationBuilder applicationBuilder)
        {
            using(var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
				var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                {
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                }
                if (!await roleManager.RoleExistsAsync(UserRoles.Voter))
                {
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Voter));
                }

                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                var adminIdNumber = "11111111111";
                var adminUser = await userManager.FindByNameAsync(adminIdNumber);
                if (adminUser == null)
                {
                    var newAdminUser = new ApplicationUser
                    {
                        UserName = adminIdNumber,
                        IdentificationNumber = adminIdNumber,
                        EmailConfirmed = true,
                        FirstName = "Admin1",
                        Lastname = "Admin1",
                        RegistrationDate = DateTime.Now,
                    };

                    var result = await userManager.CreateAsync(newAdminUser, "Admin123!");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);

                }

                var voterIdNumber = "22222222222";
                var voterUser = await userManager.FindByNameAsync(voterIdNumber);
                if (voterUser == null)
                {
                    var newVoterUser = new ApplicationUser
                    {
                        UserName = voterIdNumber,
                        IdentificationNumber = voterIdNumber,
                        EmailConfirmed = true,
                        FirstName = "Voter1",
                        Lastname = "Voter1",
                        RegistrationDate = DateTime.Now
                    };

                    await userManager.CreateAsync(newVoterUser, "Voter123!");
                    await userManager.AddToRoleAsync(newVoterUser, UserRoles.Voter);

                }
            }
        }
    }
}
