using System.ComponentModel.DataAnnotations;

namespace Api_User.Models
{
    public class User
    {
        [Key]
        public long Id { get; set; }
        public string? Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set;}
    }
    public class UserModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UserAutenticate
    {
        public static List<UserModel> users = new List<UserModel>();
    }
}
