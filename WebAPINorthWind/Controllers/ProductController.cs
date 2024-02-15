using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPINorthWind.Data;
using WebAPINorthWind.Models;

namespace WebAPINorthWind.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly DataContext _context;

        public ProductController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProduct(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product is null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] int? categoryId = null)
        {
            
            pageNumber = Math.Max(1, pageNumber);
            pageSize = Math.Max(1, pageSize);
            
            var query = _context.Products.AsQueryable();

            if (categoryId.HasValue)
                query = query.Where(p => p.CategoryId == categoryId.Value);
            
            var itemsToSkip = (pageNumber - 1) * pageSize;
                        
            var products = await query
                .Skip(itemsToSkip)
                .Take(pageSize)
                .ToListAsync();

            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return Ok(await _context.Products.FindAsync());
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(Product product)
        {
            var dbProduct = await _context.Products.FindAsync(product.ProductId);

            if (dbProduct is null)
                return NotFound();

            dbProduct.ProductId = product.ProductId;
            dbProduct.ProductName = product.ProductName;
            dbProduct.CategoryId = product.CategoryId;
            dbProduct.Category = product.Category;
            dbProduct.Discontinued = product.Discontinued;
            dbProduct.OrderDetails = product.OrderDetails;
            dbProduct.QuantityPerUnit = product.QuantityPerUnit;
            dbProduct.ReorderLevel = product.ReorderLevel;
            dbProduct.Supplier = product.Supplier;
            dbProduct.SupplierId = product.SupplierId;
            dbProduct.UnitPrice = product.UnitPrice;
            

            await _context.SaveChangesAsync();

            return Ok(await _context.Products.FindAsync());

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var product = await _context.Products.FindAsync(productId);

            if (product is null)
                return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return Ok(await _context.Products.FindAsync());

        }
    }
}
