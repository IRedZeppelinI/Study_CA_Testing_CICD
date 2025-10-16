using CatsForAdoption.Application.Interfaces;
using CatsForAdoption.Domain.Entities;
using CatsForAdoption.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CatsForAdoption.Infrastructure.Repositories;

public class CatRepository : ICatRepository
{
    private readonly AppDbContextBase _context;

    public CatRepository(AppDbContextBase context)
    {
        _context = context;
    }

    public async Task<List<Cat>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Cats.AsNoTracking().ToListAsync();
    }
}
