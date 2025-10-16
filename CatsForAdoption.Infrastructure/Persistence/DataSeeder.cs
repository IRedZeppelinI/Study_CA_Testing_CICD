using CatsForAdoption.Domain.Entities;

namespace CatsForAdoption.Infrastructure.Persistence;

public static class DataSeeder
{
    public static async Task SeedAsync(AppDbContextBase context)
    {
        if (context.AdoptionCenters.Any())
        {
            return;
        }

        var centerLeiria = new AdoptionCenter("Leiria", "Rua dos Gatos Felizes, 123");
        var centerLisboa = new AdoptionCenter("Lisboa", "Avenida dos Animais, 789");

        var catGarfield = new Cat("Garfield", "Persa", new DateOnly(2022, 1, 1), 0);
        var catFelix = new Cat("Félix", "Siamês", new DateOnly(2023, 5, 10), 0);
        var catTom = new Cat("Tom", "Rafeiro", new DateOnly(2021, 3, 15), 0);
        catTom.MarkAsAdopted();

        centerLeiria.AddCat(catGarfield);
        centerLeiria.AddCat(catTom);

        var catSimba = new Cat("Simba", "Ashera", new DateOnly(2023, 2, 20), 0);
        var catMittens = new Cat("Mittens", "Scottish Fold", new DateOnly(2022, 8, 5), 0);
        catMittens.MarkAsAdopted();
        var catLeo = new Cat("Leo", "British Shorthair", new DateOnly(2024, 1, 30), 0);

        centerLisboa.AddCat(catSimba);
        centerLisboa.AddCat(catMittens);
        centerLisboa.AddCat(catLeo);

        await context.AdoptionCenters.AddRangeAsync(
            centerLeiria,
            centerLisboa
        );

        await context.SaveChangesAsync();
    }
}