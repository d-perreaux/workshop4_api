using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Data;
using api.Models;
using api.Dtos.Pharmacy;
using api.Mappers;

namespace api.Controllers
{
    [ApiController]
    [Route("api/pharmacies")]
    public class PharmacyController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public PharmacyController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pharmacies = await _context.Pharmacy.ToListAsync();
            var pharmacyDtos = pharmacies.Select(p => p.ToPharmacyDto());
            return Ok(pharmacyDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var pharmacy = await _context.Pharmacy.FindAsync(id);

            if (pharmacy == null)
            {
                return NotFound();
            }

            return Ok(pharmacy.ToPharmacyDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePharmacyRequestDto pharmacyDto)
        {
            var pharmacyModel = pharmacyDto.ToPharmacyFromCreateDto();
            await _context.Pharmacy.AddAsync(pharmacyModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = pharmacyModel.PharmacyId }, pharmacyModel.ToPharmacyDto());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdatePharmacyRequestDto updateDto){
            var pharmacyModel = await _context.Pharmacy.FirstOrDefaultAsync(x => x.PharmacyId == id);

            if(pharmacyModel == null){
                return NotFound();
            }

            pharmacyModel.Name = updateDto.Name;

            await _context.SaveChangesAsync();

            return Ok(pharmacyModel.ToPharmacyDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var pharmacyModel = await _context.Pharmacy.FirstOrDefaultAsync(x => x.PharmacyId == id);

            if (pharmacyModel == null)
            {
                return NotFound();
            }

            _context.Pharmacy.Remove(pharmacyModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
