using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Mappers;
using api.Models;
using api.Dtos.Product;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetAll(){
            // ToList permet d'éxécuter la requête SQL? Comment avec les stream en JAVA? Vérifier la doc!!
            // var products = _context.Product.ToList();

            // Select(x => x*2) est le map de JS
            var products = _context.Product.Select(p => p.ToProductDto()).ToList();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id){
            var product = _context.Product.Find(id);

            if (product == null){
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateProductRequestDto productDto){
            var productModel = productDto.ToProductFromCreateDto();
            _context.Product.Add(productModel);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = productModel.ProductId }, productModel.ToProductDto());
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateProductRequestDto updateDto){
            var productModel = _context.Product.FirstOrDefault(x => x.ProductId == id);

            if(productModel == null){
                return NotFound();
            }

            productModel.Name = updateDto.Name;
            productModel.CIP = updateDto.CIP;

            _context.SaveChanges();

            return Ok(productModel.ToProductDto());
        }
    }
}