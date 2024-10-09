using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Data;
using api.Models;
using api.Dtos.Prescription;
using api.Dtos.Product;
using api.Dtos.Advice;
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

        // GET: api/prescriptions/{id}/products
        [HttpGet("{id}/products")]
        public async Task<IActionResult> GetProductsByPrescriptionId([FromRoute] int id)
        {
            var prescription = await _context.Prescription
                .Include(p => p.PrescriptionProducts)
                    .ThenInclude(pp => pp.Product)
                .FirstOrDefaultAsync(p => p.PrescriptionId == id);

            if (prescription == null)
            {
                return NotFound();
            }

            var products = prescription.PrescriptionProducts.Select(pp => pp.Product.ToProductDto());

            return Ok(products);
        }

        // GET: api/prescriptions/{id}/advices
        [HttpGet("{id}/advices")]
        public async Task<IActionResult> GetAdvicesByPrescriptionId([FromRoute] int id)
        {
            var prescription = await _context.Prescription
                .Include(p => p.PrescriptionProducts)
                    .ThenInclude(pp => pp.PrescriptionProductAdvices)
                        .ThenInclude(ppa => ppa.Advice)
                .FirstOrDefaultAsync(p => p.PrescriptionId == id);

            if (prescription == null)
            {
                return NotFound();
            }

            var advices = prescription.PrescriptionProducts
                .SelectMany(pp => pp.PrescriptionProductAdvices)
                .Select(ppa => ppa.Advice.ToAdviceDto())
                .Distinct();

            return Ok(advices);
        }

        // GET: api/prescriptions/{id}/details
        [HttpGet("{id}/details")]
        public async Task<IActionResult> GetPrescriptionDetails([FromRoute] int id)
        {
            var prescription = await _context.Prescription
                .AsNoTracking()
                .Include(p => p.PrescriptionProducts)
                    .ThenInclude(pp => pp.Product)
                .Include(p => p.PrescriptionProducts)
                    .ThenInclude(pp => pp.PrescriptionProductAdvices)
                        .ThenInclude(ppa => ppa.Advice)
                .FirstOrDefaultAsync(p => p.PrescriptionId == id);

            if (prescription == null)
            {
                return NotFound();
            }

            var prescriptionDetailsDto = new PrescriptionDetailsDto
            {
                PrescriptionId = prescription.PrescriptionId,
                PharmacyId = prescription.PharmacyId,
                Date = prescription.Date,
                Products = prescription.PrescriptionProducts.Select(pp => new ProductWithAdvicesDto
                {
                    ProductId = pp.ProductId,
                    Name = pp.Product.Name,
                    // Ajoutez d'autres propriétés du produit si nécessaire
                    Advices = pp.PrescriptionProductAdvices.Select(ppa => new AdviceDto
                    {
                        AdviceId = ppa.AdviceId,
                        Content = ppa.Advice.Content
                        // Ajoutez d'autres propriétés du conseil si nécessaire
                    }).ToList()
                }).ToList()
            };

            return Ok(prescriptionDetailsDto);
        }



    }
}
