using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace MainForm
{
    public class AbstractConnection
    {
        private bool f = false;
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
            try
            {
                connection.Open();
                f = false;
            }
            catch
            {
                if (connection != null)
                    connection.Close();
                if(!f)
                    MessageBox.Show("Отсутствует соединение с базой данных", "Ошибка", MessageBoxButtons.OK);
                f = true;
                Open();
            }
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