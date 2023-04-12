using Api_User.Data;
using Api_User.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Api_User.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CompraController : ControllerBase
    {
        private IConfiguration Configuration;
        private readonly Api_UserContext _context;
        public CompraController(IConfiguration configuration, Api_UserContext context)
        {
            Configuration = configuration;
            _context = context;
        }
        [HttpPost]
        public dynamic AddCompra([FromBody] Compra compra)
        {
            //return CompraData.AddCompra(compra, Configuration.GetConnectionString("Api_UserContext"));
            CompraData.AddCompra(compra, Configuration.GetConnectionString("Api_UserContext"));
            return new
            {
                success = true,
                dato = compra.id_compra
            };
        }
        [HttpGet("{id}")]
        public dynamic GetCompraById(int id)
        {
            return CompraData.getCompraById(id, Configuration.GetConnectionString("Api_UserContext"));
        }
        [HttpGet]
        public dynamic GetMaxId()
        {
            return CompraData.GetMaxId(Configuration.GetConnectionString("Api_UserContext"));
        }
        [Route("getCompra")]
        [HttpGet]
        public dynamic GetCompra()
        {
            return CompraData.GetCompra(Configuration.GetConnectionString("Api_UserContext"));
        }
        [Route("getCompra2")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Compra2>>> GetCompra2()
        {
            if (_context.Compra == null)
            {
                return NotFound();
            }
            return await _context.Compra.ToListAsync();
        }
    }
}
