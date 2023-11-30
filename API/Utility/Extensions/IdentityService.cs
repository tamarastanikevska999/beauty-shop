using Core.Entities.Identity;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
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
            services.AddIdentityCore<ShopUser>(opt => 
            {
                opt.Password.RequireDigit = true;
                opt.Password.RequireNonAlphanumeric = true;
                opt.Password.RequiredLength = 6;
                opt.Password.RequireUppercase = true;
                opt.Password.RequireLowercase = true;
            })
            .AddEntityFrameworkStores<AppIdentityDbContext>()
            .AddSignInManager<SignInManager<ShopUser>>();

            services.AddAuthentication();
            services.AddAuthorization();
            return services;
        }
    }
}