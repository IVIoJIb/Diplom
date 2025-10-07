using Microsoft.AspNetCore.Mvc;
using PharmExpertAPI.Data;
using PharmExpertAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace PharmExpertAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CartController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout([FromBody] List<OrderItemDto> cartItems)
        {
            foreach (var item in cartItems)
            {
                var product = await _context.Products.FindAsync(item.ProductId);
                if (product == null)
                    return BadRequest($"Товар з ID {item.ProductId} не знайдено.");

                if (product.Quantity < item.Quantity)
                    return BadRequest($"Недостатньо товару '{product.Name}'. В наявності: {product.Quantity}, ви хочете купити: {item.Quantity}.");

                product.Quantity -= item.Quantity;
            }

            await _context.SaveChangesAsync();
            return Ok("Замовлення успішно оформлене!");
        }
    }

    public class OrderItemDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
