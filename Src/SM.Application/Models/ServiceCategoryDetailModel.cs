using System;
using System.Collections.Generic;
using System.Text;

namespace SM.Application.Models
{
    public class ServiceCategoryDetailModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<ServiceModel> Services { get; set; }
    }
}
