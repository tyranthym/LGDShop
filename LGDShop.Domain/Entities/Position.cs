﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LGDShop.Domain.Entities
{
    public class Position
    {
        [Key]
        public int PositionId { get; set; }   //pk

        public int Rank { get; set; }     // higher number = higher rank
        public string Name { get; set; }

        //navigation property
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
