using Api_User.Models;
using Microsoft.Data.SqlClient;

namespace Api_User.Data
{
    public class CompraData
    {
        static SqlConnection conn;
        private IConfiguration Configuration;
       
        public CompraData(IConfiguration configuration)
        {
            Configuration= configuration;
        }
        public static dynamic AddCompra(Compra compra, string connection)
        {
            //long id;
            using(conn=new SqlConnection(connection))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("insert into Compra(id_compra,id_user, id_product,amount,total_price,date) values(@idcompra,@iduser,@idproduct,@amount,@price,GETDATE())", conn);
               
                    cmd.Parameters.AddWithValue("@idcompra", compra.id_compra);
                    cmd.Parameters.AddWithValue("@iduser", compra.id_user);
                    cmd.Parameters.AddWithValue("@idproduct", compra.id_product);
                    cmd.Parameters.AddWithValue("@amount", compra.amount);
                    cmd.Parameters.AddWithValue("@price", compra.price);

                    cmd.ExecuteNonQuery();

                     conn.Close();

                    return new
                    {
                        success = true,
                        message = "added",
                        date = DateTime.Now
                    };
                }
                catch (Exception)
                {
                    return new
                    {
                        success = false,
                        message = "error",
                        date = DateTime.Now
                    };
                }
                
            }
        }
        public static dynamic getCompraById(int id, string connection)
        {
            using(conn=new SqlConnection(connection))
            {
                Compra c = new Compra();
                conn.Open();
                SqlCommand cmd = new SqlCommand("  Select c.id_compra, u.Name,c.id_product, pr.Name, c.amount, c.total_price, c.date from Compra c " +
                    "join [User] u on c.id_user=u.Id " +
                    "join Products pr on c.id_product=pr.Id where c.id_compra=@id", conn);

                cmd.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    c.id_compra = reader.GetInt32(0);
                    c.userName = reader.GetString(1);
                    c.id_product= reader.GetInt32(2);
                    c.productName= reader.GetString(3);
                    c.amount = reader.GetInt32(4);
                    c.price=reader.GetString(5);
                    c.date=reader.GetDateTime(6);
                }
                reader.Close();
                reader.Dispose();
                conn.Close();

                return new
                {
                    success = true,
                    Data = c
                };
            }
        }
        public static dynamic GetMaxId(string connection)
        {
            int id = 0;
            using (conn=new SqlConnection(connection)) 
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from vwMaxId", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while(reader.Read())
                {
                    id =reader.IsDBNull(0) ? 0: reader.GetInt32(0);
                }

                return new 
                { 
                    success= true,
                    id= id
                };
            }
        }
        public static dynamic GetCompra(string connection)
        {
            List<Compra> list = new List<Compra>();

            using(conn= new SqlConnection(connection))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(" Select c.id_compra, u.Name, pr.Name, c.amount, c.total_price, c.date from Compra c " +
                    "join [User] u on c.id_user=u.Id " +
                    "join Products pr on c.id_product=pr.Id", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while(reader.Read())
                {
                    list.Add(new Compra
                    {
                        id_compra = reader.GetInt32(0),
                        userName = reader.GetString(1),
                        productName = reader.GetString(2),
                        amount = reader.GetInt32(3),
                        price = reader.GetString(4),
                        date = reader.GetDateTime(5),
                    });
                }
                reader.Close();
                reader.Dispose();

                conn.Close();
                return new
                {
                    success = true,
                    data = list
                };
            }
        }
    }
}
