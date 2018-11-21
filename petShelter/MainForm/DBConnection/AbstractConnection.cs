using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Diagnostics;

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
            try
            {
                connection.Open();
            }
            catch
            {
                if (connection != null)
                    connection.Close();
                MessageBox.Show("Отсутствует соединение с базой данных", "Ошибка", MessageBoxButtons.OK);
                Process.GetCurrentProcess().Kill();
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