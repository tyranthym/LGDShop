using LGDShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGDShop.Services.EntityServices
{
    public interface IDepartmentService
    {
        IQueryable<Department> GetDepartments();
        Task<Department> FindDepartmentAsync(int? id);
    }
}
