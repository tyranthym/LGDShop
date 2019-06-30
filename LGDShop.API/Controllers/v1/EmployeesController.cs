using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LGDShop.API.ModelMapper.V1;
using LGDShop.API.Models.V1.Requests;
using LGDShop.API.Models.V1.Responses;
using LGDShop.DataAccess.Data;
using LGDShop.Domain.Entities;
using LGDShop.Services.EntityServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LGDShop.API.Controllers.V1
{
    public class EmployeesController : ApiControllerV1BaseController
    {
        private readonly ShopDbContext db;
        private readonly IEmployeeService employeeService;

        public EmployeesController(ShopDbContext db, IEmployeeService employeeService)
        {
            this.db = db;
            this.employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Employee> employees = await employeeService.GetEmployees()
                .Include(emp => emp.Department)
                .Include(emp => emp.Position)
                .AsNoTracking()
                .Take(10)
                .ToListAsync();
            List<EmployeeGetAllResponseIndividual> employeeGetAllResponse = EmployeeMapper.MapFromEmployeesToEmployeeGetAllResponse(employees);
            return Ok(employeeGetAllResponse);
        }

        [HttpGet("{id}", Name = "CreateEmployee")]
        public async Task<IActionResult> GetById(int? id)
        {
            if (id == null)
            {
                return BadRequest("id not provided!");
            }
            Employee employee = await employeeService.FindEmployeeAsync(id);
            if (employee == null)
            {
                return NotFound("model does not exist!");
            }
            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EmployeeCreateRequest requestModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Employee employee = EmployeeMapper.MapFromEmployeeCreateRequestToEmployee(requestModel);
            db.Employees.Add(employee);
            await db.SaveChangesAsync();

            return CreatedAtRoute("CreateEmployee", new { id = employee.EmployeeId }, null);
        }
    }
}