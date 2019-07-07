using LGDShop.DataAccess.Data;
using LGDShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGDShop.Services.EntityServices
{
    public class DepartmentService : IDepartmentService
    {
        private readonly ShopDbContext db;

        public DepartmentService(ShopDbContext db)
        {
            this.db = db;
        }

        /// <summary>
        /// order by name
        /// </summary>
        /// <returns></returns>
        IQueryable<Department> IDepartmentService.GetDepartments()
        {
            return db.Departments.OrderBy(dep => dep.Name);
        }

        async Task<Department> IDepartmentService.FindDepartmentAsync(int? id)
        {
            return await db.Departments.FindAsync(id);
        }
    }
}
