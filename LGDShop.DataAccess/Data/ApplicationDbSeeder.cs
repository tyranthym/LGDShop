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
        public static async Task Seed(ApplicationDbContext db, RoleManager<IdentityRole> roleManager)
        {
            await db.Database.MigrateAsync();

            if (!await db.Roles.AnyAsync(r => r.Name == "superAdmin"))
            {
                var role = new IdentityRole { Name = "SuperAdmin" };
                await roleManager.CreateAsync(role);
            }
            if (!await db.Roles.AnyAsync(r => r.Name == "admin"))
            {
                var role = new IdentityRole { Name = "Admin" };
                await roleManager.CreateAsync(role);
            }
            if (!await db.Roles.AnyAsync(r => r.Name == "employee"))
            {
                var role = new IdentityRole { Name = "Employee" };
                await roleManager.CreateAsync(role);
            }
        }
    }
}
