using CatsForAdoption.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CatsForAdoption.Infrastructure.IntegrationTests;

public static class DbContextFactory
{
    public static AppDbContextBase Create()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Testing.json")            
            .AddEnvironmentVariables()
            .Build();

        var provider = configuration.GetValue<string>("DatabaseSettings:Provider");
        
        AppDbContextBase context;

        if (provider != null && provider.Equals("PostgreSQL", StringComparison.OrdinalIgnoreCase))
        {
            var optionsBuilder = new DbContextOptionsBuilder<PostgreSqlDbContext>();

            var connectionString = configuration.GetConnectionString("PostgreSqlConnection");
            optionsBuilder.UseNpgsql(connectionString,
                b => b.MigrationsAssembly(typeof(PostgreSqlDbContext).Assembly.FullName));

            context = new PostgreSqlDbContext(optionsBuilder.Options);
        }
        else
        {
            var optionsBuilder = new DbContextOptionsBuilder<SqlServerDbContext>();

            var connectionString = configuration.GetConnectionString("SqlConnection");
            optionsBuilder.UseSqlServer(connectionString,
                b => b.MigrationsAssembly(typeof(SqlServerDbContext).Assembly.FullName));

            context = new SqlServerDbContext(optionsBuilder.Options);
        }

            
        context.Database.EnsureDeleted();
        //context.Database.Migrate();
        context.Database.EnsureCreated(); // Mudar de Migrate para EnsureCreated

        return context;
    }
}
