using BikestoreApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikestoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductContext _context;

        public ProductsController(ProductContext context)
        {
            _context = context;

            if (_context.Products.Count() == 0)
            {
                _context.Products.Add(new Product { name = "Rower 1" });
                _context.SaveChanges();
            }
        }
        [HttpGet]
        public IEnumerable<Product> GetProducts()
        {
            return _context.Products.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(int id)
        {
            var Product = _context.Products.Find(id);

            if (Product == null)
            {
                return NotFound();
            }
            else
            {
                return Product;
            }
        }
        [HttpPost]
        public ActionResult<Product> PostProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetProduct), new { id = product.id }, product);
        }
        [HttpPut("{id}")]
        public ActionResult<Product> PutProduct(int id, Product product)
        {
            if (id != product.id)
            {
                return BadRequest();
            }

                _context.Products.Update(product);
                _context.SaveChanges();

            return NoContent();
        }
    }
}
