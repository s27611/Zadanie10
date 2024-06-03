using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace WebApplication1.Entities.Config;

public class DoctorEfConfig: IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.HasKey(d => d.IdDoctor)
            .HasName("Doctor_pk");

        builder.Property(d => d.IdDoctor).ValueGeneratedNever();

        builder.Property(d => d.FirstName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(d => d.LastName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(d => d.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder.ToTable(nameof(Doctor));
    }
}