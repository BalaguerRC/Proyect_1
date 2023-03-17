using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api_User.Data;
using Api_User.Models;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Api_User.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly Api_UserContext _context;
        private IConfiguration Configuration;

        public UsersController(Api_UserContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        // GET: api/Users
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var rToken= ValidarToken(identity);
            if(rToken== null)
            {
                return NotFound();
            }
            else
            {
                if (_context.User == null)
                {
                    return NotFound();
                }
                return await _context.User.ToListAsync();
            }
        }

        public static dynamic ValidarToken(ClaimsIdentity identity)
        {
            try
            {
                if (identity == null)
                {
                    return false;
                }
                var email = identity.Claims.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value;
                UserAutenticate.users.FirstOrDefault(x=>x.Email== email);
                return new
                {
                    success= true,
                    data= email
                };
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        // GET: api/Users/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(long id)
        {
          if (_context.User == null)
          {
              return NotFound();
          }

            /*var identity = HttpContext.User.Identity as ClaimsIdentity;
            var validateToken=Validate(identity);

            if (!validateToken.succes)
            {
                return validateToken;
            }
            User usuario = validateToken.result;*/
            var user = await _context.User.FindAsync(id);

            string desencrypt = "";

            Encryp encrypt = new Encryp();
            User user1 = new User();

            user1.Id = id;
            user1.Name = user.Name;
            user1.Email = user.Email;
            desencrypt = encrypt.DesEncrypting(user.Password);
            user1.Password = desencrypt;
            user1.Date = user.Date;
            
            

            if (user1 == null)
            {
                return NotFound();
            }

            return user1;
        }
        /*public static dynamic Validate(ClaimsIdentity identity)
        {
            try
            {
                if (identity.Claims.Count()== 0){
                    return new
                    {
                        succes = false,
                        message = "Error"
                    };
                }
                var email= identity.Claims.FirstOrDefault(x=>x.Type== "Email").Value;
                UserModel user= new UserModel();
                user.Email = email;
                return new
                {
                    succes = true,
                    message = "Exito",
                    result= user
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    message = ex.Message
                };
            }
        }*/
        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(long id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            User user1 = new User();
            Encryp encrypt = new Encryp();
            string NewEncrypt = "";

            if (_context.User == null)
            {
               return Problem("Entity set 'Api_UserContext.User'  is null.");
            }
            bool validation=false;

            try
            {
                //User user1 = new User();
                user1.Id = user.Id;
                user1.Name = user.Name;
                user1.Email = user.Email;
                //user1.Password = encrypt.Encrypting(user.Password);
                NewEncrypt= encrypt.Encrypting(user.Password);
                user1.Password = NewEncrypt;
                user1.Date = user.Date;
                _context.User.Add(user1);
                await _context.SaveChangesAsync();
                validation= true;
            }
            catch (Exception)
            {
                validation=false;
            }
            
            //string conectionString = Configuration.GetConnectionString("Api_UserContext");

            return CreatedAtAction(nameof(GetUser), new { id = user1.Id }, user1);
        }
        #region test
        [HttpPost]
        [Route("login")]
        public dynamic Login([FromBody] User login)
        {
            /*var issuer = Configuration["Jwt:Issuer"];
            var audience = Configuration["Jwt:Audience"];
            var key = Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]);*/


            return LoginData.IsLoggedIn(login, Configuration.GetConnectionString("Api_UserContext"), Configuration);
        }
        #endregion
        // DELETE: api/Users/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            if (_context.User == null)
            {
                return NotFound();
            }
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool UserExists(long id)
        {
            return (_context.User?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
