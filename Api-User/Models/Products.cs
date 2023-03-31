using System.ComponentModel.DataAnnotations;

namespace Api_User.Models
{
    public class Products
    {
        [Key]
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Precio { get; set; }
        public string? Author { get; set; }
        public int IDCategory { get; set; }
        public string? Category { get; set; }
        public DateTime Date { get; set; }
        public int quantity { get; set; }
        public string? Image { get; set; }
    }

    public class ProductsModel
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Precio { get; set; }
        public string Author { get; set; }
        public int IDCategory { get; set; }
        public DateTime Date { get; set; }
    }
}
