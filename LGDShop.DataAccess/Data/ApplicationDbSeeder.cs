using LGDShop.Domain.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGDShop.DataAccess.Data
{
    public static class ApplicationDbSeeder
    {
        public static async Task Seed(ApplicationDbContext db, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            await db.Database.MigrateAsync();

            //seed roles
            if (!await db.Roles.AnyAsync(r => r.Name == AppRoles.SuperAdmin))
            {
                var role = new IdentityRole { Name = AppRoles.SuperAdmin };
                await roleManager.CreateAsync(role);
            }
            if (!await db.Roles.AnyAsync(r => r.Name == AppRoles.Admin))
            {
                var role = new IdentityRole { Name = AppRoles.Admin };
                await roleManager.CreateAsync(role);
            }
            if (!await db.Roles.AnyAsync(r => r.Name == AppRoles.Employee))
            {
                var role = new IdentityRole { Name = AppRoles.Employee };
                await roleManager.CreateAsync(role);
            }

            //seed one super admin
            if (!await db.Users.AnyAsync(u => u.UserName == "tyranthym1@outlook.com"))
            {
                //create super-admin user
                var user = new ApplicationUser
                {
                    UserName = "tyranthym1@outlook.com",        //default username
                    Email = "tyranthym1@outlook.com",
                    EmailConfirmed = true,
                    LockoutEnabled = true,
                    AccessFailedCount = 5,

                    IsAdmin = true
                };

                IdentityResult result = await userManager.CreateAsync(user, "superadmin");  //default password
                if (result.Succeeded)
                {
                    var roles = new List<string>()
                    {
                        AppRoles.SuperAdmin,
                        AppRoles.Admin
                    };
                    await userManager.AddToRolesAsync(user, roles);
                }
            }
            //seed one admin
            if (!await db.Users.AnyAsync(u => u.UserName == "admin@admin.com"))
            {
                //create super-admin user
                var user = new ApplicationUser
                {
                    UserName = "admin@admin.com",        //default username
                    Email = "admin@admin.com",
                    EmailConfirmed = true,

                    IsAdmin = true
                };

                IdentityResult result = await userManager.CreateAsync(user, "admin");  //default password
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, AppRoles.Admin);
                }
            }
        }
    }
}
