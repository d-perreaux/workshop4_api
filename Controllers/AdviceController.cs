using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Data;
using api.Models;
using api.Dtos.Advice;
using api.Mappers;

namespace api.Controllers
{
    [ApiController]
    [Route("api/advices")]
    public class AdviceController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public AdviceController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var advices = await _context.Advice
                .Where(a => !a.FlagIsDeleted)
                .ToListAsync();
            var adviceDtos = advices.Select(a => a.ToAdviceDto());
            return Ok(adviceDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var advice = await _context.Advice
                .FirstOrDefaultAsync(a => a.AdviceId == id && !a.FlagIsDeleted);

            if (advice == null)
            {
                return NotFound();
            }

            return Ok(advice.ToAdviceDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAdviceRequestDto adviceDto)
        {
            var adviceModel = adviceDto.ToAdviceFromCreateDto();
            await _context.Advice.AddAsync(adviceModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = adviceModel.AdviceId }, adviceModel.ToAdviceDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateAdviceRequestDto updateDto)
        {
            var adviceModel = await _context.Advice.FirstOrDefaultAsync(a => a.AdviceId == id);

            if (adviceModel == null)
            {
                return NotFound();
            }

            adviceModel.UpdateFromDto(updateDto);
            await _context.SaveChangesAsync();

            return Ok(adviceModel.ToAdviceDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var adviceModel = await _context.Advice.FirstOrDefaultAsync(a => a.AdviceId == id);

            if (adviceModel == null)
            {
                return NotFound();
            }

            // Suppression logique et non physique
            adviceModel.FlagIsDeleted = true;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
