using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LGDShop.API.Models.V1.Responses
{
    public class EmployeeGetAllResponseIndividual
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("age")]
        public int Age { get; set; }

        [JsonProperty("phone")]
        public string PhoneNumber { get; set; }

        [JsonProperty("department")]
        public string Department { get; set; }

        [JsonProperty("position")]
        public string Position { get; set; }
    }
}
