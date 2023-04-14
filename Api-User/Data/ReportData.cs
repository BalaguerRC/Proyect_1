using Api_User.Models;
using Microsoft.Data.SqlClient;

namespace Api_User.Data
{
    public class ReportData
    {
        static SqlConnection conn;
        private IConfiguration Configuration;
        public ReportData(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public static dynamic AddReport(Report report, string connection)
        {
            using(conn= new SqlConnection(connection))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("insert into Report(id_compra,total_price,date) values(@idcompra,@total,GETDATE())", conn);

                cmd.Parameters.AddWithValue("@idcompra", report.id_compra);
                cmd.Parameters.AddWithValue("@total", report.total_price);

                cmd.ExecuteNonQuery();

                conn.Close();

                return new
                {
                    success = true,
                    message = "Added"
                };
            }

        }
        public static dynamic GetTotalPriceByID(int id, string connection)
        {
            string total= "no existe";
            using(conn=new SqlConnection(connection))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("select total_price from Report where id_compra=@id", conn);

                cmd.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    total= reader.GetString(0);
                }
                reader.Close();
                reader.Dispose();

                conn.Close();

                return new
                {
                    success = true,
                    Data = total
                };
            }
        }
    }
}
