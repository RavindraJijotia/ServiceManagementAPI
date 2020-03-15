using SM.Domain.Common;
using SM.Domain.Interfaces;
using System.Collections.Generic;

namespace SM.Domain.Entities
{
    public class ServiceCategory: BaseEntity, IEntityBase<int>
    {
        public ServiceCategory()
        {
            Services = new HashSet<Service>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Service> Services { get; set; }
    }
}
