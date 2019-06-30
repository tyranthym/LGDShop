using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LGDShop.DataAccess.Data;
using LGDShop.DataAccess.IRepos;
using LGDShop.Domain.Entities;

namespace LGDShop.DataAccess.Repos
{
    class EmployeeRepository : IEmployeeRepository
    {
        private readonly ShopDbContext db;

        public EmployeeRepository(ShopDbContext db)
        {
            this.db = db;
        }
        IQueryable<Employee> IEmployeeRepository.GetAll()
        {
            return db.Employees.AsQueryable();
        }
    }
}
