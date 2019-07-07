using LGDShop.API.Models.V1.Requests;
using LGDShop.API.Models.V1.Responses;
using LGDShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LGDShop.API.ModelMapper.V1
{
    public static class EmployeeMapper
    {
        //get all
        public static List<EmployeeGetAllResponseIndividual> MapFromEmployeesToEmployeeGetAllResponse(List<Employee> employees)
        {
            List<EmployeeGetAllResponseIndividual> employeeGetAllResponse = new List<EmployeeGetAllResponseIndividual>();
            foreach (var employee in employees)
            {
                var employeeGetAllResponseIndividual = MapFromEmployeeToEmployeeGetAllResponseIndividual(employee);
                employeeGetAllResponse.Add(employeeGetAllResponseIndividual);
            }
            return employeeGetAllResponse;
        }
        private static EmployeeGetAllResponseIndividual MapFromEmployeeToEmployeeGetAllResponseIndividual(Employee employee)
        {
            var departmentId = employee.DepartmentId;
            var positionId = employee.PositionId;

            var employeeGetAllResponseIndividual = new EmployeeGetAllResponseIndividual
            {
                Name = employee.Name,
                Age = employee.Age,
                PhoneNumber = employee.PhoneNumber,
                Department = departmentId == null ? null : employee.Department?.Name,
                Position = positionId == null ? null : employee.Position?.Name,
            };
            return employeeGetAllResponseIndividual;
        }

        //create employee
        /// <summary>
        /// entity to add: Employee
        /// </summary>
        /// <param name="employeeCreateRequest"></param>
        /// <returns></returns>
        public static Employee MapFromEmployeeCreateRequestToEmployee(EmployeeCreateRequest employeeCreateRequest)
        {
            Employee employee = new Employee
            {
                Name = employeeCreateRequest.Name,
                Age = employeeCreateRequest.Age,
                PhoneNumber = employeeCreateRequest.PhoneNumber,
                DepartmentId = employeeCreateRequest.DepartmentId,
                PositionId = employeeCreateRequest.PositionId,

                CreatedAt = DateTime.UtcNow
            };

            return employee;
        }


        //update employee
        /// <summary>
        /// entity to modify: Employee
        /// </summary>
        /// <param name="employeeUpdateRequest"></param>
        /// <param name="employee"></param>
        /// <returns></returns>
        public static Employee MapFromEmployeeUpdateRequestToEmployee(EmployeeUpdateRequest employeeUpdateRequest, Employee employee)
        {
            employee.Name = employeeUpdateRequest.Name;
            employee.Age = employeeUpdateRequest.Age;
            employee.PhoneNumber = employeeUpdateRequest.PhoneNumber;
            employee.DepartmentId = employeeUpdateRequest.DepartmentId;
            employee.PositionId = employeeUpdateRequest.PositionId;

            return employee;
        }
    }
}
