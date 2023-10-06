using Api_User.Models;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api_User.Data
{
    public class LoginData
    {
        public static string? TokenString;
        public static dynamic IsLoggedIn(User user, string connectionString, IConfiguration configuration)
        {
            bool R = false;
            bool exito=false;
            object message= new {succes=false,data="Incorrect Email or Password"};
            object datos;

            User user1=new User();
            User user2 = new User();
            
            Encryp encryp=new Encryp();
            string DescPassword = "";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("select Id,Name,Email,Password,Date from [User] where Email=@email", connection);
                cmd.Parameters.AddWithValue("@email", user.Email);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                DataTable tb = new DataTable();

                adapter.Fill(tb);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read()) 
                {
                    R = true;
                }
                if (R)
                {
                    message = new { succes = false, data = "", Token="" };
                    if (tb != null && tb.Rows.Count > 0)
                    {
                        
                        user1.Id = Convert.ToInt32(tb.Rows[0]["Id"].ToString());
                        user1.Name = tb.Rows[0]["Name"].ToString();
                        user1.Email = tb.Rows[0]["Email"].ToString();
                        user1.Password = tb.Rows[0]["Password"].ToString();
                        user1.Date = Convert.ToDateTime(tb.Rows[0]["Date"].ToString());

                        DescPassword = encryp.DesEncrypting(user1.Password);
                        if (DescPassword == user.Password)
                        {
                            //datos = new { id = Id, name = Name, email = Email, date = Date };
                            reader.Close();
                            reader.Dispose();

                            UserModel userModel = new UserModel();
                            userModel.Email = user1.Email;
                            userModel.Password = DescPassword;//desencry

                            
                            
                            GenerateToken(userModel, configuration);
                            /*
                            //token

                            //end token*/
                            exito = true;
                            message = new { succes=exito,data= (datos=new { id = user1.Id, name = user1.Name, email = user1.Email, date = user1.Date }), Token= TokenString};
                        }
                    }
                    return message;
                }
                else
                {
                    reader.Close();
                    reader.Dispose();
                    exito = false;
                    return message = new
                    {
                        succes = exito,
                        data =""
                    };
                }
            }
        }
        public static string GenerateToken(UserModel user, IConfiguration configuration)
        {
            var issuer = configuration["Jwt:Issuer"];
            var audience = configuration["Jwt:Audience"];
            var key = configuration["Jwt:Key"];

            //generatetoken
            var securitykey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));

            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha512Signature);

            var claims = new[]
            {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("email", user.Email)
            };

            var token = new JwtSecurityToken(
                issuer,
                audience,
                claims,
                expires: DateTime.Now.AddMinutes(20),
                signingCredentials: credentials);
            var tokenHandler = new JwtSecurityTokenHandler();

            TokenString = tokenHandler.WriteToken(token);

            return TokenString;
        }
        /*public static UserModel Authenticate(User user)
        {
            var currentuser = UserAutenticate.users.FirstOrDefault(x => x.Email.ToLower() == user.Email.ToLower() && x.Password.ToLower() == user.Password.ToLower());

            if(currentuser == null)
            {
                return currentuser;
            }
            return null;
        }*/
    }
    /*var tokendescriptor = new SecurityTokenDescriptor
    {
        Subject = new ClaimsIdentity(new[]
                                {
                                    new Claim("Id", Guid.NewGuid().ToString()),
                                    new Claim(JwtRegisteredClaimNames.Sub, user1.Email),
                                    new Claim(JwtRegisteredClaimNames.Email, user1.Email),
                                    new Claim(JwtRegisteredClaimNames.Jti,
                                    Guid.NewGuid().ToString())
                                }),
        Expires = DateTime.UtcNow.AddMinutes(5),
        Issuer = issuer,
        Audience = audience,
        SigningCredentials = new SigningCredentials
                                (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
    };
    var tokenhandler = new JwtSecurityTokenHandler();
    var token = tokenhandler.CreateToken(tokendescriptor);
    var jwtToken = tokenhandler.WriteToken(token);
    var stringToken = tokenhandler.WriteToken(token);*/

}
