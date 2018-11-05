using System;
using System.Windows.Forms;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
//using Word = Microsoft.Office.Interop.Word;
using System.Collections.Generic;

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

            psDebitcredit.debitcredit.DefaultView.RowFilter = string.Format("CONVERT(UserId, 'System.String') LIKE '" + userId + "'");
            dataGridViewVolunteerFormOneCharity.DataSource = psDebitcredit.debitcredit.DefaultView;
            dataGridViewVolunteerFormOneCharity.Columns[0].Visible = false;
            dataGridViewVolunteerFormOneCharity.Columns[4].Visible = false;
            dataGridViewVolunteerFormOneCharity.Columns[6].Visible = false;
            dataGridViewVolunteerFormOneCharity.Columns[7].Visible = false;
            dataGridViewVolunteerFormOneCharity.Columns[1].HeaderText = "Предмет";
            dataGridViewVolunteerFormOneCharity.Columns[2].HeaderText = "Комментарий";
            dataGridViewVolunteerFormOneCharity.Columns[3].HeaderText = "Дата";
            dataGridViewVolunteerFormOneCharity.Columns[5].HeaderText = "Количество";
            dataGridViewVolunteerFormOneCharity.Columns[1].DisplayIndex = 0;
            dataGridViewVolunteerFormOneCharity.Columns[5].DisplayIndex = 1;
            dataGridViewVolunteerFormOneCharity.Columns[3].DisplayIndex = 2;
            dataGridViewVolunteerFormOneCharity.Columns[2].DisplayIndex = 3;
            dataGridViewVolunteerFormOneCharity.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewVolunteerFormOneCharity.ClearSelection();

            dataGridViewVolunteerFormGoods.DataSource = psGoods;
            psGoods.goods.DefaultView.RowFilter = string.Format("CONVERT(Type, 'System.String') NOT LIKE '3'");
            dataGridViewVolunteerFormGoods.DataSource = psGoods.goods.DefaultView;
            setGoodsTable();
        }
        #endregion
        #region Настройка параметров отображения таблицы "Предметы"
        private void setGoodsTable()
        {
            dataGridViewVolunteerFormGoods.Columns[0].Visible = false;
            dataGridViewVolunteerFormGoods.Columns[1].HeaderText = "Предмет";
            dataGridViewVolunteerFormGoods.Columns[2].HeaderText = "Тип";
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