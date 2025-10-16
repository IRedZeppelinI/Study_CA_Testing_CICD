using Microsoft.EntityFrameworkCore;

namespace CatsForAdoption.Infrastructure.Persistence;

public class PostgreSqlDbContext : AppDbContextBase
{
    public PostgreSqlDbContext(DbContextOptions<PostgreSqlDbContext> dbContextOptions) : base(dbContextOptions)
    {

    }
}
