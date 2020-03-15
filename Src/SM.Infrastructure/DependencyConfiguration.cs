using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SM.Application.Interfaces;
using SM.Infrastructure.Mappings;
using SM.Infrastructure.Services;
using System.Reflection;

namespace SM.Infrastructure
{
    public static class DependencyConfiguration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(MappingProfile).Assembly);

            services.AddTransient<ISampleDataSeeder, SampleDataSeeder>();
            services.AddTransient<IServiceCategoryService, ServiceCategoryService>();
            services.AddTransient<IServiceDetailService, ServiceDetailService>();

            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(configuration.GetConnectionString("NorthwindDatabase")));

            return services;
        }
    }
}
