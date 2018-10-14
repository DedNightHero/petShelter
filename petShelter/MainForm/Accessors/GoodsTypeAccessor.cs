using MySql.Data.MySqlClient;

namespace MainForm.Accessors
{
    public class GoodsTypeAccessor
    {
        public void ReadData(AbstractTransaction abstractTransaction, AbstractConnection abstractConnection, PetShelter petShelter)
        {
            MySqlDataAdapter sqlDataAdapter = new MySqlDataAdapter();
            sqlDataAdapter.SelectCommand = new MySqlCommand("Select * from goodstype");
            sqlDataAdapter.SelectCommand.Connection = abstractConnection.Connection;
            sqlDataAdapter.SelectCommand.Transaction = abstractTransaction.Transaction;
            sqlDataAdapter.Fill(petShelter, "goodstype");
        }
        public void WriteData(AbstractTransaction abstractTransaction, AbstractConnection abstractConnection, PetShelter petShelter)
        {
            MySqlDataAdapter sqlDataAdapter = new MySqlDataAdapter();
            sqlDataAdapter.UpdateCommand = new MySqlCommand("Update goodstype set TypeOfGoods = @tog where Id_GoodsType = @i");
            sqlDataAdapter.InsertCommand = new MySqlCommand("Insert into goodstype (TypeOfGoods) values (@tog)");
            sqlDataAdapter.DeleteCommand = new MySqlCommand("Delete from goodstype where Id_GoodsType = @i");

            MySqlParameter Id_GoodsType = new MySqlParameter
            {
                SourceColumn = "Id_GoodsType",
                ParameterName = "@i",
            };
            MySqlParameter TypeOfGoods = new MySqlParameter
            {
                SourceColumn = "TypeOfGoods",
                ParameterName = "@tog",
            };

            sqlDataAdapter.UpdateCommand.Parameters.Add(Id_GoodsType);
            sqlDataAdapter.UpdateCommand.Parameters.Add(TypeOfGoods);

            sqlDataAdapter.DeleteCommand.Parameters.Add(Id_GoodsType);

            sqlDataAdapter.InsertCommand.Parameters.Add(Id_GoodsType);
            sqlDataAdapter.InsertCommand.Parameters.Add(TypeOfGoods);

            sqlDataAdapter.UpdateCommand.Connection = abstractConnection.Connection;
            sqlDataAdapter.UpdateCommand.Transaction = abstractTransaction.Transaction;
            sqlDataAdapter.InsertCommand.Connection = abstractConnection.Connection;
            sqlDataAdapter.InsertCommand.Transaction = abstractTransaction.Transaction;
            sqlDataAdapter.DeleteCommand.Connection = abstractConnection.Connection;
            sqlDataAdapter.DeleteCommand.Transaction = abstractTransaction.Transaction;

            sqlDataAdapter.Update(petShelter, "goodstype");
        }
    }
}