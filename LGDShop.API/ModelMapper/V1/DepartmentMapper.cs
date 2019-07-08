using LGDShop.API.Models.V1.Requests;
using LGDShop.API.Models.V1.Responses;
using LGDShop.DataAccess.Data;
using LGDShop.Domain.Constants;
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
        public static DepartmentGetAllResponse MapFromDepartmentsToDepartmentGetAllResponse(ShopDbContext db, List<Department> departments)
        {
            DepartmentGetAllResponse departmentGetAllResponse = new DepartmentGetAllResponse
            {
                DepartmentGetAllResponseIndividuals = new List<DepartmentGetAllResponseIndividual>()
            };
            foreach (var department in departments)
            {
                var departmentGetAllResponseIndividual = MapFromDepartmentToDepartmentGetAllResponseIndividual(db, department);
                departmentGetAllResponse.DepartmentGetAllResponseIndividuals.Add(departmentGetAllResponseIndividual);
            }
            departmentGetAllResponse.IsSuccessful = true;
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

        //create department
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

        //update department
        /// <summary>
        /// entity to modify: Department
        /// </summary>
        /// <param name="departmentUpdateRequest"></param>
        /// <param name="department"></param>
        /// <returns></returns>
        public static Department MapFromDepartmentUpdateRequestToDepartment(DepartmentUpdateRequest departmentUpdateRequest, Department department)
        {
            department.Name = departmentUpdateRequest.Name;

            return department;
        }

        //delete department
        public static EntityDeleteResponse MapFromDepartmentToEntityDeleteResponse(Department department)
        {
            EntityDeleteResponse entityDeleteResponse = new EntityDeleteResponse
            {
                Id = department.DepartmentId,
                Entity = EntityType.Department,
                IsDeleted = true,
                IsSuccessful = true
            };
            return entityDeleteResponse;
        }

    }
}
