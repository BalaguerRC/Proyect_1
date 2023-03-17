using Api_User.Models;
using Microsoft.Data.SqlClient;

namespace Api_User.Data
{
    public class DashboardData
    {
        static SqlConnection conn;
        private IConfiguration Configuration;
        //public static string connectionString;
        public DashboardData(IConfiguration configuration)
        {
            Configuration = configuration;
            //connectionString = Configuration.GetConnectionString("Api_UserContext");
        }
        public static dynamic GetAmount(string connection)
        { 
            List<Dashboard> list = new List<Dashboard>();
            using(conn=new SqlConnection(connection))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM vwDashboard", conn);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Dashboard
                    {
                        total_user= reader.GetInt32(0),
                        total_product = reader.GetInt32(1),
                        total_category = reader.GetInt32(2)
                    });

                }
                return new
                {
                    succes = true,
                    data = list
                };
            }
        }
    }
}
