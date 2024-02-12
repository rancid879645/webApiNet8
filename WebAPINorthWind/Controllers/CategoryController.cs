using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPINorthWind.Data;
using WebAPINorthWind.Models;

namespace WebAPINorthWind.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly DataContext _context;

        public CategoryController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _context.Categories.ToListAsync();
            return Ok(categories);
        }

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetCategory(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            if (category is null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return Ok(await GetAllCategories());
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory(Category category)
        {
            var dbCategory = await _context.Categories.FindAsync(category.CategoryId);

            if (dbCategory is null)
                return NotFound();

            dbCategory.CategoryId = category.CategoryId;
            dbCategory.CategoryName = category.CategoryName;
            dbCategory.Description = category.Description;
            dbCategory.Picture = category.Picture;
            dbCategory.Products = category.Products;

            await _context.SaveChangesAsync();

            return Ok(await GetAllCategories());

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            
            if (category is null)
                return NotFound();

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return Ok(await GetAllCategories());

        }
    }
}
