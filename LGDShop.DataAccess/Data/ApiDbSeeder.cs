using LGDShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LGDShop.DataAccess.Data
{
    public static class ApiDbSeeder
    {
        public static async Task Seed(ShopDbContext db)
        {
            await db.Database.MigrateAsync();
            //if any error occurs, roll  back the entire transaction(avoid database inconsistency)
            using (var transaction = await db.Database.BeginTransactionAsync())
            {
                if (!await db.Positions.AnyAsync())
                {
                    var positionList = new List<Position>
                    {
                        new Position
                        {
                            Rank = 1,
                            Name = "副组长"
                        },
                        new Position
                        {
                            Rank = 2,
                            Name = "组长"
                        },
                        new Position
                        {
                            Rank = 3,
                            Name = "副主任"
                        },
                        new Position
                        {
                            Rank = 4,
                            Name = "主任"
                        },
                        new Position
                        {
                            Rank = 5,
                            Name = "台长助理"
                        },
                        new Position
                        {
                            Rank = 6,
                            Name = "副台长"
                        },
                        new Position
                        {
                            Rank = 7,
                            Name = "台长"
                        }
                    };
                    await db.Positions.AddRangeAsync(positionList);
                    await db.SaveChangesAsync();
                }
                if (!await db.Departments.AnyAsync())
                {
                    var departmentList = new List<Department>
                    {
                        new Department { Name = "Technical"},
                        new Department { Name = "Personnel"},
                        new Department { Name = "Sales"}
                    };
                    await db.Departments.AddRangeAsync(departmentList);
                    await db.SaveChangesAsync();
                }
                transaction.Commit();
            }
        }
    }
}
