

using System.Reflection;
using API.Mappers;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Utility.Extensions
{
    public static class AppServices
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services,
            IConfiguration config)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddDbContext<StoreContext>(options =>
                        options.UseSqlServer(
                            config.GetConnectionString("DefaultConnection")));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddAutoMapper(Assembly.GetAssembly(typeof(AutoMapperProfile)));
            return services;
        }
    }
}