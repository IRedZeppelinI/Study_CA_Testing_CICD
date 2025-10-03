namespace CatsForAdoption.Domain.Entities;

public class Cat
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Breed { get; set; } = string.Empty;    
    public bool Adopted { get; set; }
    public DateOnly BirthDate { get; set; }
    public int Age 
    {
        get 
        {
            var today = DateOnly.FromDateTime(DateTime.Now);
            var age = today.Year - BirthDate.Year;
            if (BirthDate > today.AddYears(-age))
            {
                age--;
            }
            return age;
        }        
    }
}
