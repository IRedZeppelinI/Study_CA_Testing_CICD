
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
        var provider = configuration.GetValue<string>("DatabaseSettings:Provider");
        Console.WriteLine(provider);

        if (provider != null && provider.Equals("PostgreSQL", StringComparison.OrdinalIgnoreCase))
        {
            // Regista PostgreSqlDbContext, mas diz ao DI que ele deve ser usado
            // sempre que alguém pedir um AppDbContextBase.
            services.AddDbContext<AppDbContextBase, PostgreSqlDbContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("PostgreSqlConnection");
                options.UseNpgsql(connectionString,
                    b => b.MigrationsAssembly(typeof(PostgreSqlDbContext).Assembly.FullName));
            });
        }
        else
        {
            // Regista SqlServerDbContext, mas diz ao DI que ele deve ser usado
            // sempre que alguém pedir um AppDbContextBase.
            services.AddDbContext<AppDbContextBase, SqlServerDbContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("SqlConnection");
                options.UseSqlServer(connectionString,
                    b => b.MigrationsAssembly(typeof(SqlServerDbContext).Assembly.FullName));
            });
        }

        services.AddScoped<ICatRepository, CatRepository>();
        return services;
    }
}