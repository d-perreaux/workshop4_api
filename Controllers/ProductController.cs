using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Mappers;
using api.Models;
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
    }
}