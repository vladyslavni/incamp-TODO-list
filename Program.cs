using System.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;
using NpgsqlTypes;

namespace tasks_list
{
    public class Program
    {
        public static NpgsqlConnection connection;
        public static void Main(string[] args)
        {
            connection = CreateConnection();
            connection.Open();

            CreateHostBuilder(args).Build().Run();

            connection.Close();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });


        public static NpgsqlConnection CreateConnection()
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
    }

}
