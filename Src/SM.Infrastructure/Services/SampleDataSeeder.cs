using SM.Application.Interfaces;
using SM.Domain.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SM.Infrastructure.Services
{
    public class SampleDataSeeder : ISampleDataSeeder
    {
        private readonly ISMDbContext _dbContext;

        public SampleDataSeeder(ISMDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task SeedAllAsync(CancellationToken cancellationToken)
        {
            if (!_dbContext.ServiceCategories.Any())
                await SeedServiceCategoriesAsync(cancellationToken);

            if (!_dbContext.Services.Any())
                await SeedServicesAsync(cancellationToken);
        }
        
        private async Task SeedServiceCategoriesAsync(CancellationToken cancellationToken)
        {
            var serviceCategories = new[]
            {
                new ServiceCategory { Name = "Software", Description = "Software Categories", IsActive = true},
                new ServiceCategory { Name = "Hardware", Description = "Hardware Categories",IsActive = true},
                new ServiceCategory { Name = "Administration", Description = "Admin Categories",IsActive = true}
            };

            _dbContext.ServiceCategories.AddRange(serviceCategories);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        private async Task SeedServicesAsync(CancellationToken cancellationToken)
        {
            var services = new[]
            {
                new Service { Name = "Website Development", Description = "Website Development", ServiceCategoryId = 1, IsActive = true},
                new Service { Name = "Mobile Development", Description = "Mobile Development", ServiceCategoryId = 1, IsActive = true},
                new Service { Name = "IT Support", Description = "IT Support", ServiceCategoryId = 1, IsActive = true},
                new Service { Name = "Network Support", Description = "Network Support", ServiceCategoryId = 2, IsActive = true},
                new Service { Name = "Hardware Installation", Description = "Hardware Installation", ServiceCategoryId = 2, IsActive = true},
                new Service { Name = "Recruitment Assistance", Description = "Recruitment Assistance", ServiceCategoryId = 3, IsActive = true}
            };

            _dbContext.Services.AddRange(services);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
