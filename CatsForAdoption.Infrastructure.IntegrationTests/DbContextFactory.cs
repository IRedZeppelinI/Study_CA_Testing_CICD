using CatsForAdoption.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CatsForAdoption.Infrastructure.IntegrationTests;

public static class DbContextFactory
{
    public static AppDbContext Create()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Testing.json")
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlServer(connectionString, 
                bd => bd.MigrationsAssembly("CatsForAdoption.Infrastructure")
            ).Options;

        var context = new AppDbContext(options);
        context.Database.EnsureDeleted();
        context.Database.Migrate();

        return context;
    }
}
