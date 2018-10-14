using MySql.Data.MySqlClient;

namespace MainForm.Accessors
{
    public class SpeciesAccessor
    {
        public void ReadData(AbstractTransaction abstractTransaction, AbstractConnection abstractConnection, PetShelter petShelter)
        {
            MySqlDataAdapter sqlDataAdapter = new MySqlDataAdapter();
            sqlDataAdapter.SelectCommand = new MySqlCommand("Select * from species");
            sqlDataAdapter.SelectCommand.Connection = abstractConnection.Connection;
            sqlDataAdapter.SelectCommand.Transaction = abstractTransaction.Transaction;
            sqlDataAdapter.Fill(petShelter, "species");
        }
        public void WriteData(AbstractTransaction abstractTransaction, AbstractConnection abstractConnection, PetShelter petShelter)
        {
            MySqlDataAdapter sqlDataAdapter = new MySqlDataAdapter();
            sqlDataAdapter.UpdateCommand = new MySqlCommand("Update species set NameOfSpecies = @nos where Id_Species = @i");
            sqlDataAdapter.InsertCommand = new MySqlCommand("Insert into species (NameOfSpecies) values (@nos)");
            sqlDataAdapter.DeleteCommand = new MySqlCommand("Delete from species where Id_Species = @i");

            MySqlParameter Id_Species = new MySqlParameter
            {
                SourceColumn = "Id_Species",
                ParameterName = "@i",
            };
            MySqlParameter NameOfSpecies = new MySqlParameter
            {
                SourceColumn = "NameOfSpecies",
                ParameterName = "@nos",
            };

            sqlDataAdapter.UpdateCommand.Parameters.Add(Id_Species);
            sqlDataAdapter.UpdateCommand.Parameters.Add(NameOfSpecies);

            sqlDataAdapter.DeleteCommand.Parameters.Add(Id_Species);

            sqlDataAdapter.InsertCommand.Parameters.Add(Id_Species);
            sqlDataAdapter.InsertCommand.Parameters.Add(NameOfSpecies);

            sqlDataAdapter.UpdateCommand.Connection = abstractConnection.Connection;
            sqlDataAdapter.UpdateCommand.Transaction = abstractTransaction.Transaction;
            sqlDataAdapter.InsertCommand.Connection = abstractConnection.Connection;
            sqlDataAdapter.InsertCommand.Transaction = abstractTransaction.Transaction;
            sqlDataAdapter.DeleteCommand.Connection = abstractConnection.Connection;
            sqlDataAdapter.DeleteCommand.Transaction = abstractTransaction.Transaction;

            sqlDataAdapter.Update(petShelter, "species");
        }
    }
}