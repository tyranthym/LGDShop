using LGDShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGDShop.Services.EntityServices
{
    public interface IEmployeeService
    {
        IQueryable<Employee> GetEmployees();

        IQueryable<Employee> GetEmployeesForDepartment(int? departmentId);

        IQueryable<Employee> GetEmployeesForPosition(int? positionId);

        Task<Employee> FindEmployeeAsync(int? id);
    }
}
