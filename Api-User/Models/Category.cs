using System.ComponentModel.DataAnnotations;

namespace Api_User.Models
{
    public class Category
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
