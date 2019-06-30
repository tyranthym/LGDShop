using FluentValidation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LGDShop.API.Models.V1.Requests
{
    public class EmployeeCreateRequest
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("age")]
        public int Age { get; set; }
        [JsonProperty("phone")]
        [Phone]
        public string PhoneNumber { get; set; }
        [JsonProperty("departmentId")]
        public int? DepartmentId { get; set; }
        [JsonProperty("positionId")]
        public int? PositionId { get; set; }
    }

    //fluent validation validatior
    public class EmployeeCreateRequestValidator : AbstractValidator<EmployeeCreateRequest>
    {
        public EmployeeCreateRequestValidator()
        {
            RuleFor(model => model.Name).NotEmpty().MaximumLength(20);
            RuleFor(model => model.Age).NotEmpty().ExclusiveBetween(0, 100);
        }
    }
}
