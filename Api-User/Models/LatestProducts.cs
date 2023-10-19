namespace Api_User.Models
{
    public class LatestProducts
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int IdCategory { get; set; }
        public string? Price { get; set; }
        public string? Image { get; set; }
    }
    public class LatestVideoGames
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int IdCategory { get; set; }
        public string? Price { get; set; }
        public string? Image { get; set; }
    }
    public class LatestElectronics
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string Image { get; set; }
    }
}
