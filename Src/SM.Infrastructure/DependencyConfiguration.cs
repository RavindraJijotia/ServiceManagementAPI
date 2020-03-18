using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SM.Application.Interfaces;
using SM.Common;
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
            services.AddTransient<IDateTime, ServerDateTime>();

            return services;
        }
    }
}
