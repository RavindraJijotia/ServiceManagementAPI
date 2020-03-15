using System.Threading;
using System.Threading.Tasks;

namespace SM.Application.Interfaces
{
    public interface ISampleDataSeeder
    {
        Task SeedAllAsync(CancellationToken cancellationToken);
    }
}
