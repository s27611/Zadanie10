using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace WebApplication1.Entities.Config;

public class MedicamentEfConfig: IEntityTypeConfiguration<Medicament>
{
    public void Configure(EntityTypeBuilder<Medicament> builder)
    {
        builder.HasKey(m => m.IdMedicament)
            .HasName("Medicament_pk");

        builder.Property(m => m.IdMedicament).ValueGeneratedNever();

        builder.Property(m => m.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(m => m.Description)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(m => m.Type)
            .IsRequired()
            .HasMaxLength(100);

        builder.ToTable(nameof(Medicament));
    }
}