using LGDShop.API.Models.V1.Requests;
using LGDShop.API.Models.V1.Responses;
using LGDShop.DataAccess.Data;
using LGDShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LGDShop.API.ModelMapper.V1
{
    public static class DepartmentMapper
    {
        //get all
        public static List<DepartmentGetAllResponseIndividual> MapFromDepartmentsToDepartmentGetAllResponse(ShopDbContext db, List<Department> departments)
        {
            List<DepartmentGetAllResponseIndividual> departmentGetAllResponse = new List<DepartmentGetAllResponseIndividual>();
            foreach (var department in departments)
            {
                var departmentGetAllResponseIndividual = MapFromDepartmentToDepartmentGetAllResponseIndividual(db, department);
                departmentGetAllResponse.Add(departmentGetAllResponseIndividual);
            }
            return departmentGetAllResponse;
        }
        private static DepartmentGetAllResponseIndividual MapFromDepartmentToDepartmentGetAllResponseIndividual(ShopDbContext db, Department department)
        {
            int employeeCount = db.Employees.Count(emp => emp.DepartmentId == department.DepartmentId);
            var departmentGetAllResponseIndividual = new DepartmentGetAllResponseIndividual
            {
                Name = department.Name,
                EmployeeCount = employeeCount
            };
            return departmentGetAllResponseIndividual;
        }

        /// <summary>
        /// entity to add: Department
        /// </summary>
        /// <param name="departmentCreateRequest"></param>
        /// <returns></returns>
        public static Department MapFromDepartmentCreateRequestToDepartment(DepartmentCreateRequest departmentCreateRequest)
        {
            Department department = new Department
            {
                Name = departmentCreateRequest.Name
            };

            return department;
        }

    }
}
