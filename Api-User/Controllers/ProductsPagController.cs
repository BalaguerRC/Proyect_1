using Api_User.Data;
using Api_User.Models;
using Api_User.Services;
using Api_User.Services.NewFolder;
using Api_User.Wrappers;
using Api_User.Wrappers.Filter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_User.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsPagController:ControllerBase
    {
        private IConfiguration Configuration;
        private readonly Api_UserContext _context;
        private readonly IUriService UriService;
        public ProductsPagController(IConfiguration configuration, IUriService uriService, Api_UserContext context)
        {
            Configuration = configuration;
            UriService = uriService;
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var list = ProductsData.GetProducts(Configuration.GetConnectionString("Api_UserContext"));
            var lista= list.Skip((validFilter.PageNumber-1)*validFilter.PageSize)
                .Take(validFilter.PageSize);
            var totalRecord= list.Count();

            var pagedResponse = PaginationHelper.CreatePagedResponse<Products>(lista, validFilter, totalRecord, UriService, route);

            return Ok(pagedResponse);
            //return Ok(lista);
            //return Ok(new PagedResponse<IEnumerable<Products>>(lista, validFilter.PageNumber, validFilter.PageSize));
            //return Ok(new PagedResponse<List<Products>>(list,validFilter.PageNumber,validFilter.PageSize));
        }
        //Category
        [Route("getCategory")]
        [HttpGet]
        public async Task<IActionResult> GetAllCategory([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var list = await _context.Categories.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize).ToListAsync();
            /*var lista = list.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize);*/
            var totalRecord = await _context.Categories.CountAsync();

            var pagedResponse = PaginationHelper.CreatePagedResponse<Category>(list, validFilter, totalRecord, UriService, route);

            return Ok(pagedResponse);
        }
        //User
        [Route("getUser")]
        [HttpGet]
        public async Task<IActionResult> GetAllUser([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var list = await _context.User.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize).ToListAsync();
            /*var lista = list.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize);*/
            var totalRecord = await _context.User.CountAsync();

            var pagedResponse = PaginationHelper.CreatePagedResponse<User>(list, validFilter, totalRecord, UriService, route);

            return Ok(pagedResponse);
        }

    }
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryPagController: ControllerBase
    {
        private readonly Api_UserContext _context;
        private IConfiguration Configuration;
        private readonly IUriService UriService;
        public CategoryPagController(IConfiguration configuration, IUriService uriService, Api_UserContext context)
        {
            _context = context;
            Configuration = configuration;
            UriService = uriService;

        }
        [HttpGet]
        public async Task<IActionResult> GetAllCategory([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var list = await _context.Categories.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize).ToListAsync();
            /*var lista = list.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize);*/
            var totalRecord = await _context.Categories.CountAsync();

            var pagedResponse = PaginationHelper.CreatePagedResponse<Category>(list, validFilter, totalRecord, UriService, route);

            return Ok(pagedResponse);
        }
    }
    
}
