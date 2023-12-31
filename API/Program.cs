using System.Reflection;
using API.Mappers;
using API.Utility.Extensions;
using Core.Entities.Identity;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Data.Seed;
using Infrastructure.Identity;
using Infrastructure.Identity.Seed;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddAppServices(builder.Configuration);
        builder.Services.AddIdentityServices(builder.Configuration);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // app.UseHttpsRedirection();

        app.UseCors("CorsPolicy");
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        //add migrations if not and update database on service start
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<StoreContext>();
        var identityContext = services.GetRequiredService<AppIdentityDbContext>();
        var userManager = services.GetRequiredService<UserManager<ShopUser>>();
        var logger = services.GetRequiredService<ILogger<Program>>();
        try
        {
            await context.Database.MigrateAsync();
            await identityContext.Database.MigrateAsync();
            await AppIdentityDbContextSeed.SeedUsersAsync(userManager,context);
            await DbSeedData.SeedData(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occured during migration and data seeding");
        }

        app.Run();
    }
}