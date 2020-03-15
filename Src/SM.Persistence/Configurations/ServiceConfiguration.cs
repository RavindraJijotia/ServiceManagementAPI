using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SM.Domain.Entities;

namespace SM.Persistence.Configurations
{
    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.Property(e => e.Id).HasColumnName("Id");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(e => e.Description).HasMaxLength(500);

            builder.Property(e => e.IsActive).HasDefaultValue(false);

            builder.Property(e => e.ServiceCategoryId).HasColumnName("ServiceCategoryId");
        }
    }
}
