using MySql.Data.MySqlClient;

namespace MainForm.Accessors
{
    public class UsersAccessor
    {
        public void ReadData(AbstractTransaction abstractTransaction, AbstractConnection abstractConnection, PetShelter petShelter)
        {
            MySqlDataAdapter sqlDataAdapter = new MySqlDataAdapter();
            sqlDataAdapter.SelectCommand = new MySqlCommand("Select * from users");
            sqlDataAdapter.SelectCommand.Connection = abstractConnection.Connection;
            sqlDataAdapter.SelectCommand.Transaction = abstractTransaction.Transaction;
            sqlDataAdapter.Fill(petShelter, "users");
        }
        public void WriteData(AbstractTransaction abstractTransaction, AbstractConnection abstractConnection, PetShelter petShelter)
        {
            MySqlDataAdapter sqlDataAdapter = new MySqlDataAdapter();
            sqlDataAdapter.UpdateCommand = new MySqlCommand("Update users set Login = @l, Password = @p, FirstMiddleLastName = @fmln, Position = @pn, Phone = @pe, Address = @a where Id_Users = @i");
            sqlDataAdapter.InsertCommand = new MySqlCommand("Insert into users (Login, Password, FirstMiddleLastName, Position, Phone, Address) values (@l, @p, @fmln, @pn, @pe, @a)");
            sqlDataAdapter.DeleteCommand = new MySqlCommand("Delete from users where Id_Users = @i");

            MySqlParameter Id_Users = new MySqlParameter
            {
                SourceColumn = "Id_Users",
                ParameterName = "@i",
            };
            MySqlParameter Login = new MySqlParameter
            {
                SourceColumn = "Login",
                ParameterName = "@l",
            };
            MySqlParameter Password = new MySqlParameter
            {
                SourceColumn = "Password",
                ParameterName = "@p",
            };
            MySqlParameter FirstMiddleLastName = new MySqlParameter
            {
                SourceColumn = "FirstMiddleLastName",
                ParameterName = "@fmln",
            };
            MySqlParameter Position = new MySqlParameter
            {
                SourceColumn = "Position",
                ParameterName = "@pn",
            };
            MySqlParameter Phone = new MySqlParameter
            {
                SourceColumn = "Phone",
                ParameterName = "@pe",
            };
            MySqlParameter Address = new MySqlParameter
            {
                SourceColumn = "Address",
                ParameterName = "@a",
            };

            sqlDataAdapter.UpdateCommand.Parameters.Add(Id_Users);
            sqlDataAdapter.UpdateCommand.Parameters.Add(Login);
            sqlDataAdapter.UpdateCommand.Parameters.Add(Password);
            sqlDataAdapter.UpdateCommand.Parameters.Add(FirstMiddleLastName);
            sqlDataAdapter.UpdateCommand.Parameters.Add(Position);
            sqlDataAdapter.UpdateCommand.Parameters.Add(Phone);
            sqlDataAdapter.UpdateCommand.Parameters.Add(Address);

            sqlDataAdapter.DeleteCommand.Parameters.Add(Id_Users);

            sqlDataAdapter.InsertCommand.Parameters.Add(Login);
            sqlDataAdapter.InsertCommand.Parameters.Add(Password);
            sqlDataAdapter.InsertCommand.Parameters.Add(FirstMiddleLastName);
            sqlDataAdapter.InsertCommand.Parameters.Add(Position);
            sqlDataAdapter.InsertCommand.Parameters.Add(Phone);
            sqlDataAdapter.InsertCommand.Parameters.Add(Address);

            sqlDataAdapter.UpdateCommand.Connection = abstractConnection.Connection;
            sqlDataAdapter.UpdateCommand.Transaction = abstractTransaction.Transaction;
            sqlDataAdapter.InsertCommand.Connection = abstractConnection.Connection;
            sqlDataAdapter.InsertCommand.Transaction = abstractTransaction.Transaction;
            sqlDataAdapter.DeleteCommand.Connection = abstractConnection.Connection;
            sqlDataAdapter.DeleteCommand.Transaction = abstractTransaction.Transaction;

            sqlDataAdapter.Update(petShelter, "users");
        }
    }
}