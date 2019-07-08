using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LGDShop.API.Models.V1.Responses
{
    public class EmployeeGetAllResponse : ResponseBase
    {
        [JsonProperty("items")]
        public List<EmployeeGetAllResponseIndividual> EmployeeGetAllResponseIndividuals { get; set; }
    }
}
