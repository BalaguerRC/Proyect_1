using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api_User.Data;
using Api_User.Models;
using System.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace Api_User.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController: ControllerBase
    {
        private IConfiguration Configuration;
        public DashboardController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        [HttpGet]
        public dynamic GetDashboar()
        {
            //dynamic n = DashboardData.GetAmount(Configuration.GetConnectionString("Api_UserContext"));

            //if (n != null)
            //{/
            return DashboardData.GetAmount(Configuration.GetConnectionString("Api_UserContext"));
            //}
        }
        
    }
}
