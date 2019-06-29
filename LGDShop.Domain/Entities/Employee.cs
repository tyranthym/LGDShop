using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LGDShop.Domain.Entities
{
    public class Employee : EntityBase
    {
        [Key]
        public int EmployeeId { get; set; }

        public string Name { get; set; }
        public int Age { get; set; }

        public int PositionId { get; set; }   //fk

        //navigation property 
        public virtual Position Position { get; set; }
    }
}
