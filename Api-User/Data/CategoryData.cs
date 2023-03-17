using Api_User.Models;
using Microsoft.Data.SqlClient;

namespace Api_User.Data
{
    public class CategoryData
    {
        static SqlConnection conn;
        private IConfiguration Configuration;
        public CategoryData(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public static dynamic GetProductByID(long id, string connection)
        {
            List<Products> list = new List<Products>();
            using (conn = new SqlConnection(connection))
            {   
                Products products = new Products();
                conn.Open();

                SqlCommand cmd = new SqlCommand("Select pr.Id,pr.Name,pr.Description,pr.Precio,pr.Author,c.Name,pr.Date,pr.Cantidad,pr.Imagen from Products pr join Categories c on pr.IDCategory=c.Id where pr.IDCategory=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = cmd.ExecuteReader();
                if(reader.Read())
                {
                    reader.Close();
                    reader.Dispose();
                    SqlDataReader reader2 = cmd.ExecuteReader();

                    while (reader2.Read())
                    {
                        list.Add(new Products
                        {
                            Id = reader2.GetInt64(0),
                            Name = reader2.GetString(1),
                            Description = reader2.GetString(2),
                            Precio = reader2.GetString(3),
                            Author = reader2.GetString(4),
                            Category = reader2.GetString(5),
                            Date = reader2.GetDateTime(6),
                            quantity = reader2.GetInt32(7),
                            Image = reader2.IsDBNull(8) ? null : reader2.GetString(8)
                        });
                        products.Category = reader2.GetString(5);
                    }
                    reader2.Close();
                    reader2.Dispose();

                    conn.Close();

                    return new
                    {
                        success = true,
                        category= products.Category,
                        Data = list
                    };
                }
                else
                {
                    reader.Close();
                    reader.Dispose();

                    conn.Close();
                    return new
                    {
                        success = false,
                        message = "No existe"
                    };
                }
            }
        }

        /**
          public static dynamic GetProductByID(long id, string connection)
        {
            List<Products> list = new List<Products>();
            using (conn = new SqlConnection(connection))
            {   
                Products products = new Products();
                conn.Open();

                SqlCommand cmd = new SqlCommand("Select pr.Id,pr.Name,pr.Description,pr.Precio,pr.Author,c.Name,pr.Date,pr.Cantidad,pr.Imagen from Products pr join Categories c on pr.IDCategory=c.Id where pr.IDCategory=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = cmd.ExecuteReader();
                if(reader.Read())
                {
                    reader.Close();
                    reader.Dispose();
                    SqlDataReader reader2 = cmd.ExecuteReader();

                    while (reader2.Read())
                    {
                        list.Add(new Products
                        {
                            Id = reader2.GetInt64(0),
                            Name = reader2.GetString(1),
                            Description = reader2.GetString(2),
                            Precio = reader2.GetString(3),
                            Author = reader2.GetString(4),
                            Category = reader2.GetString(5),
                            Date = reader2.GetDateTime(6),
                            quantity = reader.GetInt32(7),
                            Image = reader.IsDBNull(8) ? null : reader.GetString(8)
                        });
                        products.Category = reader2.GetString(5);
                    }
                    reader2.Close();
                    reader2.Dispose();

                    conn.Close();

                    return new
                    {
                        success = true,
                        category= products.Category,
                        Data = list
                    };
                }
                else
                {
                    reader.Close();
                    reader.Dispose();

                    conn.Close();
                    return new
                    {
                        success = false,
                        message = "No existe"
                    };
                }
            }
        }
         */
    }
}
