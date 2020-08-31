using Npgsql;

namespace tasks_list.utils
{
    public class PGConnection
    {
        private static NpgsqlConnection connection;

        static PGConnection()
        {
            connection = CreateConnection();

            connection.Open();
        }

        private static NpgsqlConnection CreateConnection()
        {
            var connectionStringBuilder = new NpgsqlConnectionStringBuilder
            {
                Host = "localhost",
                Port = 5432,
                Username = "postgres",
                Password = "admin",
                Database = "ToDoDB"
            };

            return new NpgsqlConnection(connectionStringBuilder.ToString()); 
        }

        public static NpgsqlConnection Get()
        {
            return connection;
        }

        public static void Close()
        {
            connection.Close();
        }
    }
}