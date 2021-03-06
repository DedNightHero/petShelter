﻿using MySql.Data.MySqlClient;

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
            sqlDataAdapter.UpdateCommand = new MySqlCommand("Update debitcredit set GoodsName = @gn, Comment = @c, Date = @d, Debit = @dt, Credit = @ct, PatientId = @pi, UserId = @ui, GoodsType = @gt where Id_DebitCredit = @i");
            if (petShelter.debitcredit.Rows[petShelter.debitcredit.Rows.Count - 1][6].ToString() == "-1" && petShelter.debitcredit.Rows[petShelter.debitcredit.Rows.Count - 1][7].ToString() == "-1")
                sqlDataAdapter.InsertCommand = new MySqlCommand("Insert into debitcredit (GoodsName, Comment, Date, Debit, Credit, PatientId, UserId, GoodsType) values (@gn, @c, @d, @dt, @ct, null, null,@gt)");
            else if (petShelter.debitcredit.Rows[petShelter.debitcredit.Rows.Count - 1][6].ToString() == "-1" && petShelter.debitcredit.Rows[petShelter.debitcredit.Rows.Count - 1][1].ToString() != "-1")
                sqlDataAdapter.InsertCommand = new MySqlCommand("Insert into debitcredit (GoodsName, Comment, Date, Debit, Credit, PatientId, UserId, GoodsType) values (@gn, @c, @d, @dt, @ct, null, @ui, @gt)");
            else if(petShelter.debitcredit.Rows[petShelter.debitcredit.Rows.Count - 1][7].ToString() == "-1" && petShelter.debitcredit.Rows[petShelter.debitcredit.Rows.Count - 1][1].ToString() != "-1")
                sqlDataAdapter.InsertCommand = new MySqlCommand("Insert into debitcredit (GoodsName, Comment, Date, Debit, Credit, PatientId, UserId, GoodsType) values (@gn, @c, @d, @dt, @ct, @pi, null,@gt)");
            else if (petShelter.debitcredit.Rows[petShelter.debitcredit.Rows.Count - 1][8].ToString() == "-1" && petShelter.debitcredit.Rows[petShelter.debitcredit.Rows.Count - 1][1].ToString() != "-1")
                sqlDataAdapter.InsertCommand = new MySqlCommand("Insert into debitcredit (GoodsName, Comment, Date, Debit, Credit, PatientId, UserId, GoodsType) values (@gn, @c, @d, @dt, @ct, @pi, @ui, null)");
            else if(petShelter.debitcredit.Rows[petShelter.debitcredit.Rows.Count - 1][1].ToString() == "-1")
                sqlDataAdapter.InsertCommand = new MySqlCommand("Insert into debitcredit (GoodsName, Comment, Date, Debit, Credit, PatientId, UserId, GoodsType) values (null, @c, @d, @dt, @ct, null, @ui, @gt)");
            else
                sqlDataAdapter.InsertCommand = new MySqlCommand("Insert into debitcredit (GoodsName, Comment, Date, Debit, Credit, PatientId, UserId, GoodsType) values (@gn, @c, @d, @dt, @ct, @pi, @ui, @gt)");
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
            MySqlParameter GoodsType = new MySqlParameter
            {
                SourceColumn = "GoodsType",
                ParameterName = "@gt",
            };


            sqlDataAdapter.UpdateCommand.Parameters.Add(Id_DebitCredit);
            sqlDataAdapter.UpdateCommand.Parameters.Add(GoodsName);
            sqlDataAdapter.UpdateCommand.Parameters.Add(Comment);
            sqlDataAdapter.UpdateCommand.Parameters.Add(Date);
            sqlDataAdapter.UpdateCommand.Parameters.Add(Debit);
            sqlDataAdapter.UpdateCommand.Parameters.Add(Credit);
            sqlDataAdapter.UpdateCommand.Parameters.Add(PatientId);
            sqlDataAdapter.UpdateCommand.Parameters.Add(UserId);
            sqlDataAdapter.UpdateCommand.Parameters.Add(GoodsType);

            sqlDataAdapter.DeleteCommand.Parameters.Add(Id_DebitCredit);

            sqlDataAdapter.InsertCommand.Parameters.Add(GoodsName);
            sqlDataAdapter.InsertCommand.Parameters.Add(Comment);
            sqlDataAdapter.InsertCommand.Parameters.Add(Date);
            sqlDataAdapter.InsertCommand.Parameters.Add(Debit);
            sqlDataAdapter.InsertCommand.Parameters.Add(Credit);
            sqlDataAdapter.InsertCommand.Parameters.Add(PatientId);
            sqlDataAdapter.InsertCommand.Parameters.Add(UserId);
            sqlDataAdapter.InsertCommand.Parameters.Add(GoodsType);

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