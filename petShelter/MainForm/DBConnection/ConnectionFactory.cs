using System;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace MainForm
{
    public class ConnectionFactory
    {
        public static AbstractConnection CreateConnection()
        {
            MySqlConnection connection = new MySqlConnection(getConnectionString());
            AbstractConnection result = new AbstractConnection();
            result.Connection = connection;
            return result;
        }

        private static String getConnectionString()
        {
            return ConfigurationManager.AppSettings["ConnectionString"];
        }
    }
}
