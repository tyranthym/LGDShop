using System;
using System.Collections.Generic;
using System.Text;

namespace LGDShop.Domain.Entities
{
    public class EntityBase : IEntityBase
    {
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
    }

    public interface IEntityBase
    {
        DateTime CreatedAt { get; set; }

        bool IsDeleted { get; set; }
        DateTime? DeletedAt { get; set; }

    }
}
