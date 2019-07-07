using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LGDShop.API.ModelMapper.V1;
using LGDShop.API.Models.V1.Requests;
using LGDShop.API.Models.V1.Responses;
using LGDShop.DataAccess.Data;
using LGDShop.Domain.Constants;
using LGDShop.Domain.CustomExtensions;
using LGDShop.Domain.Entities;
using LGDShop.Services.EntityServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Swashbuckle.AspNetCore.Annotations;

namespace LGDShop.API.Controllers.V1
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [SwaggerTag("Create, read, update and delete employees. Admin role required for read access. Admin with " + ApiClaims.Permission + ":" + AppPermissions.EmployeesManage + " claim for full access")]
    [Authorize(Roles = AppRoles.Admin)]
    public class EmployeesController : ApiControllerV1BaseController
    {
        private readonly ShopDbContext db;
        private readonly IEmployeeService employeeService;
        private static readonly ILogger logger = Log.ForContext<EmployeesController>();

        public EmployeesController(ShopDbContext db, IEmployeeService employeeService)
        {
            this.db = db;
            this.employeeService = employeeService;
            //init default error message
            this.ModelName = nameof(Employee);
        }

        /// <summary>
        /// Get first 10 employees
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Employee> employees = await employeeService.GetEmployees()
                .Include(emp => emp.Department)
                .Include(emp => emp.Position)
                .AsNoTracking()
                .Take(10)
                .ToListAsync();
            logger.Here().Information("Get employees successfully");
            //map to response
            List<EmployeeGetAllResponseIndividual> employeeGetAllResponse = EmployeeMapper.MapFromEmployeesToEmployeeGetAllResponse(employees);
            return Ok(employeeGetAllResponse);
        }

        /// <summary>
        /// Get single employee by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetEmployeeById")]
        public async Task<IActionResult> GetById(int? id)
        {
            if (id == null)
            {
                return IdNotProvidedBadRequest(logger.Here());
            }
            Employee employee = await employeeService.FindEmployeeAsync(id);
            if (employee == null)
            {
                return ModelNotFound(logger.Here(), id);
            }
            return Ok(employee);
        }

        /// <summary>
        /// Create new employee
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        /// <response code="201">new employee has been created successfully</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [Authorize(Policy = "CanManageEmployee")]
        public async Task<IActionResult> Create([FromBody] EmployeeCreateRequest requestModel)
        {
            //if (!ModelState.IsValid)
            //{
            //    logger.Here().Warning("Created employee failed. Model is not valid");
            //    return BadRequest(ModelState);
            //}

            //map to entity
            Employee employee = EmployeeMapper.MapFromEmployeeCreateRequestToEmployee(requestModel);
            db.Employees.Add(employee);
            await db.SaveChangesAsync();
            logger.Here().Information("Created employee successfully");

            return CreatedAtRoute("GetEmployeeById", new { id = employee.EmployeeId }, null);
        }

        /// <summary>
        /// Update existing employee
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        /// <response code="204">employee has been updated successfully</response>
        [HttpPut]
        [ProducesResponseType(204)]
        [Authorize(Policy = "CanManageEmployee")]
        public async Task<IActionResult> Update([FromBody] EmployeeUpdateRequest requestModel)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            Employee employee = await employeeService.FindEmployeeAsync(requestModel.Id);
            if (employee == null)
            {
                return ModelNotFound(logger.Here(), requestModel.Id);
            }

            EmployeeMapper.MapFromEmployeeUpdateRequestToEmployee(requestModel, employee);
            db.Employees.Update(employee);
            await db.SaveChangesAsync();
            logger.Here().Information("Updated employee successfully");

            return NoContent();
        }

        /// <summary>
        /// Soft-Delete employee
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="204">employee has been deleted successfully</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [Authorize(Policy = "CanManageEmployee")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return IdNotProvidedBadRequest(logger.Here());
            }
            Employee employee = await employeeService.FindEmployeeAsync(id);
            if (employee == null)
            {
                return ModelNotFound(logger.Here(), id);
            }

            //soft delete employee
            employee.IsDeleted = true;
            employee.DeletedAt = DateTime.UtcNow;
            db.Employees.Update(employee);
            await db.SaveChangesAsync();
            logger.Here().Information("Deleted employee successfully");

            return NoContent();
        }
    }
}