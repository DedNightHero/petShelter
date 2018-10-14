using MySql.Data.MySqlClient;

namespace MainForm.Accessors
{
    public class AnimalsAccessor
    {
        public void ReadData(AbstractTransaction abstractTransaction, AbstractConnection abstractConnection, PetShelter petShelter)
        {
            MySqlDataAdapter sqlDataAdapter = new MySqlDataAdapter();
            sqlDataAdapter.SelectCommand = new MySqlCommand("Select * from animals");
            sqlDataAdapter.SelectCommand.Connection = abstractConnection.Connection;
            sqlDataAdapter.SelectCommand.Transaction = abstractTransaction.Transaction;
            sqlDataAdapter.Fill(petShelter, "animals");
        }
        public void WriteData(AbstractTransaction abstractTransaction, AbstractConnection abstractConnection, PetShelter petShelter)
        {
            MySqlDataAdapter sqlDataAdapter = new MySqlDataAdapter();
            sqlDataAdapter.UpdateCommand = new MySqlCommand("Update animals set Species = @s, Breed = @b, NickName = @nn, ArrivalDate = @ad, InHere = @ih, FMLNameOfOwner = @fmlnoo, OwnerPhone = @op, OwnerAddress = @oa, DeliveryDate = @dd where Id_Animals = @i");
            sqlDataAdapter.InsertCommand = new MySqlCommand("Insert into animals (Species, Breed, NickName, ArrivalDate, InHere, FMLNameOfOwner, OwnerPhone, OwnerAddress, DeliveryDate) values (@s, @b, @nn, @ad, @ih, @fmlnoo, @op, @oa, @dd)");
            sqlDataAdapter.DeleteCommand = new MySqlCommand("Delete from animals where Id_Animals = @i");

            MySqlParameter Id_Animals = new MySqlParameter
            {
                SourceColumn = "Id_Animals",
                ParameterName = "@i",
            };
            MySqlParameter Species = new MySqlParameter
            {
                SourceColumn = "Species",
                ParameterName = "@s",
            };
            MySqlParameter Breed = new MySqlParameter
            {
                SourceColumn = "Breed",
                ParameterName = "@b",
            };
            MySqlParameter NickName = new MySqlParameter
            {
                SourceColumn = "NickName",
                ParameterName = "@nn",
            };
            MySqlParameter ArrivalDate = new MySqlParameter
            {
                SourceColumn = "ArrivalDate",
                ParameterName = "@ad",
            };
            MySqlParameter InHere = new MySqlParameter
            {
                SourceColumn = "InHere",
                ParameterName = "@ih",
            };
            MySqlParameter FMLNameOfOwner = new MySqlParameter
            {
                SourceColumn = "FMLNameOfOwner",
                ParameterName = "@fmlnoo",
            };
            MySqlParameter OwnerPhone = new MySqlParameter
            {
                SourceColumn = "OwnerPhone",
                ParameterName = "@op",
            };
            MySqlParameter OwnerAddress = new MySqlParameter
            {
                SourceColumn = "OwnerAddress",
                ParameterName = "@oa",
            };
            MySqlParameter DeliveryDate = new MySqlParameter
            {
                SourceColumn = "DeliveryDate",
                ParameterName = "@dd",
            };

            sqlDataAdapter.UpdateCommand.Parameters.Add(Id_Animals);
            sqlDataAdapter.UpdateCommand.Parameters.Add(Species);
            sqlDataAdapter.UpdateCommand.Parameters.Add(Breed);
            sqlDataAdapter.UpdateCommand.Parameters.Add(NickName);
            sqlDataAdapter.UpdateCommand.Parameters.Add(ArrivalDate);
            sqlDataAdapter.UpdateCommand.Parameters.Add(InHere);
            sqlDataAdapter.UpdateCommand.Parameters.Add(FMLNameOfOwner);
            sqlDataAdapter.UpdateCommand.Parameters.Add(OwnerPhone);
            sqlDataAdapter.UpdateCommand.Parameters.Add(OwnerAddress);
            sqlDataAdapter.UpdateCommand.Parameters.Add(DeliveryDate);

            sqlDataAdapter.DeleteCommand.Parameters.Add(Id_Animals);

            sqlDataAdapter.InsertCommand.Parameters.Add(Species);
            sqlDataAdapter.InsertCommand.Parameters.Add(Breed);
            sqlDataAdapter.InsertCommand.Parameters.Add(NickName);
            sqlDataAdapter.InsertCommand.Parameters.Add(ArrivalDate);
            sqlDataAdapter.InsertCommand.Parameters.Add(InHere);
            sqlDataAdapter.InsertCommand.Parameters.Add(FMLNameOfOwner);
            sqlDataAdapter.InsertCommand.Parameters.Add(OwnerPhone);
            sqlDataAdapter.InsertCommand.Parameters.Add(OwnerAddress);
            sqlDataAdapter.InsertCommand.Parameters.Add(DeliveryDate);

            sqlDataAdapter.UpdateCommand.Connection = abstractConnection.Connection;
            sqlDataAdapter.UpdateCommand.Transaction = abstractTransaction.Transaction;
            sqlDataAdapter.InsertCommand.Connection = abstractConnection.Connection;
            sqlDataAdapter.InsertCommand.Transaction = abstractTransaction.Transaction;
            sqlDataAdapter.DeleteCommand.Connection = abstractConnection.Connection;
            sqlDataAdapter.DeleteCommand.Transaction = abstractTransaction.Transaction;

            sqlDataAdapter.Update(petShelter, "animals");
        }
    }
}