namespace CatsForAdoption.Domain.Entities;

public class AdoptionCenter
{
    public int Id { get; private set; }
    public string City { get; private set; }
    public string Address { get; private set; }

    public ICollection<Cat> Cats { get; private set; } = new List<Cat>();

    // ...e expomos uma coleção apenas de leitura.

    public IEnumerable<Cat> AvailableCats => Cats.Where(c => !c.IsAdopted);
    public IEnumerable<Cat> AdoptedCats => Cats.Where(c => c.IsAdopted);

    private AdoptionCenter()
    {
        City = string.Empty;
        Address = string.Empty;
    }

    public AdoptionCenter(string city, string address)
    {
        City = city;
        Address = address;
    }
        
    public void AddCat(Cat cat)
    {        
        Cats.Add(cat);
    }
}