using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Povorot.DAL.Models;

namespace Povorot.Api
{
    public static class DataSeeder
    {
        public static async Task SeedUser(UserManager<User> userManager, RoleManager<IdentityRole<long>> roleManager)
        {
            var username = "povorotAdmin";
            var password = "admin123";
            if (await roleManager.FindByNameAsync(Role.SystemAdmin) == null)
                await roleManager.CreateAsync(new IdentityRole<long>(Role.SystemAdmin));
            if (await roleManager.FindByNameAsync(Role.Manager) == null)
                await roleManager.CreateAsync(new IdentityRole<long>(Role.Manager));
            if (await roleManager.FindByNameAsync(Role.Operator) == null)
                await roleManager.CreateAsync(new IdentityRole<long>(Role.Operator)); 
            if (await roleManager.FindByNameAsync(Role.User) == null)
                await roleManager.CreateAsync(new IdentityRole<long>(Role.User));
            if (await roleManager.FindByNameAsync(Role.Anonym) == null)
                await roleManager.CreateAsync(new IdentityRole<long>(Role.Anonym));
            var admin = await userManager.FindByNameAsync(username);
            if (admin == null)
            {
                User user = new User
                {
                    Email = "povorotAdmin@dd.com",
                    UserName = username,
                    PhoneNumber = "1",
                    AccessFailedCount = 0,
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    PhoneNumberConfirmed = true,
                    TwoFactorEnabled = false,
                };
                user.SecurityStamp = Guid.NewGuid().ToString();
                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                    await userManager.AddToRolesAsync(user, new[] {Role.SystemAdmin});
            }
        }
    }
}