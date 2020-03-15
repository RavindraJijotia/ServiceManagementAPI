using Microsoft.EntityFrameworkCore;
using SM.Application.Interfaces;
using SM.Domain.Common;
using SM.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SM.Persistence
{
    public class SMDbContext : DbContext, ISMDbContext
    {
        public SMDbContext(DbContextOptions<SMDbContext> options) : base(options)
        {

        }

        public DbSet<ServiceCategory> ServiceCategories { get; set; }
        public DbSet<Service> Services { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.IsActive = true;
                        entry.Entity.CreatedBy = Guid.NewGuid(); // _currentUserService.UserId;
                        entry.Entity.Created = DateTime.UtcNow; // _dateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = Guid.NewGuid(); //_currentUserService.UserId;
                        entry.Entity.LastModified = DateTime.UtcNow; // _dateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SMDbContext).Assembly);

            //This will singularize all table names
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                // entity.Relational().TableName = entity.DisplayName();
                modelBuilder.Entity(entity.ClrType).ToTable(entity.ClrType.Name);
            }
        }
    }
}
