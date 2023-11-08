using Api_User.Data;
using Api_User.Models;
using Api_User.Services.NewFolder;
using Api_User.Services;
using Api_User.Wrappers.Filter;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api_User.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LatestProductsController
    {
        private readonly Api_UserContext _context;

        private IConfiguration Configuration;
        public LatestProductsController(Api_UserContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }
        [HttpGet]
        public List<LatestProducts> GetLatestProducts()
        {
            return LatestProductsData.GetLatestProduct(Configuration.GetConnectionString("Api_UserContext"));
        }
        [Route("videogames")]
        [HttpGet]
        public List<LatestVideoGames> GetLatestVideoGames()
        {
            return LatestVideoGamesData.GetLatestVideoGames(Configuration.GetConnectionString("Api_UserContext"));
        }
        [Route("electronics")]
        [HttpGet]
        public List<LatestElectronics> GetLatestElectronics()
        {
            return LatestElectronicsData.GetLatestElectronics(Configuration.GetConnectionString("Api_UserContext"));
        }
        [Route("shoes")]
        [HttpGet]
        public List<LatestShoes> GetLatestShoes()
        {
            return LatesShoesData.GetLatestShoes(Configuration.GetConnectionString("Api_UserContext"));
        }
    }
}
