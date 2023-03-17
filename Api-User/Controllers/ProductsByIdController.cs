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
    public class ProductsByIdController: ControllerBase
    {
        private readonly Api_UserContext _context;
        private IConfiguration Configuration;

        public ProductsByIdController(Api_UserContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetProduct(long id)
        {
            if (_context.Categories == null)
            {
                return NotFound();
            }
            try
            {
                dynamic pro = CategoryData.GetProductByID(id, Configuration.GetConnectionString("Api_UserContext"));
                if (pro != null)
                {
                    return Ok(new
                    {
                        pro.category,
                        pro.Data
                    });
                }
                else
                {
                    return Ok(pro.message);
                }
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}
