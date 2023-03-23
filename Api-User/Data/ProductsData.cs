using Api_User.Models;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Runtime.Intrinsics.X86;

namespace Api_User.Data
{
    public class ProductsData
    {
        static SqlConnection conn;
        private IConfiguration Configuration;
        //public static string connectionString;
        public ProductsData(IConfiguration configuration)
        {
            Configuration = configuration;
            //connectionString = Configuration.GetConnectionString("Api_UserContext");
        }  
        public static List<Products> GetProducts(string connection)
        {
            List<Products> list = new List<Products>();
            Products products = new Products();

            conn = new SqlConnection(connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand("Select pr.Id,pr.Name,pr.Description,pr.Precio,pr.Author,c.Name,pr.Date,pr.Cantidad,pr.Imagen from Products pr join Categories c on pr.IDCategory=c.Id", conn);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new Products
                {
                    Id = reader.GetInt64(0),
                    Name = reader.GetString(1),
                    Description = reader.GetString(2),
                    Precio = reader.GetString(3),
                    Author = reader.GetString(4),
                    Category = reader.GetString(5),
                    Date = reader.GetDateTime(6),
                    quantity= reader.GetInt32(7),
                    Image =reader.IsDBNull(8) ? null: reader.GetString(8)
                });
            }
            reader.Close();
            reader.Dispose();

            conn.Close();

            return list;
        }
        public static dynamic AddProduct(Products products, string connection)
        {
            using(conn= new SqlConnection(connection))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("insert into Products(Name,Description,Precio,Author,IDCategory,Date,Cantidad,Imagen) values(@name,@description,@precio,@author,@idcategory,GETDATE(),@cantidad,@imagen)", conn);

                cmd.Parameters.AddWithValue("@name", products.Name);
                cmd.Parameters.AddWithValue("@description", products.Description);
                cmd.Parameters.AddWithValue("@precio", products.Precio);
                cmd.Parameters.AddWithValue("@author", products.Author);
                cmd.Parameters.AddWithValue("@idcategory", products.IDCategory);
                cmd.Parameters.AddWithValue("@cantidad", products.quantity);
                cmd.Parameters.AddWithValue("@imagen", products.Image);

                cmd.ExecuteNonQuery();
                conn.Close();
                return new
                {
                    succes = true,
                    message= "Added"
                };
            }
        }
        public static dynamic EditProduct(Products products, string connection)
        {
            using( conn= new SqlConnection(connection))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("update Products set Name=@name, Description=@description,Precio=@precio,Author=@author,IDCategory=@idcategory,Date=GETDATE(),Cantidad=@cantidad,Imagen=@imagen where Id=@id", conn);
                cmd.Parameters.AddWithValue("@id", products.Id);
                cmd.Parameters.AddWithValue("@name", products.Name);
                cmd.Parameters.AddWithValue("@description", products.Description);
                cmd.Parameters.AddWithValue("@precio", products.Precio);
                cmd.Parameters.AddWithValue("@author", products.Author);
                cmd.Parameters.AddWithValue("@idcategory", products.IDCategory);
                cmd.Parameters.AddWithValue("@cantidad", products.quantity);
                cmd.Parameters.AddWithValue("@imagen", products.Image);

                cmd.ExecuteNonQuery();

                conn.Close();

                return new
                {
                    succes = true,
                    message = "Edited"
                };
            }
        }
        public static dynamic DeleteProduct(long id, string connection)
        {
            try
            {
                using( conn= new SqlConnection(connection))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("delete Products where Id=@id", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();

                    conn.Close();
                    return new
                    {
                        succes = true,
                        message = "deleted"
                    };
                }
            }
            catch (Exception)
            {
                return new
                {
                    succes = false,
                    message = "error"
                };
            }
            
        }

        public static dynamic GetIDProduct(long id, string connection)
        {
            using( conn= new SqlConnection(connection))
            {
                try
                {
                    Products products = new Products();
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("Select pr.Id,pr.Name,pr.Description,pr.Precio,pr.Author,c.Name,pr.Date from Products pr join Categories c on pr.IDCategory=c.Id where pr.Id=@id", conn);
                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read()) {
                        reader.Close();
                        reader.Dispose();
                        SqlDataReader reader2 = cmd.ExecuteReader();

                        while (reader2.Read())
                        {

                            products.Id = reader2.GetInt64(0);
                            products.Name = reader2.GetString(1);
                            products.Description = reader2.GetString(2);
                            products.Precio = reader2.GetString(3);
                            products.Author = reader2.GetString(4);
                            products.Category = reader2.GetString(5);
                            products.Date = reader2.GetDateTime(6);
                            //});
                        }
                        reader2.Close();
                        reader2.Dispose();

                        conn.Close();

                        return new
                        {
                            success = true,
                            Data = products
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
                catch (Exception)
                {
                    return new
                    {
                        success = false,
                        message = "No existe"
                    };
                }
            }
        }
    }
}
