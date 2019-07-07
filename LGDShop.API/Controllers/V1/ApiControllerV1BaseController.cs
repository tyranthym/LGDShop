﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace LGDShop.API.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ApiControllerV1BaseController : ControllerBase
    {
        private string modelName = string.Empty;
        protected string ModelName
        {
            get
            {
                return modelName;
            }
            set
            {
                modelName = value;
                defaultErrorMessageIdNotProvided = $"Id not provided for {modelName}";
                defaultErrorMessageModelNotFound = $"{modelName} not found";
            }
        }
        protected string defaultErrorMessageIdNotProvided = "Id not provided";
        protected string defaultErrorMessageModelNotFound = "Model not found";

        /// <summary>
        /// Log 400BadRequest, 
        /// Creates an Microsoft.AspNetCore.Mvc.BadRequestObjectResult that produces a Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest response.
        /// </summary>
        /// <param name="logger"></param>
        /// <returns>The created Microsoft.AspNetCore.Mvc.BadRequestObjectResult for the response.</returns>
        protected BadRequestObjectResult IdNotProvidedBadRequest(ILogger logger)
        {
            logger.Warning($"{defaultErrorMessageIdNotProvided}.");
            return BadRequest(defaultErrorMessageIdNotProvided);
        }

        /// <summary>
        /// Log 404NotFound response, 
        /// Creates an Microsoft.AspNetCore.Mvc.NotFoundObjectResult that produces a Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound response.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="id">model primary key</param>
        /// <returns>The created Microsoft.AspNetCore.Mvc.NotFoundObjectResult for the response.</returns>
        protected NotFoundObjectResult ModelNotFound(ILogger logger, int? id = null)
        {
            if (id == null)
            {
                logger.Warning($"{defaultErrorMessageModelNotFound}.");
            }
            else
            {
                logger.Warning($"{defaultErrorMessageModelNotFound}. Id passed: {id.ToString()}");
            }
            return NotFound(defaultErrorMessageModelNotFound);
        }
    }
}