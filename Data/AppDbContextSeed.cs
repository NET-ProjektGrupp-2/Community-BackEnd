﻿using Community_BackEnd.Entities;
using Microsoft.AspNetCore.Identity;

namespace Community_BackEnd.Data
{
    public class AppDbContextSeed
    {
        public class ApplicationDbContextSeed
        {
            public static async Task SeedEssentialsAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
            {
                //Seed Roles
                await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.Administrator.ToString()));
                await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.Moderator.ToString()));
                await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.Author.ToString()));
                await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.User.ToString()));

                //Seed Default User
                var defaultUser = new AppUser("user") { UserName = Authorization.default_username, Email = Authorization.default_email, EmailConfirmed = true, PhoneNumberConfirmed = true };

                if (userManager.Users.All(u => u.Id != defaultUser.Id))
                {
                    await userManager.CreateAsync(defaultUser, Authorization.default_password);
                    await userManager.AddToRoleAsync(defaultUser, Authorization.default_role.ToString());
                }
            }
        }
    }
}
