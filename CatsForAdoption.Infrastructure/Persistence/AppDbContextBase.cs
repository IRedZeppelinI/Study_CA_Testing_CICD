using CatsForAdoption.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CatsForAdoption.Infrastructure.Persistence;

public abstract class AppDbContextBase : DbContext
{

    protected AppDbContextBase(DbContextOptions dbContextOptions) : base(dbContextOptions)
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
