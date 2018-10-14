using MySql.Data.MySqlClient;

namespace MainForm.Accessors
{
    public class PositionsAccessor
    {
        public void ReadData(AbstractTransaction abstractTransaction, AbstractConnection abstractConnection, PetShelter petShelter)
        {
            MySqlDataAdapter sqlDataAdapter = new MySqlDataAdapter();
            sqlDataAdapter.SelectCommand = new MySqlCommand("Select * from positions");
            sqlDataAdapter.SelectCommand.Connection = abstractConnection.Connection;
            sqlDataAdapter.SelectCommand.Transaction = abstractTransaction.Transaction;
            sqlDataAdapter.Fill(petShelter, "positions");
        }
        public void WriteData(AbstractTransaction abstractTransaction, AbstractConnection abstractConnection, PetShelter petShelter)
        {
            MySqlDataAdapter sqlDataAdapter = new MySqlDataAdapter();
            sqlDataAdapter.UpdateCommand = new MySqlCommand("Update positions set NameOfPosition = @nop, Rights = @r where Id_Positions = @i");
            sqlDataAdapter.InsertCommand = new MySqlCommand("Insert into positions (NameOfPosition, Rights) values (@nop, @r)");
            sqlDataAdapter.DeleteCommand = new MySqlCommand("Delete from positions where Id_Positions = @i");

            MySqlParameter Id_Positions = new MySqlParameter
            {
                SourceColumn = "Id_Positions",
                ParameterName = "@i",
            };
            MySqlParameter NameOfPosition = new MySqlParameter
            {
                SourceColumn = "NameOfPosition",
                ParameterName = "@nop",
            };
            MySqlParameter Rights = new MySqlParameter
            {
                SourceColumn = "Rights",
                ParameterName = "@r",
            };

            sqlDataAdapter.UpdateCommand.Parameters.Add(Id_Positions);
            sqlDataAdapter.UpdateCommand.Parameters.Add(NameOfPosition);
            sqlDataAdapter.UpdateCommand.Parameters.Add(Rights);

            sqlDataAdapter.DeleteCommand.Parameters.Add(Id_Positions);

            sqlDataAdapter.InsertCommand.Parameters.Add(Id_Positions);
            sqlDataAdapter.InsertCommand.Parameters.Add(NameOfPosition);
            sqlDataAdapter.InsertCommand.Parameters.Add(Rights);

            sqlDataAdapter.UpdateCommand.Connection = abstractConnection.Connection;
            sqlDataAdapter.UpdateCommand.Transaction = abstractTransaction.Transaction;
            sqlDataAdapter.InsertCommand.Connection = abstractConnection.Connection;
            sqlDataAdapter.InsertCommand.Transaction = abstractTransaction.Transaction;
            sqlDataAdapter.DeleteCommand.Connection = abstractConnection.Connection;
            sqlDataAdapter.DeleteCommand.Transaction = abstractTransaction.Transaction;

            sqlDataAdapter.Update(petShelter, "positions");
        }
    }
}