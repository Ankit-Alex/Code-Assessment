using Core.Interfaces;
using Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services) 
        {            
            services.AddScoped<IProductService, ProductService>();

            return services;
        }
    }
}
