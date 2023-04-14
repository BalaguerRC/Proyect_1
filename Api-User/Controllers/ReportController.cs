using Api_User.Data;
using Api_User.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_User.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        // GET: ReportController
        private IConfiguration Configuration;
        public ReportController(IConfiguration configuration)
        {
            Configuration = configuration;
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
    }
}
