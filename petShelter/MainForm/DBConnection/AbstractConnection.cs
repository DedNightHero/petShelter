using MySql.Data.MySqlClient;

namespace MainForm
{

    public class AbstractConnection
    {
        private MySqlConnection connection;

        public MySqlConnection Connection
        {
            get
            {
                return connection;
            }
            set
            {
                connection = value;
            }
        }

        public void Open()
        {
            connection.Open();
        }

        public void Close()
        {
            connection.Close();
        }

        public AbstractTransaction BeginTransaction()
        {
            MySqlTransaction transaction = connection.BeginTransaction();
            AbstractTransaction result = new AbstractTransaction();
            result.Transaction = transaction;
            return result;
        }
    }
}