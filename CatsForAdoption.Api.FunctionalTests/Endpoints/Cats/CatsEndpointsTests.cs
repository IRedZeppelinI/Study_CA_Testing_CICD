
using CatsForAdoption.Application.Features.Cats.Dtos;
using CatsForAdoption.Domain.Entities;
using CatsForAdoption.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;

namespace CatsForAdoption.Api.FunctionalTests.Endpoints.Cats;

[Collection("ApiFunctionalTests")]
public class CatsEndpointsTests : IClassFixture<CustomWebApplicationFactory>, IDisposable
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public CatsEndpointsTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    private async Task ResetDatabaseAsync()
    {        
        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        
        await context.Database.EnsureDeletedAsync();
        await context.Database.MigrateAsync();
    }

    [Fact]
    [Trait("Category", "Functional")]
    public async Task GetAllCatsEndpoint_MustReturn200OK_And_CatDtoList()
    {
        await ResetDatabaseAsync();

        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();


        var adoptionCenter = new AdoptionCenter("Leiria", "Rua dos Gatos Felizes, 123");

        context.Add(adoptionCenter);
        await context.SaveChangesAsync();

        var catsToAdd = new List<Cat>
        {
            new Cat("Garfield", "Persa", new DateOnly(2022, 1, 1), adoptionCenter.Id),
            new Cat("Félix", "Siamês", new DateOnly(2023, 5, 10), adoptionCenter.Id),
            new Cat("Tom", "Rafeiro", new DateOnly(2021, 3, 15), adoptionCenter.Id)
        };

        await context.AddRangeAsync(catsToAdd);
        await context.SaveChangesAsync();

        var response = await _client.GetAsync($"/api/Cats");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());

        var catsDto = await response.Content.ReadFromJsonAsync<List<CatDto>>();
        Assert.NotNull(catsDto);
        Assert.Equal(3, catsDto.Count);

        var garfieldDto = catsDto.FirstOrDefault(c => c.Name == "Garfield");
        Assert.NotNull(garfieldDto);
        Assert.Equal("Persa", garfieldDto.Breed);
    }

    [Fact]
    [Trait("Category", "Functional")]
    public async Task GetAllCatsEndpoint_WhenNoCatsExist_Returns200OK_AndEmptyList()
    {
        // ARRANGE
        await ResetDatabaseAsync(); 

        // ACT
        var response = await _client.GetAsync("/api/Cats");

        // ASSERT
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var catsDto = await response.Content.ReadFromJsonAsync<List<CatDto>>();

        Assert.NotNull(catsDto);
        Assert.Empty(catsDto); 
    }

    public void Dispose()
    {
        _client.Dispose();
    }
}
