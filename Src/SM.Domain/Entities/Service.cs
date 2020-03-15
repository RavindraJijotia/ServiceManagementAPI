using SM.Domain.Common;
using SM.Domain.Interfaces;

namespace SM.Domain.Entities
{
    public class Service : BaseEntity, IEntityBase<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ServiceCategoryId { get; set; }
        public ServiceCategory ServiceCategory { get; set; }
    }
}
