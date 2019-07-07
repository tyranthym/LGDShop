using FluentValidation;
using LGDShop.API.Models.V1.CustomValidator;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LGDShop.API.Models.V1.Requests
{
    public class DepartmentUpdateRequest
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        [UniqueNameForDepartmentUpdateRequest]
        public string Name { get; set; }
    }

    //fluent validation validatior
    public class DepartmentUpdateRequestValidator : AbstractValidator<DepartmentUpdateRequest>
    {
        public DepartmentUpdateRequestValidator()
        {
            RuleFor(model => model.Id).NotEmpty();
            RuleFor(model => model.Name).NotEmpty().MaximumLength(50);  //TODO: make sure the name is unique(using fluent validation)
        }
    }

}
