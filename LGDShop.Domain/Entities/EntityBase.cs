using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LGDShop.Domain.Entities
{
    public interface IEntityBase
    {
        DateTime CreatedAt { get; set; }
        //string DisplayId { get; set; }
    }

    public interface IEntityBaseCanSoftDelete
    {
        DateTime CreatedAt { get; set; }
        bool IsDeleted { get; set; }
        DateTime? DeletedAt { get; set; }
    }

    public interface IEntityBaseAuditable
    {
        DateTime CreatedAt { get; set; }
        string CreatedBy { get; set; }

        DateTime? LastUpdatedAt { get; set; }
        string LastUpdatedBy { get; set; }

        bool IsDeleted { get; set; }
        DateTime? DeletedAt { get; set; }
        string DeletedBy { get; set; }
    }



    public class EntityBase : IEntityBase
    {
        [Required]
        public DateTime CreatedAt { get; set; }
        //public string DisplayId { get; set; }    //TODO: Required
    }

    public class EntityBaseCanSoftDelete : IEntityBaseCanSoftDelete
    {
        [Required]
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
    }

    public class EntityBaseAuditable : IEntityBaseAuditable
    {
        [Required]
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }

        public DateTime? LastUpdatedAt { get; set; }
        public string LastUpdatedBy { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string DeletedBy { get; set; }
    }
}
