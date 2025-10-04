using CatsForAdoption.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CatsForAdoption.Infrastructure.Persistence;

public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions)
    {
        
    }

    public DbSet<Cat> Cats { get; set; }
    public DbSet<AdoptionCenter> AdoptionCenters { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

}
