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
using Api_User.Services.NewFolder;
using Api_User.Wrappers.Filter;
using Api_User.Services;

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
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsByIdPageController : ControllerBase
    {
        private readonly Api_UserContext _context;
        private IConfiguration Configuration;
        private readonly IUriService UriService;
        private readonly IUriServiceClient UriServiceClient;
        public ProductsByIdPageController(Api_UserContext context, IConfiguration configuration, IUriService uriService, IUriServiceClient uriServiceClient)
        {
            _context = context;
            Configuration = configuration;
            UriService = uriService;
            UriServiceClient = uriServiceClient;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter, long id)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var list = CategoryData.GetProductByIDPage(id,Configuration.GetConnectionString("Api_UserContext"));
            var lista = list.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize);
            var totalRecord = list.Count();

            var pagedResponse = PaginationHelper.CreatePagedResponse<Products>(lista, validFilter, totalRecord, UriService, route);

            return Ok(pagedResponse);
            //return Ok(lista);
            //return Ok(new PagedResponse<IEnumerable<Products>>(lista, validFilter.PageNumber, validFilter.PageSize));
            //return Ok(new PagedResponse<List<Products>>(list,validFilter.PageNumber,validFilter.PageSize));
        }

    }
}
