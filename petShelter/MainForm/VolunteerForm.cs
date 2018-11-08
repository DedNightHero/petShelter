using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;

namespace MainForm
{
    public partial class VolunteerForm : Form
    {
        #region Объявление переменных
        private int userId;
        PetShelter psUsers = new PetShelter();
        PetShelter psDebitcredit = new PetShelter();
        PetShelter psGoods = new PetShelter();
        PetShelter psGoodstype = new PetShelter();
        BLogic.IBL ibl = new BLogic.BLogic();
        #endregion
        #region Закрытие приложения при закрытии формы
        private void VolunteerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        #endregion
        #region Инициализация и загрузка формы
        public VolunteerForm(int id)
        {
            InitializeComponent();
            userId = id;
        }

        private void VolunteerForm_Load(object sender, EventArgs e)
        {
            initVolunteerForm();
        }
        #endregion
        #region Инициализация информации на форме
        private void initVolunteerForm()
        {
            psUsers = ibl.getUsers();
            psDebitcredit = ibl.getDebitCredit();
            psGoods = ibl.getGoods();
            psGoodstype = ibl.getGoodsType();
            labelVolunteerName.Text = psUsers.users.FindById_Users(userId)[3].ToString();
            textBoxVolunteerFormLastName.Text =psUsers.users.FindById_Users(userId)[3].ToString().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0];
            textBoxVolunteerFormFirstName.Text = psUsers.users.FindById_Users(userId)[3].ToString().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1];
            textBoxVolunteerFormMiddleName.Text = psUsers.users.FindById_Users(userId)[3].ToString().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[2];
            textBoxVolunteerFormVacancy.Text = psUsers.positions.Rows[Convert.ToInt32(psUsers.users.FindById_Users(userId)[4]) - 1][1].ToString();
            textBoxVolunteerFormPhoneNumber.Text = psUsers.users.FindById_Users(userId)[5].ToString();
            textBoxVolunteerFormAddress.Text = psUsers.users.FindById_Users(userId)[6].ToString();

            psDebitcredit.debitcredit.DefaultView.RowFilter = string.Format("CONVERT(UserId, 'System.String') LIKE '" + userId + "' and CONVERT(GoodsName, 'System.String') NOT LIKE '' and Debit = 0");
            dataGridViewVolunteerFormOneCharity.DataSource = psDebitcredit.debitcredit.DefaultView;
            dataGridViewVolunteerFormOneCharity.Columns.Add("goodsNameVolunteer", "Предмет");
            FillCharityVolunteer();
            dataGridViewVolunteerFormOneCharity.Columns["Id_DebitCredit"].Visible = false;
            dataGridViewVolunteerFormOneCharity.Columns["GoodsName"].Visible = false;
            dataGridViewVolunteerFormOneCharity.Columns["Debit"].Visible = false;
            dataGridViewVolunteerFormOneCharity.Columns["PatientId"].Visible = false;
            dataGridViewVolunteerFormOneCharity.Columns["UserId"].Visible = false;
            dataGridViewVolunteerFormOneCharity.Columns["Comment"].HeaderText = "Комментарий";
            dataGridViewVolunteerFormOneCharity.Columns["Date"].HeaderText = "Дата";
            dataGridViewVolunteerFormOneCharity.Columns["Credit"].HeaderText = "Количество";
            dataGridViewVolunteerFormOneCharity.Columns["goodsNameVolunteer"].DisplayIndex = 0;
            dataGridViewVolunteerFormOneCharity.Columns["Credit"].DisplayIndex = 1;
            dataGridViewVolunteerFormOneCharity.Columns["Date"].DisplayIndex = 2;
            dataGridViewVolunteerFormOneCharity.Columns["Comment"].DisplayIndex = 3;
            dataGridViewVolunteerFormOneCharity.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewVolunteerFormOneCharity.ClearSelection();

