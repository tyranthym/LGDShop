using LGDShop.API.Models.V1.Requests;
using LGDShop.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LGDShop.API.Models.V1.CustomValidator
{
    public class UniqueNameForDepartmentCreateRequest : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var departmentCreateRequest = validationContext.ObjectInstance as DepartmentCreateRequest;
            if (departmentCreateRequest == null) return new ValidationResult("Request model is empty");
            using (ShopDbContext db = new ShopDbContext())
            {
                var departmentWithSameName = db.Departments.FirstOrDefault(dep => dep.Name == departmentCreateRequest.Name);

                if (departmentWithSameName == null)
                    return ValidationResult.Success;
                else
                    return new ValidationResult($"Name '{departmentCreateRequest.Name}' already exists");
            }
        }
    }

    public class UniqueNameForDepartmentUpdateRequest : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var departmentUpdateRequest = validationContext.ObjectInstance as DepartmentUpdateRequest;
            if (departmentUpdateRequest == null) return new ValidationResult("Request model is empty");
            using (ShopDbContext db = new ShopDbContext())
            {
                var departmentWithSameName = db.Departments.FirstOrDefault(dep => dep.Name == departmentUpdateRequest.Name && dep.DepartmentId != departmentUpdateRequest.Id);

                if (departmentWithSameName == null)
                    return ValidationResult.Success;
                else
                    return new ValidationResult($"Name '{departmentUpdateRequest.Name}' already exists");
            }
        }
    }


}
