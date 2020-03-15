using System;
using System.Collections.Generic;
using System.Text;

namespace SM.Application.Models
{
    public class ServiceCreateModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int ServiceCategoryId { get; set; }
    }
}
