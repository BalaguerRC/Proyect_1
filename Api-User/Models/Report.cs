using System.ComponentModel.DataAnnotations;

namespace Api_User.Models
{
    public class Report
    {
        [Key]
        public int? Id { get; set; }
        public int id_compra { get; set; }
        public string total_price { get; set; }
        public DateTime date { get; set; }
    }
}
