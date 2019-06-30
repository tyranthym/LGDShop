using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LGDShop.Domain.Entities
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }   //pk

        public string Name { get; set; }


        //navigation property
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
