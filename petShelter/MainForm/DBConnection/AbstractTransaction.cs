using MySql.Data.MySqlClient;

namespace MainForm
{
    public class AbstractTransaction
    {
        private MySqlTransaction transaction;

        public MySqlTransaction Transaction
        {
            get
            {
                return transaction;
            }
            set
            {
                transaction = value;
            }
        }

        public void Commit()
        {
            transaction.Commit();
        }

        public void Rollback()
        {
            transaction.Rollback();
        }
    }
}
