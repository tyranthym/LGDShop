using LGDShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGDShop.DataAccess.IRepos
{
    public interface IEmployeeRepository
    {
        IQueryable<Employee> GetAll();
    }
}
