using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LGDShop.API.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ApiControllerV1BaseController : ControllerBase
    {
    }
}