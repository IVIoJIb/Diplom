using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmExpertAPI.Data;
using PharmExpertAPI.Models;

namespace PharmExpertAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        // Отримати всі препарати
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _context.Products.ToListAsync();
            return Ok(products);
        }

        // Отримати препарат за id 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        // Додати новий препарат
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }

        // Оновити препарат
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product updatedProduct)
        {
            if (id != updatedProduct.Id)
                return BadRequest("ID не співпадають");

            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            product.Name = updatedProduct.Name;
            product.SerialNumber = updatedProduct.SerialNumber;
            product.Manufacturer = updatedProduct.Manufacturer;
            product.Dosage = updatedProduct.Dosage;
            product.ExpirationDate = updatedProduct.ExpirationDate;
            product.Quantity = updatedProduct.Quantity;
            product.Price = updatedProduct.Price;
            product.ActiveSubstance = updatedProduct.ActiveSubstance;
            product.RequiresPrescription = updatedProduct.RequiresPrescription;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Видалити препарат
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Препарати з терміном придатності менше ніж 30 днів
        [HttpGet("expiring")]
        public async Task<IActionResult> GetExpiringProducts()
        {
            var today = DateTime.Today;
            var products = await _context.Products
                .Where(p => p.ExpirationDate != null && p.ExpirationDate <= today.AddDays(30))
                .ToListAsync();

            return Ok(products);
        }
    }
}
