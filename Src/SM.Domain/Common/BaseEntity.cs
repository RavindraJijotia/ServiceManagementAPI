using System;

namespace SM.Domain.Common
{
    public class BaseEntity
    {
        public bool IsActive { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTime Created { get; set; }

        public Guid? LastModifiedBy { get; set; }

        public DateTime? LastModified { get; set; }
    }
}
