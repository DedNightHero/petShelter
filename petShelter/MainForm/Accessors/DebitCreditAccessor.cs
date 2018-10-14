using MySql.Data.MySqlClient;

namespace MainForm.Accessors
{
    public class DebitCreditAccessor
    {
        public void ReadData(AbstractTransaction abstractTransaction, AbstractConnection abstractConnection, PetShelter petShelter)
        {
            MySqlDataAdapter sqlDataAdapter = new MySqlDataAdapter();
            sqlDataAdapter.SelectCommand = new MySqlCommand("Select * from debitcredit");
            sqlDataAdapter.SelectCommand.Connection = abstractConnection.Connection;
            sqlDataAdapter.SelectCommand.Transaction = abstractTransaction.Transaction;
            sqlDataAdapter.Fill(petShelter, "debitcredit");
        }
        public void WriteData(AbstractTransaction abstractTransaction, AbstractConnection abstractConnection, PetShelter petShelter)
        {
            MySqlDataAdapter sqlDataAdapter = new MySqlDataAdapter();
            sqlDataAdapter.UpdateCommand = new MySqlCommand("Update debitcredit set GoodsName = @gn, Comment = @c, Date = @d, Debit = @dt, Credit = @ct, PatientId = @pi, UserId = @ui where Id_DebitCredit = @i");
            sqlDataAdapter.InsertCommand = new MySqlCommand("Insert into debitcredit (GoodsName, Comment, Date, Debit, Credit, PatientId, UserId) values (@gn, @c, @d, @dt, @ct, @pi, @ui)");
            sqlDataAdapter.DeleteCommand = new MySqlCommand("Delete from debitcredit where Id_DebitCredit = @i");

            MySqlParameter Id_DebitCredit = new MySqlParameter
            {
                SourceColumn = "Id_DebitCredit",
                ParameterName = "@i",
            };
            MySqlParameter GoodsName = new MySqlParameter
            {
                SourceColumn = "GoodsName",
                ParameterName = "@gn",
            };
            MySqlParameter Comment = new MySqlParameter
            {
                SourceColumn = "Comment",
                ParameterName = "@c",
            };
            MySqlParameter Date = new MySqlParameter
            {
                SourceColumn = "Date",
                ParameterName = "@d",
            };
            MySqlParameter Debit = new MySqlParameter
            {
                SourceColumn = "Debit",
                ParameterName = "@dt",
            };
            MySqlParameter Credit = new MySqlParameter
            {
                SourceColumn = "Credit",
                ParameterName = "@ct",
            };
            MySqlParameter PatientId = new MySqlParameter
            {
                SourceColumn = "PatientId",
                ParameterName = "@pi",
            };
            MySqlParameter  UserId = new MySqlParameter
            {
                SourceColumn = "UserId",
                ParameterName = "@ui",
            };

            sqlDataAdapter.UpdateCommand.Parameters.Add(Id_DebitCredit);
            sqlDataAdapter.UpdateCommand.Parameters.Add(GoodsName);
            sqlDataAdapter.UpdateCommand.Parameters.Add(Comment);
            sqlDataAdapter.UpdateCommand.Parameters.Add(Date);
            sqlDataAdapter.UpdateCommand.Parameters.Add(Debit);
            sqlDataAdapter.UpdateCommand.Parameters.Add(Credit);
            sqlDataAdapter.UpdateCommand.Parameters.Add(PatientId);
            sqlDataAdapter.UpdateCommand.Parameters.Add(UserId);

            sqlDataAdapter.DeleteCommand.Parameters.Add(Id_DebitCredit);

            sqlDataAdapter.InsertCommand.Parameters.Add(GoodsName);
            sqlDataAdapter.InsertCommand.Parameters.Add(Comment);
            sqlDataAdapter.InsertCommand.Parameters.Add(Date);
            sqlDataAdapter.InsertCommand.Parameters.Add(Debit);
            sqlDataAdapter.InsertCommand.Parameters.Add(Credit);
            sqlDataAdapter.InsertCommand.Parameters.Add(PatientId);
            sqlDataAdapter.InsertCommand.Parameters.Add(UserId);

            sqlDataAdapter.UpdateCommand.Connection = abstractConnection.Connection;
            sqlDataAdapter.UpdateCommand.Transaction = abstractTransaction.Transaction;
            sqlDataAdapter.InsertCommand.Connection = abstractConnection.Connection;
            sqlDataAdapter.InsertCommand.Transaction = abstractTransaction.Transaction;
            sqlDataAdapter.DeleteCommand.Connection = abstractConnection.Connection;
            sqlDataAdapter.DeleteCommand.Transaction = abstractTransaction.Transaction;

            sqlDataAdapter.Update(petShelter, "debitcredit");
        }
    }
}