using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api_User.Data;
using Api_User.Models;
using System.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace Api_User.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly Api_UserContext _context;
        private IConfiguration Configuration;
        public ProductsController(Api_UserContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        // GET: api/Products
        /*[HttpGet]
        public async Task<ActionResult<IEnumerable<Products>>> GetProducts()
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            Products products = new Products();
            ProductsModel productsM= new ProductsModel();

            productsM.Id= products.Id;
            productsM.Name= products.Name;
            productsM.Description= products.Description;
            productsM.Precio= products.Precio;
            productsM.Author= products.Author;
            productsM.Date= products.Date;

            return await _context.Products.ToListAsync();
        }*/

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Products>> GetProducts(long id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }

            try
            {
                dynamic n = ProductsData.GetIDProduct(id, Configuration.GetConnectionString("Api_UserContext"));
                if (n.success == true)
                {
                    return Ok(n);
                }
                else
                {
                    return Ok(n.message);
                }

            }
            catch (Exception)
            {
                return NotFound();
            }
        }


        [HttpGet]
        //[Route("getproduct")]
        public List<Products> GetProducto()
        {
            //string connectionString = Configuration.GetConnectionString("Api_UserContext");
            return ProductsData.GetProducts(Configuration.GetConnectionString("Api_UserContext"));
        }
        [HttpPost]
        //[Route("getproduct")]
        public dynamic AddProduct([FromBody] Products products)
        {
            return ProductsData.AddProduct(products, Configuration.GetConnectionString("Api_UserContext"));
        }
        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducts(long id, Products products)
        {
            //if (id != products.Id)
            if (id == null)
            {
                return BadRequest();
            }

            //EdittProduct(products, id);
            Products p = new Products();
            p.Id = id;
            p.Name = products.Name;
            p.Description = products.Description;
            p.Precio = products.Precio;
            p.Author = products.Author;
            p.IDCategory = products.IDCategory;
            p.quantity = products.quantity;
            p.Image= products.Image;
            
            //_context.Entry(products).State = EntityState.Modified;

            try
            {
                //await _context.SaveChangesAsync();
                dynamic n=ProductsData.EditProduct(p, Configuration.GetConnectionString("Api_UserContext"));
                if (n.succes == true)
                {
                    return Ok(n);
                }
                else
                {
                    return Ok(n.message);
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }
        
        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducts(long id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }

            try
            {
                dynamic n = ProductsData.DeleteProduct(id, Configuration.GetConnectionString("Api_UserContext"));
                if(n.succes == true)
                {
                    return Ok(n);
                }
                else
                {
                    return Ok(n.message);
                }
            }
            catch (Exception err)
            {
                return Ok(err);
            }
            return NoContent();
        }

        private bool ProductsExists(long id)
        {
            return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
