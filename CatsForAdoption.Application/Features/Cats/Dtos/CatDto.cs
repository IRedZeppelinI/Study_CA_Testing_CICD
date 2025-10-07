namespace CatsForAdoption.Application.Features.Cats.Dtos;

public class CatDto
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string? Description { get; init; }
    public string Breed { get; init ; }
    public bool IsAdopted { get;    init; }
    public DateOnly BirthDate { get; init; }
    public int Age { get; init; }
}
