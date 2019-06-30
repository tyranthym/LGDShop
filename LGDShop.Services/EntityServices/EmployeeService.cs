using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LGDShop.DataAccess.Data;
using LGDShop.Domain.Entities;

namespace LGDShop.Services.EntityServices
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ShopDbContext db;

        public EmployeeService(ShopDbContext db)
        {
            this.db = db;
        }

        IQueryable<Employee> IEmployeeService.GetEmployees()
        {
            return db.Employees.AsQueryable();
        }

        async Task<Employee> IEmployeeService.FindEmployeeAsync(int? id)
        {
            return await db.Employees.FindAsync(id);
        }

    }
}
