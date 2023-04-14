using System.ComponentModel.DataAnnotations;

namespace Api_User.Models
{
    public class Compra
    {
        [Key]
        public long? id_compra { get; set; }
        public int? id_user { get; set; }
        public string? userName { get; set; }
        public int? id_product { get; set; }
        public string? productName { get; set; }
        public int amount { get; set; }
        public string? price { get; set; }
        public string? Total { get; set; }


        public DateTime date { get; set; }
    }
    public class Compra2
    {
        [Key]
        public int? id_compra { get; set; }
        public int? id_user { get; set; }
        public int? id_product { get; set; }
        public int amount { get; set; }
        public string? total_price { get; set; }

        public DateTime date { get; set; }
    }
    public class CompraByID
    {
        [Key]
        public int? id_compra { get; set; }
        public DateTime date { get; set; }
    }
    public class CombreClient
    {
        [Key]
        public int id_product { get; set; }
        public string? productName { get; set; }
        public int amount { get; set; }
        public string? price { get; set; }

        public string? total_price { get; set; }
    }
}
