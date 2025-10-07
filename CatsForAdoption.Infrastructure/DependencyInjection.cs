using CatsForAdoption.Application.Interfaces;
using CatsForAdoption.Infrastructure.Persistence;
using CatsForAdoption.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CatsForAdoption.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionStrring = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionStrring));

        services.AddScoped<ICatRepository, CatRepository>();

        return services;
    } 
}
