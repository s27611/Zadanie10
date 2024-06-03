using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Entities.Model;

namespace WebApplication1.Entities.Controler;

public class PrescriptionsController : ControllerBase
{
    private readonly HospitalDbContext _context;

    public PrescriptionsController(HospitalDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> AddPrescription([FromBody] PrescriptionRequest request)
    {
        if (request.Medicaments.Count > 10)
        {
            return BadRequest("Prescription cannot have more than 10 medicaments.");
        }

        if (request.DueDate < request.Date)
        {
            return BadRequest("DueDate cannot be earlier than Date.");
        }

        var patient = await _context.Patients.FindAsync(request.Patient.IdPatient);
        if (patient == null)
        {
            patient = new Patient
            {
                FirstName = request.Patient.FirstName,
                LastName = request.Patient.LastName,
                Birthdate = request.Patient.Birthdate
            };
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
        }

        var doctor = await _context.Doctors.FindAsync(request.IdDoctor);
        if (doctor == null)
        {
            return NotFound("Doctor not found.");
        }

        var prescription = new Prescription
        {
            Date = request.Date,
            DueDate = request.DueDate,
            IdPatient = patient.IdPatient,
            IdDoctor = doctor.IdDoctor,
            PrescriptionMedicaments = new List<PrescriptionMedicament>()
        };

        foreach (var med in request.Medicaments)
        {
            var medicament = await _context.Medicaments.FindAsync(med.IdMedicament);
            if (medicament == null)
            {
                return NotFound($"Medicament with ID {med.IdMedicament} not found.");
            }

            prescription.PrescriptionMedicaments.Add(new PrescriptionMedicament
            {
                IdMedicament = medicament.IdMedicament,
                Dose = med.Dose,
                Details = med.Details
            });
        }

        _context.Prescriptions.Add(prescription);
        await _context.SaveChangesAsync();

        return Ok(prescription);
    }
}