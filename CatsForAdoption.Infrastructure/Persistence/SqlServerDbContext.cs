using Microsoft.EntityFrameworkCore;

namespace CatsForAdoption.Infrastructure.Persistence;

public class SqlServerDbContext : AppDbContextBase
{
    public SqlServerDbContext(DbContextOptions<SqlServerDbContext> options) : base(options)
    {

    }
}
