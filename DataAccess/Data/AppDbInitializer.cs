using DataAccess.Static;
using Entities.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
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
                        Name = "Admin1",
                        Surname = "Admin1"
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
                        Name = "Voter1",
                        Surname = "Voter1"
                    };

                    await userManager.CreateAsync(newVoterUser, "Voter123!");
                    await userManager.AddToRoleAsync(newVoterUser, UserRoles.Voter);

                }
            }
        }
    }
}
