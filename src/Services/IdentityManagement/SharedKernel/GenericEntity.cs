using System;
using System.ComponentModel.DataAnnotations;

namespace IdentityManagement.SharedKernel
{
    public abstract class GenericEntity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
    }
}
