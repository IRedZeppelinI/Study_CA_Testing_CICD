using CatsForAdoption.Domain.Entities;
using CatsForAdoption.Infrastructure.Persistence;
using CatsForAdoption.Infrastructure.Repositories;

namespace CatsForAdoption.Infrastructure.IntegrationTests.Repositories;

[Collection("DatabaseTests")]
public class CatRepositoryTests : IDisposable
{
    private readonly AppDbContext _context;

    public CatRepositoryTests()
    {
        _context = DbContextFactory.Create();
    }


    [Fact]
    [Trait("Category", "Integration")]
    public async Task GetAllAsync_WhenCatsExist_MustReturnCatsList()
    {
        var adoptionCenter = new AdoptionCenter("Leiria", "Rua dos Gatos Felizes, 123");

        _context.Add(adoptionCenter);
        await _context.SaveChangesAsync();

        var catsToAdd = new List<Cat>
        {
            new Cat("Garfield", "Persa", new DateOnly(2022, 1, 1), adoptionCenter.Id),
            new Cat("Félix", "Siamês", new DateOnly(2023, 5, 10), adoptionCenter.Id),
            new Cat("Tom", "Rafeiro", new DateOnly(2021, 3, 15), adoptionCenter.Id)
        };

        await _context.AddRangeAsync(catsToAdd);
        await _context.SaveChangesAsync();

        var repository = new CatRepository(_context);

        var result = await repository.GetAllAsync(CancellationToken.None);


        Assert.NotNull(result);
        Assert.IsType<List<Cat>>(result);
        Assert.Equal("Garfield", result.First().Name);
    }

    [Fact]
    [Trait("Category", "Integration")]
    public async Task GetAllAsync_WhenCats_NOT_Exist_MustReturnEmptyCatsList()
    {
        var repository = new CatRepository(_context);

        var result = await repository.GetAllAsync(CancellationToken.None);

        Assert.NotNull(result);
        Assert.IsType<List<Cat>>(result);
        Assert.Empty(result);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
