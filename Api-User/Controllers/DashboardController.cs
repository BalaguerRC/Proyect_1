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
    public class DashboardController: ControllerBase
    {
        private IConfiguration Configuration;
        public DashboardController(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public IActionResult Amount([FromBody] Dashboard dashboard)
        {
            if(dashboard!= null)
            {
                
            }
            return Ok(dashboard);
        }
        
    }
}
