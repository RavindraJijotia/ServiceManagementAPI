using System;
using System.Collections.Generic;
using System.Text;

namespace SM.Application.Models
{
    public class BaseEntityModel
    {
        public bool IsActive { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTime Created { get; set; }

        public Guid? LastModifiedBy { get; set; }

        public DateTime? LastModified { get; set; }
    }
}