            dataGridViewVolunteerFormGoods.DataSource = psGoods;
            psGoods.goods.DefaultView.RowFilter = string.Format("CONVERT(Type, 'System.String') NOT LIKE '3'");
            dataGridViewVolunteerFormGoods.DataSource = psGoods.goods.DefaultView;
            dataGridViewVolunteerFormGoods.Columns.Add("goodsTypeVolunteer", "Тип");
            setGoodsTable();
        }
        #endregion
        #region Правильное отображение типов предметов
        private void FillGoodsVolunteer()
        {
            psGoods = ibl.getGoods();
            psGoodstype = ibl.getGoodsType();
            String typeId;
            int rowCount = dataGridViewVolunteerFormGoods.RowCount;
            string expression;

            for (int i = 0; i < rowCount; i++)
            {
                typeId = dataGridViewVolunteerFormGoods.Rows[i].Cells["Type"].Value.ToString();
                System.Data.DataRow[] foundRows;
                expression = "Id_GoodsType = " + typeId;
                foundRows = psGoodstype.goodstype.Select(expression);
                expression = foundRows[0][1].ToString();
                dataGridViewVolunteerFormGoods.Rows[i].Cells["goodsTypeVolunteer"].Value = expression;
            }
        }
        #endregion
        #region Правильное отображение название предметов
        private void FillCharityVolunteer()
        {
            psDebitcredit = ibl.getDebitCredit();
            psGoods = ibl.getGoods();
            String typeId;
            String expression;
            int rowCount = dataGridViewVolunteerFormOneCharity.RowCount;
            for (int i = 0; i < rowCount; i++)
            {
                typeId = dataGridViewVolunteerFormOneCharity.Rows[i].Cells["GoodsName"].Value.ToString();
                System.Data.DataRow[] foundRows;
                expression = "Id_Goods = " + typeId;
                foundRows = psGoods.goods.Select(expression);
                expression = foundRows[0][1].ToString();
                dataGridViewVolunteerFormOneCharity.Rows[i].Cells["goodsNameVolunteer"].Value = expression;
            }
        }
        #endregion
        #region Действия при сортировке таблиц
        private void VolunteerGridSorted(object sender, EventArgs e)
        {
            FillGoodsVolunteer();
        }

        private void CharityGridSorted(object sender, EventArgs e)
        {
            FillCharityVolunteer();
        }
        #endregion
        #region Настройка параметров отображения таблицы "Предметы"
        private void setGoodsTable()
        {
            FillGoodsVolunteer();
            dataGridViewVolunteerFormGoods.Columns["Id_Goods"].Visible = false;
            dataGridViewVolunteerFormGoods.Columns["Type"].Visible = false;
            dataGridViewVolunteerFormGoods.Columns["NameOfGoods"].DisplayIndex = 0;
            dataGridViewVolunteerFormGoods.Columns["goodsTypeVolunteer"].DisplayIndex = 1;
            dataGridViewVolunteerFormGoods.Columns["Amount"].DisplayIndex = 2;
            dataGridViewVolunteerFormGoods.Columns["Required"].DisplayIndex = 3;
            dataGridViewVolunteerFormGoods.Columns["NameOfGoods"].HeaderText = "Название";
            dataGridViewVolunteerFormGoods.Columns[3].HeaderText = "В наличии";
            dataGridViewVolunteerFormGoods.Columns[4].HeaderText = "Всего необходимо";
            dataGridViewVolunteerFormGoods.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewVolunteerFormGoods.ClearSelection();
        }
        #endregion
        #region Фильтрация таблицы "Предметы" согласно указанным критериям
        private void sortGoodsTable(object sender, EventArgs e)
        {
            String filter = "CONVERT(Type, 'System.String') NOT LIKE '3'";
            if (radioButtonVolunteerFormSortCure.Checked) filter += " and CONVERT(Type, 'System.String') LIKE '2'";
            else if (radioButtonVolunteerFormSortEat.Checked) filter += " and CONVERT(Type, 'System.String') LIKE '1'";
            else if (radioButtonVolunteerFormSortOther.Checked) filter += " and CONVERT(Type, 'System.String') LIKE '4'";
            else if (radioButtonVolunteerFormSortAll.Checked) filter += "";
            if (textBoxVolunteerFormSortName.Text != "")
            {
                if (filter != null) filter += " and ";
                filter += "NameOfGoods LIKE '*" + textBoxVolunteerFormSortName.Text + "*'";
            }
            if (filter != null) psGoods.goods.DefaultView.RowFilter = string.Format(filter);
            else
            {
                dataGridViewVolunteerFormGoods.DataSource = psGoods;
                dataGridViewVolunteerFormGoods.DataMember = "goods";
                setGoodsTable();
                return;
            }
            dataGridViewVolunteerFormGoods.DataSource = psGoods.goods.DefaultView;
            setGoodsTable();
            dataGridViewVolunteerFormGoods.ClearSelection();
        }
        #endregion
        #region Кнопка формирования отчёта
        private void buttonVolunteerFormCreateReport_Click(object sender, EventArgs e)
        {
            Reports report = new Reports();
            report.CreateReportFromVisibleItems(dataGridViewVolunteerFormGoods, "Необходимые приюту вещи");
        }
        #endregion
    }
}