using Core.Configuration;
using Core.Interfaces;
using DataAccess.Data.Interceptors;
using DataAccess.Repository;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zeiss_TakeHome.DataAccess.Data;

namespace DataAccess
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddDbContext<AppDbContext>(options => {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
                options.AddInterceptors(new AuditableEntityInterceptor());
            });

           

            // Register the ID generator
            services.AddScoped<IProductIdGenerator, DbSequenceProductIdGenerator>();

            return services;
        }
    }
}
