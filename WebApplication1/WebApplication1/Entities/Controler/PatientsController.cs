using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Entities;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly HospitalDbContext _context;

        public PatientsController(HospitalDbContext context)
        {
            _context = context;
        }

        // GET: api/Patients/1
        [HttpGet("{id}")]
        public async Task<ActionResult<PatientDetailsDto>> GetPatient(int id)
        {
            var patient = await _context.Patients
                .Include(p => p.Prescriptions)
                    .ThenInclude(pres => pres.Doctor)
                .Include(p => p.Prescriptions)
                    .ThenInclude(pres => pres.PrescriptionMedicaments)
                        .ThenInclude(pm => pm.Medicament)
                .Where(p => p.IdPatient == id)
                .Select(p => new PatientDetailsDto
                {
                    IdPatient = p.IdPatient,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Birthdate = p.Birthdate,
                    Prescriptions = p.Prescriptions
                        .OrderBy(pres => pres.DueDate)
                        .Select(pres => new PrescriptionDetailsDto
                        {
                            IdPrescription = pres.IdPrescription,
                            Date = pres.Date,
                            DueDate = pres.DueDate,
                            Doctor = new DoctorDto
                            {
                                IdDoctor = pres.Doctor.IdDoctor,
                                FirstName = pres.Doctor.FirstName,
                                LastName = pres.Doctor.LastName,
                                Email = pres.Doctor.Email
                            },
                            Medicaments = pres.PrescriptionMedicaments.Select(pm => new MedicamentDto
                            {
                                IdMedicament = pm.IdMedicament,
                                Name = pm.Medicament.Name,
                                Dose = pm.Dose,
                                Description = pm.Medicament.Description,
                                Type = pm.Medicament.Type
                            }).ToList()
                        }).ToList()
                })
                .FirstOrDefaultAsync();

            if (patient == null)
            {
                return NotFound();
            }

            return patient;
        }
    }
}
