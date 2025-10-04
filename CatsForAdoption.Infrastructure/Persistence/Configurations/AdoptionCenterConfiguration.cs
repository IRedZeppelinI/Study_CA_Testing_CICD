// CatsForAdoption.Infrastructure/Persistence/Configurations/AdoptionCenterConfiguration.cs

using CatsForAdoption.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatsForAdoption.Infrastructure.Persistence.Configurations;

public class AdoptionCenterConfiguration : IEntityTypeConfiguration<AdoptionCenter>
{
    public void Configure(EntityTypeBuilder<AdoptionCenter> builder)
    {
        builder.ToTable("AdoptionCenters");

        builder.HasKey(ac => ac.Id);

        builder.Property(ac => ac.City)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(ac => ac.Address)
            .IsRequired()
            .HasMaxLength(250);

        builder.Ignore(adoptionCenter => adoptionCenter.AvailableCats);
        builder.Ignore(adoptionCenter => adoptionCenter.AdoptedCats);


        builder
            .HasMany(ac => ac.Cats) // Usa a propriedade pública
            .WithOne(c => c.AdoptionCenter) // A inversa continua a ser fortemente tipada
            .HasForeignKey(c => c.AdoptionCenterId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
