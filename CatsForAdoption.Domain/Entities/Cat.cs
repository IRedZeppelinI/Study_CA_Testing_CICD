// CatsForAdoption.Domain/Entities/Cat.cs

namespace CatsForAdoption.Domain.Entities;

public class Cat
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public string Breed { get; private set; }
    public bool IsAdopted { get; private set; }
    public DateOnly BirthDate { get; private set; }

    public int AdoptionCenterId { get; private set; }
    public AdoptionCenter? AdoptionCenter { get; private set; }

    // Construtor para garantir que um gato é criado num estado válido.
    // O construtor vazio é para o EF Core.
    private Cat()
    {
        Name = string.Empty;
        Breed = string.Empty;
    }

    public Cat(string? name, string breed, DateOnly birthDate, int adoptionCenterId)
    {
        // Aqui poderia adicionar validações (ex: nome não pode ser vazio)
        Name = string.IsNullOrWhiteSpace(name) ? "Unnamed" : name;
        Breed = breed;
        BirthDate = birthDate;
        IsAdopted = false; // Um gato começa sempre por não estar adotado
        AdoptionCenterId = adoptionCenterId;
    }

    // A idade é calculada em tempo de execução, não mapeada para o DB.
    public int Age => GetAgeAsOf(DateOnly.FromDateTime(DateTime.Now));

    public int GetAgeAsOf(DateOnly today)
    {
        var age = today.Year - BirthDate.Year;
        if (BirthDate > today.AddYears(-age))
        {
            age--;
        }
        return age;
    }

    // Método para marcar como adotado (Exemplo de comportamento)
    public void MarkAsAdopted()
    {
        IsAdopted = true;
    }
}