using Api_User.Data;
using Api_User.Models;
using Api_User.Services;
using Api_User.Services.NewFolder;
using Api_User.Wrappers;
using Api_User.Wrappers.Filter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_User.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        // GET: ReportController
        private IConfiguration Configuration;
        
        private readonly Api_UserContext _context;
        private readonly IUriService UriService;
        private readonly IUriServiceClient UriServiceClient;
        public ReportController(IConfiguration configuration, IUriService uriService, IUriServiceClient uriServiceClient)
        {
            Configuration = configuration;
            UriService = uriService;
            UriServiceClient = uriServiceClient;
        }

        [HttpPost]
        public dynamic AddReport([FromBody] Report report)
        {
            return ReportData.AddReport(report, Configuration.GetConnectionString("Api_UserContext"));
        }
        [HttpGet("{id}")]
        public dynamic GetTotalById(int id)
        {
            return ReportData.GetTotalPriceByID(id, Configuration.GetConnectionString("Api_UserContext"));
        }
        /*
                [HttpGet]
                public dynamic GetReport() {
                    return ReportData.GetReport(Configuration.GetConnectionString("Api_UserContext"));
                }*/
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var list = ReportData.GetReport(Configuration.GetConnectionString("Api_UserContext"));
            var lista = list.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize);
            var totalRecord = list.Count();

            var pagedResponse = PaginationHelper.CreatePagedResponse<Report>(lista, validFilter, totalRecord, UriService, route);

            return Ok(pagedResponse);
        }
    }
}
