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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Swashbuckle.AspNetCore.Annotations;

namespace LGDShop.API.Controllers.V1
{
    [SwaggerTag("Create, read, update and delete departments. Super-admin role required")]
    [Authorize(Roles = AppRoles.SuperAdmin)]
    public class DepartmentsController : ApiControllerV1BaseController
    {
        private readonly ShopDbContext db;
        private readonly IDepartmentService departmentService;
        private static readonly ILogger logger = Log.ForContext<DepartmentsController>();

        public DepartmentsController(ShopDbContext db, IDepartmentService departmentService)
        {
            this.db = db;
            this.departmentService = departmentService;
            //init default error message
            this.ModelName = nameof(Department);
        }

        /// <summary>
        /// Get all departments
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Department> department = await departmentService.GetDepartments()
                .AsNoTracking()
                .ToListAsync();
            logger.Here().Information("Get departments successfully");
            //map to response
            DepartmentGetAllResponse departmentGetAllResponse = DepartmentMapper.MapFromDepartmentsToDepartmentGetAllResponse(db, department);
            return Ok(departmentGetAllResponse);
        }

        /// <summary>
        /// Get single department by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetDepartmentById")]
        public async Task<IActionResult> GetById(int? id)
        {
            if (id == null)
            {
                return IdNotProvidedBadRequest(logger.Here());
            }
            Department department = await departmentService.FindDepartmentAsync(id);
            if (department == null)
            {
                return ModelNotFound(logger, id);
            }
            return Ok(department);
        }

        /// <summary>
        /// Create new department
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        /// <response code="201">new department has been created successfully</response>
        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<IActionResult> Create([FromBody] DepartmentCreateRequest requestModel)
        {
            //map to entity
            Department department = DepartmentMapper.MapFromDepartmentCreateRequestToDepartment(requestModel);
            db.Departments.Add(department);
            await db.SaveChangesAsync();
            logger.Here().Information("Created department successfully");

            return CreatedAtRoute("GetDepartmentById", new { id = department.DepartmentId }, null);
        }

        /// <summary>
        /// Update existing department
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        /// <response code="204">department has been updated successfully</response>
        [HttpPut]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Update([FromBody] DepartmentUpdateRequest requestModel)
        {
            Department department = await departmentService.FindDepartmentAsync(requestModel.Id);
            if (department == null)
            {
                return ModelNotFound(logger.Here(), requestModel.Id);
            }

            DepartmentMapper.MapFromDepartmentUpdateRequestToDepartment(requestModel, department);
            db.Departments.Update(department);
            await db.SaveChangesAsync();
            logger.Here().Information("Updated department successfully");

            return NoContent();
        }

        /// <summary>
        /// Delete single department
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">department has been deleted successfully</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return IdNotProvidedBadRequest(logger.Here());
            }
            Department department = await departmentService.FindDepartmentAsync(id);
            if (department == null)
            {
                return ModelNotFound(logger.Here(), id);
            }

            //check if this department does not have any employee
            bool isEmptyDepartment = await departmentService.HasNoEmployeeAsync(department.DepartmentId);
            if (isEmptyDepartment == false)
            {
                return ErrorResponseOk(logger.Here(), $"Delete department failed. Cannot delete department that has employees associated with it. DepartmentId: {department.DepartmentId}");
            }

            //delete department
            db.Departments.Remove(department);
            await db.SaveChangesAsync();
            logger.Here().Information("Deleted department successfully");

            //map to delete response
            EntityDeleteResponse entityDeleteResponse = DepartmentMapper.MapFromDepartmentToEntityDeleteResponse(department);
            return Ok(entityDeleteResponse);
        }
    }
}