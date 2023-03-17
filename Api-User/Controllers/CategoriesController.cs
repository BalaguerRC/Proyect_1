using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api_User.Data;
using Api_User.Models;
using Microsoft.AspNetCore.Authorization;
using System.Configuration;

namespace Api_User.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly Api_UserContext _context;
        private IConfiguration Configuration;

        public CategoriesController(Api_UserContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
          if (_context.Categories == null)
          {
              return NotFound();
          }
            return await _context.Categories.ToListAsync();
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(long id)
        {
          if (_context.Categories == null)
          {
              return NotFound();
          }
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

                //[HttpGet]
        
        /**[HttpGet("{id}")]
        [Route("getProduct")]
        public async Task<ActionResult<Category>> GetProduct(long id)
        {
            if (_context.Categories == null)
            {
                return NotFound();
            }
            /*try
            {
                
                /*dynamic pro = CategoryData.GetProductByID(id, Configuration.GetConnectionString("Api_UserContext"));
                if (pro != null)
                {
                    return pro.Data;
                }
                else
                {
                    return pro.message;
                }
            }
            catch (Exception)
            {
                return NotFound();
            }
            var get = await _context.Categories.FindAsync(id);

            return get;
        }*/
       /* public dynamic GetProductByCategoryId([FromBody]long id)
        {
            return CategoryData.GetProductByID(id, Configuration.GetConnectionString("Api_UserContext"));
        }*/
        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(long id, Category category)
        {
            category.Id=id;
            if (id != category.Id)
            {
                return BadRequest();
            }

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(new
            {
                succes = true
            });
        }

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
          if (_context.Categories == null)
          {
              return Problem("Entity set 'Api_UserContext.Categories'  is null.");
          }
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { id = category.Id }, category);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(long id)
        {
            if (_context.Categories == null)
            {
                return NotFound();
            }
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                succes=true
            });
        }

        private bool CategoryExists(long id)
        {
            return (_context.Categories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
