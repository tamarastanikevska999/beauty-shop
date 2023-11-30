using Core.Entities.Identity;
using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Utility.Extensions
{
    public static class IdentityService
     {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, 
            IConfiguration config)
        {
            services.AddDbContext<AppIdentityDbContext>(options =>
                        options.UseSqlServer(
                            config.GetConnectionString("IdentityConnection")));

            return services;
        }
    }
}