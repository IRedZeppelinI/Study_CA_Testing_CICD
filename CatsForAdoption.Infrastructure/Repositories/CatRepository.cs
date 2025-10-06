using CatsForAdoption.Application.Interfaces;
using CatsForAdoption.Domain.Entities;
using CatsForAdoption.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CatsForAdoption.Infrastructure.Repositories;

public class CatRepository : ICatRepository
{
    private readonly AppDbContext _context;

    public CatRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Cat>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Cats.AsNoTracking().ToListAsync();
    }
}
