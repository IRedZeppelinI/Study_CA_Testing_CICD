// CatsForAdoption.Infrastructure/Persistence/Configurations/CatConfiguration.cs

using CatsForAdoption.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatsForAdoption.Infrastructure.Persistence.Configurations;

public class CatConfiguration : IEntityTypeConfiguration<Cat>
{
    public void Configure(EntityTypeBuilder<Cat> builder)
    {
        builder.ToTable("Cats");

        builder.HasKey(cat => cat.Id);

        // Configuração das propriedades
        builder.Property(cat => cat.Name)
            .IsRequired()
            .HasMaxLength(100)
            .HasDefaultValue("Unnamed");

        builder.Property(cat => cat.Description)
            .HasMaxLength(500); // Descrições podem ser maiores

        builder.Property(cat => cat.Breed)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(cat => cat.BirthDate).IsRequired();

        builder.Property(cat => cat.IsAdopted)
            .IsRequired()
            .HasDefaultValue(false);

        // Dizer ao EF Core para ignorar a propriedade 'Age'
        builder.Ignore(cat => cat.Age);

        
    }
}