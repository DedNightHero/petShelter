using MySql.Data.MySqlClient;

namespace MainForm.Accessors
{
    public class GoodsAccessor
    {
        public void ReadData(AbstractTransaction abstractTransaction, AbstractConnection abstractConnection, PetShelter petShelter)
        {
            MySqlDataAdapter sqlDataAdapter = new MySqlDataAdapter();
            sqlDataAdapter.SelectCommand = new MySqlCommand("Select * from goods");
            sqlDataAdapter.SelectCommand.Connection = abstractConnection.Connection;
            sqlDataAdapter.SelectCommand.Transaction = abstractTransaction.Transaction;
            sqlDataAdapter.Fill(petShelter, "goods");
        }
        public void WriteData(AbstractTransaction abstractTransaction, AbstractConnection abstractConnection, PetShelter petShelter)
        {
            MySqlDataAdapter sqlDataAdapter = new MySqlDataAdapter();
            sqlDataAdapter.UpdateCommand = new MySqlCommand("Update goods set NameOfGoods = @nog, Type = @t, Amount = @a, Required = @r where Id_Goods = @i");
            sqlDataAdapter.InsertCommand = new MySqlCommand("Insert into goods (NameOfGoods, Type, Amount, Required) values (@nog, @t, @a, @r)");
            sqlDataAdapter.DeleteCommand = new MySqlCommand("Delete from goods where Id_Goods = @i");

            MySqlParameter Id_Goods = new MySqlParameter
            {
                SourceColumn = "Id_Goods",
                ParameterName = "@i",
            };
            MySqlParameter NameOfGoods = new MySqlParameter
            {
                SourceColumn = "NameOfGoods",
                ParameterName = "@nog",
            };
            MySqlParameter Type = new MySqlParameter
            {
                SourceColumn = "Type",
                ParameterName = "@t",
            };
            MySqlParameter Amount = new MySqlParameter
            {
                SourceColumn = "Amount",
                ParameterName = "@a",
            };
            MySqlParameter Required = new MySqlParameter
            {
                SourceColumn = "Required",
                ParameterName = "@r",
            };

            sqlDataAdapter.UpdateCommand.Parameters.Add(Id_Goods);
            sqlDataAdapter.UpdateCommand.Parameters.Add(NameOfGoods);
            sqlDataAdapter.UpdateCommand.Parameters.Add(Type);
            sqlDataAdapter.UpdateCommand.Parameters.Add(Amount);
            sqlDataAdapter.UpdateCommand.Parameters.Add(Required);

            sqlDataAdapter.DeleteCommand.Parameters.Add(Id_Goods);

            sqlDataAdapter.InsertCommand.Parameters.Add(NameOfGoods);
            sqlDataAdapter.InsertCommand.Parameters.Add(Type);
            sqlDataAdapter.InsertCommand.Parameters.Add(Amount);
            sqlDataAdapter.InsertCommand.Parameters.Add(Required);

            sqlDataAdapter.UpdateCommand.Connection = abstractConnection.Connection;
            sqlDataAdapter.UpdateCommand.Transaction = abstractTransaction.Transaction;
            sqlDataAdapter.InsertCommand.Connection = abstractConnection.Connection;
            sqlDataAdapter.InsertCommand.Transaction = abstractTransaction.Transaction;
            sqlDataAdapter.DeleteCommand.Connection = abstractConnection.Connection;
            sqlDataAdapter.DeleteCommand.Transaction = abstractTransaction.Transaction;

            sqlDataAdapter.Update(petShelter, "goods");
        }
    }
}