using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Data;
using api.Models;
using api.Dtos.Prescription;
using api.Mappers;

namespace api.Controllers
{
    [ApiController]
    [Route("api/prescriptions")]
    public class PrescriptionController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public PrescriptionController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/prescriptions
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var prescriptions = await _context.Prescription
                .Include(p => p.Pharmacy)
                .ToListAsync();
            var prescriptionDtos = prescriptions.Select(p => p.ToPrescriptionDto());
            return Ok(prescriptionDtos);
        }

        // GET: api/prescriptions/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var prescription = await _context.Prescription
                .Include(p => p.Pharmacy)
                .FirstOrDefaultAsync(p => p.PrescriptionId == id);

            if (prescription == null)
            {
                return NotFound();
            }

            return Ok(prescription.ToPrescriptionDto());
        }

        // POST: api/prescriptions
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePrescriptionRequestDto prescriptionDto)
        {
            // Vérifier si la pharmacie existe
            var pharmacyExists = await _context.Pharmacy.AnyAsync(p => p.PharmacyId == prescriptionDto.PharmacyId);
            if (!pharmacyExists)
            {
                return NotFound($"La pharmacie avec l'ID {prescriptionDto.PharmacyId} n'existe pas.");
            }

            var prescriptionModel = prescriptionDto.ToPrescriptionFromCreateDto();
            await _context.Prescription.AddAsync(prescriptionModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = prescriptionModel.PrescriptionId }, prescriptionModel.ToPrescriptionDto());
        }

        // PUT: api/prescriptions/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdatePrescriptionRequestDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var prescriptionModel = await _context.Prescription.FirstOrDefaultAsync(p => p.PrescriptionId == id);

            if (prescriptionModel == null)
            {
                return NotFound();
            }

            // Vérifier si la nouvelle pharmacie existe
            var pharmacyExists = await _context.Pharmacy.AnyAsync(p => p.PharmacyId == updateDto.PharmacyId);
            if (!pharmacyExists)
            {
                return NotFound($"La pharmacie avec l'ID {updateDto.PharmacyId} n'existe pas.");
            }

            prescriptionModel.UpdateFromDto(updateDto);
            await _context.SaveChangesAsync();

            return Ok(prescriptionModel.ToPrescriptionDto());
        }

        // DELETE: api/prescriptions/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var prescriptionModel = await _context.Prescription.FirstOrDefaultAsync(p => p.PrescriptionId == id);

            if (prescriptionModel == null)
            {
                return NotFound();
            }

            _context.Prescription.Remove(prescriptionModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
