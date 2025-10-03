using CatsForAdoption.Domain.Entities;

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
        var cat = new Cat
        {
            BirthDate = new DateOnly(birthYear, birthMonth, birthDay)
        };

        // --- ACT ---

        // Executamos a lógica, passando a nossa data controlada.
        var calculatedAge = cat.CalculateAge(controlledDateForTests);

        // --- ASSERT ---

        // Verificamos se o resultado calculado é igual ao resultado que definimos no InlineData.
        // Na Execução 1, isto será: Assert.Equal(25, calculatedAge);
        // Na Execução 3, isto será: Assert.Equal(24, calculatedAge);
        Assert.Equal(expectedAge, calculatedAge);
    }
}
