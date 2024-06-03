using Microsoft.EntityFrameworkCore;
using WebApplication1.Entities.Config;

namespace WebApplication1.Entities;

public class HospitalDbContext: DbContext
{
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }

    public HospitalDbContext(DbContextOptions<HospitalDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new DoctorEfConfig());
        modelBuilder.ApplyConfiguration(new MedicamentEfConfig());
        modelBuilder.ApplyConfiguration(new PatientEfConfig());
        modelBuilder.ApplyConfiguration(new PrescriptionEfConfig());
        modelBuilder.ApplyConfiguration(new PrescriptionMedicamentEfConfig());
    }
}