using Microsoft.EntityFrameworkCore;
using SM.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace SM.Application.Interfaces
{
    public interface ISMDbContext
    {
        DbSet<ServiceCategory> ServiceCategories { get; set; }
        DbSet<Service> Services { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
