using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class Position
    {
        [Key]
        public int PositionId { get; set; }   //pk

        public string Name { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
