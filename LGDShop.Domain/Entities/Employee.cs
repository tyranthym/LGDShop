using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LGDShop.Domain.Entities
{
    public class Employee : EntityBaseCanSoftDelete
    {
        [Key]
        public int EmployeeId { get; set; }

        public string Name { get; set; }
        public int Age { get; set; }  // TODO: replaced by DOB

        [Phone]
        public string PhoneNumber { get; set; }

        public int? DepartmentId { get; set; }   //fk
        public int? PositionId { get; set; }   //fk

        //navigation property 
        public virtual Department Department { get; set; }
        public virtual Position Position { get; set; }
    }
}
