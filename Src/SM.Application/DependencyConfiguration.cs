using Microsoft.Extensions.DependencyInjection;

namespace SM.Application
{
    public static class DependencyConfiguration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            return services;
        }
    }
}
