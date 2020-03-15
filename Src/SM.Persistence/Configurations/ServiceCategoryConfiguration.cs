using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SM.Domain.Entities;

namespace SM.Persistence.Configurations
{
    public class ServiceCategoryConfiguration : IEntityTypeConfiguration<ServiceCategory>
    {
        public void Configure(EntityTypeBuilder<ServiceCategory> builder)
        {

            builder.Property(e => e.Id).HasColumnName("Id");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(e => e.Description).HasMaxLength(500);

            builder.Property(e => e.IsActive).HasDefaultValue(false);
        }
    }
}
