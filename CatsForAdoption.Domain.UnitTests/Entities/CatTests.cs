using CatsForAdoption.Domain.Entities;
using System.Xml.Linq;

namespace CatsForAdoption.Domain.UnitTests.Entities;

public class CatTests
{

    [Theory] // Diz ao xUnit: "Prepara-te para executar isto várias vezes"
    [Trait("Category", "Unit")]
    // ---------------------------------------------------------------------------------------------------
    // CADA LINHA É UMA EXECUÇÃO DO TESTE, COM 4 ARGUMENTOS:
    //                                            (ano,   mes,  dia, resultado_esperado)
    [InlineData(2000, 1, 15, 25)] // Execução 1: idadeEsperada = 25
    [InlineData(2000, 10, 3, 25)] // Execução 2: idadeEsperada = 25
    [InlineData(2000, 12, 25, 24)] // Execução 3: idadeEsperada = 24
    [InlineData(1985, 5, 20, 40)] // Execução 4: idadeEsperada = 40
                                  // ---------------------------------------------------------------------------------------------------
    public void CalculateAge_ForDiferentDates_MustReturnCorrectAge(
    int birthYear, int birthMonth, int birthDay, int expectedAge) // <-- Os argumentos são recebidos aqui
    {
        // --- ARRANGE ---

        // Definimos o nosso "hoje" fixo para a experiência.
        var controlledDateForTests = new DateOnly(2025, 10, 3);

        // Criamos a pessoa com a data de nascimento deste cenário.
        
        var cat = new Cat(
            name: "Teste",
            breed: "testeBreed",
            birthDate: new DateOnly(birthYear, birthMonth, birthDay),
            adoptionCenterId: 2);        
            

        // --- ACT ---

        // Executamos a lógica, passando a nossa data controlada.        
        var calculatedAge = cat.GetAgeAsOf(controlledDateForTests);

        // --- ASSERT ---

        // Verificamos se o resultado calculado é igual ao resultado que definimos no InlineData.
        // Na Execução 1, isto será: Assert.Equal(25, calculatedAge);
        // Na Execução 3, isto será: Assert.Equal(24, calculatedAge);
        Assert.Equal(expectedAge, calculatedAge);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void CatIsAdoptedStartsAsFalse_AndWhenAdopted_IsAdoptedTurnsTrue()
    {
        var cat = new Cat(
            name: "Teste",
            breed: "testeBreed",
            birthDate: new DateOnly(2020, 10, 05),
            adoptionCenterId: 2);


        Assert.False(cat.IsAdopted);
        cat.MarkAsAdopted();
        Assert.True(cat.IsAdopted);
    }
}
