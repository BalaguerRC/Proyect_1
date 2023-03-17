namespace Api_User.Data
{
    public class Conexion
    {
        private static string connection;
        public static string ruta {
            get
            {
                return connection;
            } 
            set
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", true, true);

                IConfiguration configuration = builder.Build();

                var connectionString = configuration.GetConnectionString("Api_UserContext");

                connection = connectionString;
            }
        }


    }
}
