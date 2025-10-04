using CatsForAdoption.Domain.Entities;

namespace CatsForAdoption.Domain.UnitTests.Entities;

public class AdoptionCenterTests
{
    private AdoptionCenter CreateTestCenterWithCats()
    {
        var center = new AdoptionCenter("Leiria", "Rua dos Gatos Felizes, 123");

        // Gato disponível 1
        var garfield = new Cat("Garfield", "Persa", new DateOnly(2022, 1, 1), center.Id);

        // Gato disponível 2
        var felix = new Cat("Félix", "Siamês", new DateOnly(2023, 5, 10), center.Id);

        // Gato já adotado
        var tom = new Cat("Tom", "Rafeiro", new DateOnly(2021, 3, 15), center.Id);
        tom.MarkAsAdopted(); // Mudamos o estado deste gato

        center.AddCat(garfield);
        center.AddCat(felix);
        center.AddCat(tom);

        return center;
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void Constructor_WithValidData_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var city = "Lisboa";
        var address = "Avenida da Liberdade, 1";

        // Act
        var center = new AdoptionCenter(city, address);

        // Assert
        Assert.Equal(city, center.City);
        Assert.Equal(address, center.Address);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void AddCat_ShouldAddCatToTheAllCatsCollection()
    {
        // Arrange
        var center = new AdoptionCenter("Porto", "Rua de Santa Catarina, 456");
        var newCat = new Cat("Botas", "Europeu Comum", new DateOnly(2024, 1, 1), center.Id);
        var initialCount = center.Cats.Count;

        // Act
        center.AddCat(newCat);

        // Assert
        Assert.Equal(initialCount + 1, center.Cats.Count);
        Assert.Contains(newCat, center.Cats);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void AvailableCats_ShouldOnlyReturnCatsWhereIsAdoptedIsFalse()
    {        
        var center = CreateTestCenterWithCats();

        // Act
        var availableCats = center.AvailableCats;

        // Assert
        // Verificamos se a contagem de gatos disponíveis está correta
        Assert.Equal(2, availableCats.Count());
        // Verificamos se TODOS os gatos nesta lista têm, de facto, a propriedade IsAdopted a false
        Assert.All(availableCats, cat => Assert.False(cat.IsAdopted));
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void AdoptedCats_ShouldOnlyReturnCatsWhereIsAdoptedIsTrue()
    {
        // Arrange
        // Usamos o mesmo cenário do teste anterior
        var center = CreateTestCenterWithCats();

        // Act
        var adoptedCats = center.AdoptedCats;

        // Assert
        // Verificamos se a contagem de gatos adotados está correta
        Assert.Single(adoptedCats); // Uma forma mais expressiva de dizer Assert.Equal(1, ...)
        // Verificamos se TODOS os gatos nesta lista têm, de facto, a propriedade IsAdopted a true
        Assert.All(adoptedCats, cat => Assert.True(cat.IsAdopted));
    }
}
