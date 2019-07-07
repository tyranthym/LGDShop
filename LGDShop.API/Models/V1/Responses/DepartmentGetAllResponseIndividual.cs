using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LGDShop.API.Models.V1.Responses
{
    public class DepartmentGetAllResponseIndividual
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("employeeCount")]
        public int EmployeeCount { get; set; } 
    }
}
