using System;

namespace IdentityManagement.SharedKernel
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public bool IsActive { get; set; }

    }
}
