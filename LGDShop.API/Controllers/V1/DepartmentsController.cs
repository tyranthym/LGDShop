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

namespace LGDShop.API.Controllers.V1
{
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
            List<DepartmentGetAllResponseIndividual> departmentGetAllResponse = DepartmentMapper.MapFromDepartmentsToDepartmentGetAllResponse(db, department);
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
                return BadRequest("Id not provided!");
            }
            Department department = await departmentService.FindDepartmentAsync(id);
            if (department == null)
            {
                logger.Here().Warning($"{nameof(Department)} does not exist. Id passed: {id}");
                return NotFound($"{nameof(Department)} does not exist!");
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
            if (!ModelState.IsValid)
            {
                logger.Here().Warning("Create department failed. Model is not valid");
                return BadRequest(ModelState);
            }

            //map to entity
            Department department = DepartmentMapper.MapFromDepartmentCreateRequestToDepartment(requestModel);
            db.Departments.Add(department);
            await db.SaveChangesAsync();
            logger.Here().Information("Created department successfully");

            return CreatedAtRoute("GetDepartmentById", new { id = department.DepartmentId }, null);
        }
    }
}