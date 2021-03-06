﻿using FluentValidation;
using LGDShop.API.Models.V1.CustomValidator;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LGDShop.API.Models.V1.Requests
{
    public class DepartmentCreateRequest
    {
        [JsonProperty("name")]
        [UniqueNameForDepartmentCreateRequest]
        public string Name { get; set; }
    }


    //fluent validation validatior
    public class DepartmentCreateRequestValidator : AbstractValidator<DepartmentCreateRequest>
    {
        public DepartmentCreateRequestValidator()
        {
            RuleFor(model => model.Name).NotEmpty().MaximumLength(50);  //TODO: make sure the name is unique(using fluent validation)
        }
    }
}
