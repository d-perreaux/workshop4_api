using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Mappers;
using api.Models;
using api.Dtos.Product;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public ProductsController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(){
            // ToList permet d'éxécuter la requête SQL? Comment avec les stream en JAVA? Vérifier la doc!!
            var products = await _context.Product.ToListAsync();

            // Select(x => x*2) est le map de JS
            var productsDto = products.Select(p => p.ToProductDto());

            return Ok(productsDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id){
            var product = await _context.Product.FindAsync(id);

            if (product == null){
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductRequestDto productDto){
            var productModel = productDto.ToProductFromCreateDto();
            await _context.Product.AddAsync(productModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = productModel.ProductId }, productModel.ToProductDto());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateProductRequestDto updateDto){
            var productModel = await _context.Product.FirstOrDefaultAsync(x => x.ProductId == id);

            if(productModel == null){
                return NotFound();
            }

            productModel.Name = updateDto.Name;
            productModel.CIP = updateDto.CIP;
            productModel.DCI = updateDto.DCI;
            productModel.Dosage = updateDto.Dosage;
            productModel.FlagIsDelete = updateDto.FlagIsDelete;

            await _context.SaveChangesAsync();

            return Ok(productModel.ToProductDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id){
            var productModel = await _context.Product.FirstOrDefaultAsync(x => x.ProductId == id);

            if(productModel == null){
                return NotFound();
            }

            // Pas mettre de await ici car delete ne fait pas de async
            _context.Product.Remove(productModel);
            _context.SaveChanges();

            return NoContent();
        }

        // GET: api/products/{id}/advices
        [HttpGet("{id}/advices")]
        public async Task<IActionResult> GetAdvicesByProductId([FromRoute] int id)
        {
            var product = await _context.Product
                .Include(p => p.ProductAdvices)
                    .ThenInclude(pa => pa.Advice)
                .FirstOrDefaultAsync(p => p.ProductId == id && !p.FlagIsDelete);

            if (product == null)
            {
                return NotFound();
            }

            var advices = product.ProductAdvices.Select(pa => pa.Advice.ToAdviceDto());

            return Ok(advices);
        }

        // POST: api/products/{productId}/advices/{adviceId}
        [HttpPost("{productId}/advices/{adviceId}")]
        public async Task<IActionResult> AddAdviceToProduct([FromRoute] int productId, [FromRoute] int adviceId)
        {
            var product = await _context.Product.FindAsync(productId);
            var advice = await _context.Advice.FindAsync(adviceId);

            if (product == null || advice == null)
            {
                return NotFound();
            }

            // Vérifier si l'association existe déjà
            var existingAssociation = await _context.ProductAdvice
                .FirstOrDefaultAsync(pa => pa.ProductId == productId && pa.AdviceId == adviceId);

            if (existingAssociation != null)
            {
                return BadRequest("L'association existe déjà.");
            }

            var productAdvice = new ProductAdvice
            {
                ProductId = productId,
                AdviceId = adviceId
            };

            await _context.ProductAdvice.AddAsync(productAdvice);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/products/{productId}/advices/{adviceId}
        [HttpDelete("{productId}/advices/{adviceId}")]
        public async Task<IActionResult> RemoveAdviceFromProduct([FromRoute] int productId, [FromRoute] int adviceId)
        {
            var productAdvice = await _context.ProductAdvice
                .FirstOrDefaultAsync(pa => pa.ProductId == productId && pa.AdviceId == adviceId);

            if (productAdvice == null)
            {
                return NotFound();
            }

            _context.ProductAdvice.Remove(productAdvice);
            await _context.SaveChangesAsync();

            return NoContent();
        }




    }
}