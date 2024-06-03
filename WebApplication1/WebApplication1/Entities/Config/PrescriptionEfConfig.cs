using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApplication1.Entities.Config
{
    public class PrescriptionEfConfig : IEntityTypeConfiguration<Prescription>
    {
        public void Configure(EntityTypeBuilder<Prescription> builder)
        {
            builder.HasKey(p => p.IdPrescription)
                .HasName("Prescription_pk");

            builder.Property(p => p.IdPrescription)
                .ValueGeneratedNever();

            builder.Property(p => p.Date)
                .IsRequired();

            builder.Property(p => p.DueDate)
                .IsRequired();

            builder.HasOne(p => p.Patient)
                .WithMany(pat => pat.Prescriptions)
                .HasForeignKey(p => p.IdPatient)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Doctor)
                .WithMany(doc => doc.Prescriptions)
                .HasForeignKey(p => p.IdDoctor)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable(nameof(Prescription));
        }
    }
}