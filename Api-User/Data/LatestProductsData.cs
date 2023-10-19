using Api_User.Models;
using Microsoft.Data.SqlClient;

namespace Api_User.Data
{
    public class LatestProductsData
    {
        static SqlConnection conn;
        private IConfiguration Configuration;
        //public static string connectionString;
        public LatestProductsData(IConfiguration configuration)
        {
            Configuration = configuration;
            //connectionString = Configuration.GetConnectionString("Api_UserContext");
        }
        public static dynamic GetLatestProduct(string connection)
        {
            List<LatestProducts> list = new List<LatestProducts>();
            try
            {
                conn = new SqlConnection(connection);
                conn.Open();

                SqlCommand cmd = new SqlCommand("select top 5 Id,Name,Precio,IDCategory,Imagen from Products order by Date desc", conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new LatestProducts
                    {
                        Id = Convert.ToInt32(reader.GetInt64(0)),
                        Name = reader.GetString(1),
                        Price = reader.GetString(2),
                        IdCategory = reader.GetInt32(3),
                        Image = reader.IsDBNull(4) ? null : reader.GetString(4),
                    });
                }
                reader.Close();
                reader.Dispose();

                conn.Close();

                return list;
            }
            catch (Exception)
            {
                return list;
            }

        }
    } 
    public class LatestVideoGamesData
    {
        static SqlConnection conn;
        private IConfiguration Configuration;
        //public static string connectionString;
        public LatestVideoGamesData(IConfiguration configuration)
        {
            Configuration = configuration;
            //connectionString = Configuration.GetConnectionString("Api_UserContext");
        }
        public static dynamic GetLatestVideoGames(string connection)
        {
            List<LatestVideoGames> list = new List<LatestVideoGames>();

            try
            {
                conn = new SqlConnection(connection);
                conn.Open();

                SqlCommand cmd = new SqlCommand("select top 5 Id,Name,Precio,IDCategory,Imagen from Products where IDCategory=10010 order by Date desc", conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new LatestVideoGames
                    {
                        Id = Convert.ToInt32(reader.GetInt64(0)),
                        Name = reader.GetString(1),
                        Price = reader.GetString(2),
                        IdCategory = reader.GetInt32(3),
                        Image = reader.IsDBNull(4) ? null : reader.GetString(4),
                    });
                }
                reader.Close();
                reader.Dispose();

                conn.Close();

                return list;
            }
            catch (Exception)
            {
                return list;
            }
        }
    }
    public class LatestElectronicsData
    {
        static SqlConnection conn;
        private IConfiguration Configuration;
        //public static string connectionString;
        public LatestElectronicsData(IConfiguration configuration)
        {
            Configuration = configuration;
            //connectionString = Configuration.GetConnectionString("Api_UserContext");
        }
        public static dynamic GetLatestElectronics(string connection)
        {
            List<LatestElectronics> list = new List<LatestElectronics>();

            try
            {
                conn = new SqlConnection(connection);
                conn.Open();

                SqlCommand cmd = new SqlCommand("select top 4 Id,Name,Imagen from Products where IDCategory=10006 order by Date desc", conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new LatestElectronics
                    {
                        Id = Convert.ToInt32(reader.GetInt64(0)),
                        Name = reader.GetString(1),
                        Image = reader.IsDBNull(2) ? null : reader.GetString(2),
                    });
                }
                reader.Close();
                reader.Dispose();

                conn.Close();

                return list;
            }
            catch (Exception)
            {
                return list;
            }
        }
    }
}
