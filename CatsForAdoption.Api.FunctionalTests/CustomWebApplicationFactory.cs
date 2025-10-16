using CatsForAdoption.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CatsForAdoption.Api.FunctionalTests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.RemoveAll<SqlServerDbContext>();
            services.RemoveAll<PostgreSqlDbContext>();
            services.RemoveAll<AppDbContextBase>();

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.FunctionalTests.json")
                .AddEnvironmentVariables()
                .Build();

            var provider = configuration.GetValue<string>("DatabaseSettings:Provider");

            //services.AddDbContext<AppDbContextBase>(options =>
            //{
            //    if (provider != null && provider.Equals("PostgreSQL", StringComparison.OrdinalIgnoreCase))
            //    {
            //        var connectionString = configuration.GetConnectionString("PostgreSqlConnection");
            //        options.UseNpgsql(connectionString,
            //            b => b.MigrationsAssembly(typeof(AppDbContextBase).Assembly.FullName));
            //    }
            //    else
            //    {
            //        var connectionString = configuration.GetConnectionString("SqlConnection");
            //        options.UseSqlServer(connectionString,
            //            b => b.MigrationsAssembly(typeof(AppDbContextBase).Assembly.FullName));
            //    }

            //});

            if (provider != null && provider.Equals("PostgreSQL", StringComparison.OrdinalIgnoreCase))
            {
                services.AddDbContext<PostgreSqlDbContext>(options =>
                {
                    var connectionString = configuration.GetConnectionString("PostgreSqlConnection");
                    options.UseNpgsql(connectionString,
                        b => b.MigrationsAssembly(typeof(PostgreSqlDbContext).Assembly.FullName)); // <- Usando o tipo concreto
                });
                // Mapeia a classe base para a implementação concreta no DI container
                services.AddScoped<AppDbContextBase>(provider => provider.GetRequiredService<PostgreSqlDbContext>());
            }
            else
            {
                services.AddDbContext<SqlServerDbContext>(options =>
                {
                    var connectionString = configuration.GetConnectionString("SqlConnection");
                    options.UseSqlServer(connectionString,
                        b => b.MigrationsAssembly(typeof(SqlServerDbContext).Assembly.FullName)); // <- Usando o tipo concreto
                });
                // Mapeia a classe base para a implementação concreta no DI container
                services.AddScoped<AppDbContextBase>(provider => provider.GetRequiredService<SqlServerDbContext>());
            }

        });
    }
}
