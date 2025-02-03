using BlogApi.Data;
using BlogApi.Extensions;
using BlogApi.Models;
using BlogApi.ViewModels;
using BlogApi.ViewModels.Categories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace BlogApi.Controllers
{
    [ApiController]
    public class CategoryController : ControllerBase
    {

        [HttpGet()] //[HttpGet("v1/categories")]
        [Route("v1/categories")]
        public IActionResult GetAsync(
            [FromServices] IMemoryCache cache,
            [FromServices] BlogDataContext context)
        {
            try
            {
                var categories = cache.GetOrCreate("CategoriesCache", entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
                    return GetCategories(context);
                });

                return Ok(new ResultViewModel<List<Category>>(categories));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<List<Category>>("01CGE Internal Error"));
            }
        }

        private List<Category> GetCategories(BlogDataContext context)
        {
            return context.Categories.ToList();
        }

        [HttpGet()] //[HttpGet("v1/categories")]
        [Route("v1/categories/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute] int id,
            [FromServices] BlogDataContext context)
        {
            try
            { 
                var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
                if (category == null) 
                    return NotFound(new ResultViewModel<Category>("01CGINF Not found"));

                return Ok(new ResultViewModel<Category>(category));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<List<Category>>("01CGIE Internal Error"));
            }
        }

        [HttpPost()] //[HttpGet("v1/categories")]
        [Route("v1/categories")]
        public async Task<IActionResult> PostAsync(
            [FromBody] EditorCategoryViewModel model,
            [FromServices] BlogDataContext context)
        {
            if (!ModelState.IsValid)
                //return BadRequest(ModelState.GetErrors());
                return BadRequest(new ResultViewModel<Category>(ModelState.GetErrors()));

            try
            {
                var category = new Category
                {
                    Id = 0,
                    Name = model.Name,
                    Slug = model.Slug.ToLower()
                };
                await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();

                return Created($"v1/categories/{category.Id}", new ResultViewModel<Category>(category));
            }
            catch (DbUpdateException e)
            {
                return StatusCode(500, new ResultViewModel<List<Category>>("01CPS DbError"));
            }
            catch 
            {
                return StatusCode(500, new ResultViewModel<List<Category>>("01CPSE Internal Error"));
            }
        }

        [HttpPut()] //[HttpGet("v1/categories")]
        [Route("v1/categories/{id:int}")]
        public async Task<IActionResult> PutAsync(
             [FromRoute] int id,
             [FromBody] EditorCategoryViewModel model,
             [FromServices] BlogDataContext context)
        {
            try
            {
                var category = await context
                .Categories.FirstOrDefaultAsync(x => x.Id == id);   
                if (category == null)
                    return NotFound(new ResultViewModel<List<Category>>("01CPTNF Not found"));

                category.Name = model.Name;
                category.Slug = model.Slug;

                context.Categories.Update(category);
                await context.SaveChangesAsync();

                return Created($"v1/categories/{category.Id}", category);
            }
            catch (DbUpdateException e)
            {
                return StatusCode(500, new ResultViewModel<List<Category>>("01CPTNF Not found"));
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResultViewModel<List<Category>>("01CPTE Internal Error"));
            }
        }

        [HttpDelete()] //[HttpGet("v1/categories")]
        [Route("v1/categories/{id:int}")]
        public async Task<IActionResult> DeleteAsync(
             [FromRoute] int id,
             [FromServices] BlogDataContext context)
        {
            try
            {
                var category = await context
                .Categories.FirstOrDefaultAsync(x => x.Id == id);
                if (category == null)
                    return NotFound(new ResultViewModel<List<Category>>("01CDNF Not found"));

                context.Categories.Remove(category);
                await context.SaveChangesAsync();

                return Created($"v1/categories/{category.Id}", category);
            }
            catch (DbUpdateException e)
            {
                return StatusCode(500, new ResultViewModel<List<Category>>("01CD Not found"));
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResultViewModel<List<Category>>("01CDE Internal Error"));
            }
        }
    }
}
