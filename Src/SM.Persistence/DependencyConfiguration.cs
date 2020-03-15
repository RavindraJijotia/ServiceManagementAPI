using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SM.Application.Interfaces;

namespace SM.Persistence
{
    public static class DependencyConfiguration
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var useSqlLite = configuration.GetValue<bool>("UseSqlLite");

            if(useSqlLite)
            {
                services.AddDbContext<SMDbContext>(options =>
                    options.UseSqlite(configuration.GetConnectionString("SMSqlLiteDatabase")));
            }
            else
            {
                services.AddDbContext<SMDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("SMSqlDatabase")));
            }

            services.AddScoped<ISMDbContext>(provider => provider.GetService<SMDbContext>());

            return services;
        }
    }
}
