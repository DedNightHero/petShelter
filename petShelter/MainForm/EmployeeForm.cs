﻿using System;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Configuration;
using System.Net;
using System.IO;

namespace MainForm
{
    public partial class EmployeeForm : Form
    {
        #region Объявление переменных
        PetShelter psAnimals = new PetShelter();
        PetShelter psSpecies = new PetShelter();
        PetShelter psUsers = new PetShelter();
        PetShelter psPositions = new PetShelter();
        PetShelter psGoods = new PetShelter();
        PetShelter psGoodstype = new PetShelter();
        PetShelter psDebitcredit = new PetShelter();
        BLogic.IBL ibl = new BLogic.BLogic();
        private int id;
        private int userLvl;
        private int userId;
        private string photoName = "";
        #endregion
        #region Создание и загрузка формы
        public EmployeeForm(int lvl, int id)
        {
            InitializeComponent();
            userLvl = lvl;
            userId = id;
        }

        private void EmployeeForm_Load(object sender, EventArgs e)
        {
            EmployeeForm_Resize(this, new EventArgs());
            initAnimalsTab();
            initStaffTab();
            initGoodsTab();
            initReportsTab();
            nothingAccept();
            privilegeSetting();
        }
        #endregion
        #region Изначально ничего не доступно
        private void nothingAccept()
        {
            ((Control)this.tabAnimals).Enabled = false;
            ((Control)this.tabStaff).Enabled = false;
            ((Control)this.tabGoods).Enabled = false;
            ((Control)this.tabReports).Enabled = false;
        }
        #endregion
        #region Разграничение прав пользователей
        private void privilegeSetting()
        {
            switch(userLvl)
            {
                case 2:
                    juniorPrivilege();
                    break;
                case 3:
                    middlePrivilege();
                    break;
                case 4:
                    seniorPrivilege();
                    break;
            }
        }
        #endregion
        #region Ограничения открытия вкладок для пользователей разных уровней
        private void tabMain_Selecting(object sender, TabControlCancelEventArgs e)
        {
            switch (userLvl)
            {
                case 2:
                    e.Cancel=e.TabPageIndex>1;
                    break;
                case 3:
                    e.Cancel = e.TabPageIndex==3;
                    break;
                case 4:
                    break;
            }
        }
        #endregion
        #region Права сотрудников разных уровней
        #region Права младшего сотрудника
        private void juniorPrivilege()
        {
            ((Control)this.tabAnimals).Enabled = true;
            ((Control)this.tabStaff).Enabled = true;
            pictureBoxPetPhoto.Enabled = false;
            buttonPetsSave.Enabled = false;
            buttonPetsDelete.Enabled = false;
            buttonStaffSave.Enabled = false;
            buttonStaffDelete.Enabled = false;
            dataGridViewStaffAllMembers.Enabled = false;
        }
        #endregion
        #region Права среднего сотрудника
        private void middlePrivilege()
        {
            ((Control)this.tabAnimals).Enabled = true;
            ((Control)this.tabGoods).Enabled = true;
            ((Control)this.tabStaff).Enabled = true;
            buttonStaffSave.Enabled = false;
            buttonStaffDelete.Enabled = false;
            dataGridViewStaffAllMembers.Enabled = false;
        }
        #endregion
        #region Права старшего сотрудника
        private void seniorPrivilege()
        {
            ((Control)this.tabAnimals).Enabled = true;
            ((Control)this.tabStaff).Enabled = true;
            ((Control)this.tabGoods).Enabled = true;
            ((Control)this.tabReports).Enabled = true;
        }
        #endregion
        #endregion
        #region Переключение между вкладками
        private void tabMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabMain.SelectedIndex == 0)
            {
                refreshAnimalsTab();
                return;
            }
            else if (tabMain.SelectedIndex == 1)
            {
                refreshStaffTab();
                return;
            }
            else if (tabMain.SelectedIndex == 2)
            {
                refreshGoodsTab();
                return;
            }
            else if (tabMain.SelectedIndex == 3)
            {
                cleanReportsFilterArea();
                refreshReportsTab();
                return;
            }
        }
        #endregion
        #region Изменение размера вкладок, при изменении размера окна приложения
        private void EmployeeForm_Resize(object sender, EventArgs e)
        {
            int tabWidth = tabMain.Width / tabMain.TabPages.Count - 1;
            if (tabWidth>0)
                tabMain.ItemSize = new Size(tabWidth - 1, tabMain.ItemSize.Height);
        }
        #endregion
        #region Генерация хэша
        private static string CreateMD5(string input)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
        #endregion
        #region Закрытие приложения при закрытии формы
        private void EmployeeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        #endregion
        #region ОКНО "Животные"
        #region Инициализация окна
        private void initAnimalsTab()
        {
            psAnimals = ibl.getAnimals();
            psSpecies = ibl.getSpecies();
            psGoods = ibl.getGoods();
            openFileDialogAddPetPhoto.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            comboBoxPetsSpecies.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxSortSpecies.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxPetsCure.DropDownStyle = ComboBoxStyle.DropDownList;
            for (int i = 0; i < psSpecies.species.Rows.Count; i++)
            {
                comboBoxSortSpecies.Items.Add(psSpecies.species.Rows[i][1].ToString());
                comboBoxPetsSpecies.Items.Add(psSpecies.species.Rows[i][1].ToString());
            }
            comboBoxSortSpecies.Items.Add("");
            for (int i = 0; i < psGoods.goods.Rows.Count; i++)
            {
                if (psGoods.goods.Rows[i][2].ToString()=="2")
                    comboBoxPetsCure.Items.Add(psGoods.goods.Rows[i][1].ToString());
            }
            dataGridViewPetsAllPets.DataSource = psAnimals;
            dataGridViewPetsAllPets.DataMember = "animals";
            dataGridViewPetsHistory.Columns.Add("goodsNameShow", "Препарат");
            dataGridViewPetsAllPets.Columns.Add("speciesShow", "Вид");
            setPetsGridView();
            setPetsCureHistory(-2);
        }
        #endregion
        #region Обновление окна
        private void refreshAnimalsTab()
        {
            psAnimals = ibl.getAnimals();
            psSpecies = ibl.getSpecies();
            psGoods = ibl.getGoods();
            dataGridViewPetsAllPets.DataSource = psAnimals;
            dataGridViewPetsAllPets.DataMember = "animals";
            setPetsGridView();
            comboBoxPetsSpecies.Items.Clear();
            comboBoxSortSpecies.Items.Clear();
            comboBoxPetsCure.Items.Clear();
            for (int i = 0; i < psSpecies.species.Rows.Count; i++)
            {
                comboBoxSortSpecies.Items.Add(psSpecies.species.Rows[i][1].ToString());
                comboBoxPetsSpecies.Items.Add(psSpecies.species.Rows[i][1].ToString());
            }
            comboBoxSortSpecies.Items.Add("");
            for (int i = 0; i < psGoods.goods.Rows.Count; i++)
            {
                if (psGoods.goods.Rows[i][2].ToString() == "2")
                    comboBoxPetsCure.Items.Add(psGoods.goods.Rows[i][1].ToString());
            }
            pictureBoxPetPhoto.Image = Properties.Resources.d1;
            dataGridViewPetsAllPets.Refresh();
            dataGridViewPetsAllPets.ClearSelection();
            cleanPetsAddArea();
            clearPetsAddCureArea();
            cleanPetsFilterArea();
            setPetsCureHistory(-2);
        }
        #endregion
        #region Параметры датагрида "Животные"
        private void setPetsGridView()
        {
            FillSpecies();
            dataGridViewPetsAllPets.ClearSelection();
            dataGridViewPetsAllPets.Columns["Id_Animals"].Visible = false;
            dataGridViewPetsAllPets.Columns["InHere"].Visible = false;
            dataGridViewPetsAllPets.Columns["FMLNameOfOwner"].Visible = false;
            dataGridViewPetsAllPets.Columns["OwnerPhone"].Visible = false;
            dataGridViewPetsAllPets.Columns["OwnerAddress"].Visible = false;
            dataGridViewPetsAllPets.Columns["PetPhoto"].Visible = false;
            dataGridViewPetsAllPets.Columns["Species"].Visible = false;
            dataGridViewPetsAllPets.Columns["speciesShow"].HeaderText = "Вид";
            dataGridViewPetsAllPets.Columns["NickName"].HeaderText = "Кличка";
            dataGridViewPetsAllPets.Columns["Breed"].HeaderText = "Порода";
            dataGridViewPetsAllPets.Columns["ArrivalDate"].HeaderText = "Дата поступления";
            dataGridViewPetsAllPets.Columns["DeliveryDate"].HeaderText = "Дата выдачи";
            dataGridViewPetsAllPets.Columns["speciesShow"].DisplayIndex = 0;
            dataGridViewPetsAllPets.Columns["Breed"].DisplayIndex = 1;
            dataGridViewPetsAllPets.Columns["NickName"].DisplayIndex = 2;
            dataGridViewPetsAllPets.Columns["ArrivalDate"].DisplayIndex = 3;
            dataGridViewPetsAllPets.Columns["DeliveryDate"].DisplayIndex = 4;

            dataGridViewPetsAllPets.Columns["Id_Animals"].DisplayIndex = 5;
            dataGridViewPetsAllPets.Columns["InHere"].DisplayIndex = 6;
            dataGridViewPetsAllPets.Columns["FMLNameOfOwner"].DisplayIndex = 7;
            dataGridViewPetsAllPets.Columns["OwnerPhone"].DisplayIndex = 8;
            dataGridViewPetsAllPets.Columns["OwnerAddress"].DisplayIndex = 9;
            dataGridViewPetsAllPets.Columns["PetPhoto"].DisplayIndex = 10;
            dataGridViewPetsAllPets.Columns["Species"].DisplayIndex = 11;
            dataGridViewPetsAllPets.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }
        #endregion
        #region Правильное отображение названий видов животных
        private void FillSpecies()
        {
            psSpecies = ibl.getSpecies();
            String speciesId;
            int rowCount = dataGridViewPetsAllPets.RowCount;
            string expression;

            for (int i = 0; i < rowCount; i++)
            {
                speciesId = dataGridViewPetsAllPets.Rows[i].Cells["Species"].Value.ToString();
                expression = "Id_Species = " + speciesId;
                System.Data.DataRow[] foundRows;
                foundRows = psSpecies.species.Select(expression);
                expression = foundRows[0][1].ToString();
                dataGridViewPetsAllPets.Rows[i].Cells["speciesShow"].Value = expression;
            }
        }
        private void PetsGridSorted(object sender, EventArgs e)
        {
            FillSpecies();

        }
        #endregion
        #region Выбор элемента в датагриде "Животные"
        private void dataGridViewPetsAllPets_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            setPetsAddArea(e.RowIndex);
            setPetsCureHistory(id);
        }
        private void dataGridViewPetsAllPets_KeyUp(object sender, KeyEventArgs e)
        {
            setPetsAddArea(dataGridViewPetsAllPets.CurrentRow.Index);
            setPetsCureHistory(id);
        }
        private void dataGridViewPetsAllPets_KeyDown(object sender, KeyEventArgs e)
        {
            setPetsAddArea(dataGridViewPetsAllPets.CurrentRow.Index);
            setPetsCureHistory(id);
        }
        #endregion
        #region Кнопка создания или изменения информации о животном
        private bool checkIfAnimalExists(int i, string b, string nn)
        {
            //проверка есть ли такое животное в базе
            PetShelter _psAnimals = ibl.getAnimals();

            System.Data.DataRow[] foundRows = _psAnimals.animals.Select(
                "Species = '" + i + "' and " +
                "Breed LIKE '" + b + "' and " +
                "NickName LIKE '" + nn + "'");
            if (foundRows.Length > 0)
            {
                MessageBox.Show("Такое животное уже существует!\nПопробуйте изменить некоторые поля или измените существующее животное.");
                return true;
            }
            return false;
        }

        private void buttonPetsSave_Click(object sender, EventArgs e)
        {
            if (checkBoxPetsNewAnimal.Checked) //Добавить животное в бд
            {          
                bool f = checkPetsAddArea();
                if (f)
                {
                    int i = Convert.ToInt32(psSpecies.species.Rows[comboBoxPetsSpecies.SelectedIndex][0]);
                    string b = textBoxPetsBreed.Text;
                    string nn = textBoxPetsNickName.Text;
                    if (checkIfAnimalExists(i, b, nn)) return;
                    DateTime ad = dateTimePickerPetsArrivalDate.Value;
                    ad = ad.Date;
                    string pp;
                    if (photoName != "")
                        pp = photoName;
                    else
                        pp = null;
                    if (checkBoxPetsMaster.Checked)
                    {
                        string fio = textBoxPetsFIO.Text;
                        string pn = textBoxPetsPhoneNumber.Text;
                        string a = textBoxPetsAddress.Text;
                        DateTime dd = dateTimePickerPetsDeliveryDay.Value;
                        dd = dd.Date;                  
                        psAnimals.animals.AddanimalsRow(i, nn, b, ad, 0, fio, pn, a, dd, pp);
                    }
                    else
                        psAnimals.animals.AddanimalsRow(i, nn, b, ad, 1, null, null, null, default(DateTime), pp);
                    ibl.setAnimals(psAnimals);
                    cleanPetsAddArea();
                    clearPetsAddCureArea();
                }
            }
            else //Редактирование информации о животном
            {
                if (dataGridViewPetsAllPets.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Невозможно изменить животное, если оно не выбрано.\nВыберите животное из таблицы или создайте новое.");
                    return;
                }
                bool f = checkPetsAddArea();
                if (f)
                {
                        changePetInfo();
                        clearPetsAddCureArea();
                        cleanPetsAddArea();
                }
            }
            refreshAnimalsTab();
        }
        
        #endregion
        #region Очистка области внесения препарата животному
        private void clearPetsAddCureArea()
        {
            comboBoxPetsCure.SelectedIndex=-1;
            textBoxPetsComment.Text = "";
            setPetsCureHistory(-1);
        }
        #endregion
        #region Очистка фильтра
        private void cleanPetsFilterArea()
        {
            comboBoxSortSpecies.SelectedIndex = -1;
            textBoxPetsSortBreed.Text = "";
            textBoxPetsSortNickName.Text = "";
            checkBoxPetsIsAtHome.Checked = false;
            checkBoxPetsIsAtShelter.Checked = false;
        }
        #endregion
        #region Кнопка выделения лекарства животному
        private void buttonPetsAddCure_Click(object sender, EventArgs e)
        {
            if(dataGridViewPetsAllPets.SelectedRows.Count!=1)
            {
                MessageBox.Show("Нужно выбрать питомца из списка", "Ошибка", MessageBoxButtons.OK);
                return;
            }
            psDebitcredit = ibl.getDebitCredit();
            psGoods = ibl.getGoods();
            int gn = 0;
            if (comboBoxPetsCure.Text == "")
            {
                MessageBox.Show("Нужно выбрать препарат", "Ошибка", MessageBoxButtons.OK);
                return;
            }
            else if (textBoxPetsComment.Text == "")
            {
                MessageBox.Show("Нужно указать причину выдачи препарата", "Ошибка", MessageBoxButtons.OK);
                return;
            }
            for (int i = 0; i < psGoods.goods.Rows.Count; i++)
            {
                if (psGoods.goods.Rows[i][1].ToString()==comboBoxPetsCure.Text)
                {
                    int x = Convert.ToInt32(psGoods.goods.Rows[i][3].ToString()) - 1;
                    if (x>=0)
                    {
                        psGoods.goods.Rows[i][3] = x;
                        gn = Convert.ToInt32(psGoods.goods.Rows[i][0].ToString());
                        ibl.setGoods(psGoods);
                        break;
                    }
                    else
                    {
                        MessageBox.Show("Препарата нет в наличии", "Ошибка", MessageBoxButtons.OK);
                        clearPetsAddCureArea();
                        return;
                    }
                }
            }
            psDebitcredit.debitcredit.AdddebitcreditRow(gn, textBoxPetsComment.Text, DateTime.Today, 1, 0, id, userId, 2);
            ibl.setDebitCredit(psDebitcredit);
            clearPetsAddCureArea();
            cleanPetsAddArea();
            cleanPetsFilterArea();
            dataGridViewPetsHistory.Refresh();
            dataGridViewPetsAllPets.ClearSelection();
        }
        #endregion
        #region Вспомогательные функции для Области добавления питомцев
        private bool checkPetsAddArea()
        {
            if (comboBoxPetsSpecies.Text == "")
            {
                MessageBox.Show("Введите вид животного");
                return false;
            }
            else if (textBoxPetsBreed.Text == "")
            {
                MessageBox.Show("Введите породу животного");
                return false;
            }
            else if (textBoxPetsNickName.Text == "")
            {
                MessageBox.Show("Введите кличку животного");
                return false;
            }
            else if (dateTimePickerPetsArrivalDate.Text == "")
            {
                MessageBox.Show("Введите дату поступления животного");
                return false;
            }
            else if (checkBoxPetsMaster.Checked && textBoxPetsFIO.Text == "")
            {
                MessageBox.Show("Введите фамилию владельца");
                return false;
            }
            else if (checkBoxPetsMaster.Checked && textBoxPetsPhoneNumber.Text == "")
            {
                MessageBox.Show("Введите телефон владельца");
                return false;
            }
            else if (checkBoxPetsMaster.Checked && textBoxPetsAddress.Text == "")
            {
                MessageBox.Show("Введите адрес владельца");
                return false;
            }
            else if (checkBoxPetsMaster.Checked && dateTimePickerPetsDeliveryDay.Text == "")
            {
                MessageBox.Show("Введите дату выдачи питомца владельцу");
                return false;
            }
            return true;
        }

        private void cleanPetsAddArea()
        {
            comboBoxPetsSpecies.SelectedIndex = -1;
            textBoxPetsBreed.Text = "";
            textBoxPetsNickName.Text = "";
            checkBoxPetsMaster.Checked = false;
            dateTimePickerPetsArrivalDate.Value = DateTime.Today;
            dateTimePickerPetsDeliveryDay.Value = DateTime.Today;
            textBoxPetsFIO.Text = "";
            textBoxPetsPhoneNumber.Text = "";
            textBoxPetsAddress.Text = "";
            checkBoxPetsNewAnimal.Checked = false;
            photoName = "";
            pictureBoxPetPhoto.Image= Properties.Resources.d1;
        }
        #endregion
        #region Установка параметров животного для редактирования
        private void setPetsAddArea(int i)
        {
            //cleanPetsAddArea();
            if (i>=0)
            {
                id=Convert.ToInt32(dataGridViewPetsAllPets.CurrentRow.Cells["Id_Animals"].Value);
                comboBoxPetsSpecies.SelectedItem = psSpecies.species.Rows[Convert.ToInt32(psAnimals.animals.FindById_Animals(id)["Species"])-1]["NameOfSpecies"].ToString();
                textBoxPetsNickName.Text = psAnimals.animals.FindById_Animals(id)["NickName"].ToString();
                textBoxPetsBreed.Text = psAnimals.animals.FindById_Animals(id)["Breed"].ToString();
                dateTimePickerPetsArrivalDate.Text = psAnimals.animals.FindById_Animals(id)["ArrivalDate"].ToString();
                if (psAnimals.animals.FindById_Animals(id)["PetPhoto"].ToString() == "")
                    pictureBoxPetPhoto.Image = Properties.Resources.d1;
                else
                {
                   pictureBoxPetPhoto.Image = null;
                   pictureBoxPetPhoto.ImageLocation = psAnimals.animals.FindById_Animals(id)["PetPhoto"].ToString();
                }
                if (psAnimals.animals.FindById_Animals(id)["InHere"].ToString() == "0")
                {
                    checkBoxPetsMaster.Checked = true;
                    textBoxPetsFIO.Text = psAnimals.animals.FindById_Animals(id)["FMLNameOfOwner"].ToString();
                    textBoxPetsPhoneNumber.Text = psAnimals.animals.FindById_Animals(id)["OwnerPhone"].ToString();
                    textBoxPetsAddress.Text = psAnimals.animals.FindById_Animals(id)["OwnerAddress"].ToString();
                    dateTimePickerPetsDeliveryDay.Text = psAnimals.animals.FindById_Animals(id)["DeliveryDate"].ToString();
                }
                else
                {
                    checkBoxPetsMaster.Checked = false;
                    textBoxPetsFIO.Text = "";
                    textBoxPetsPhoneNumber.Text = "";
                    textBoxPetsAddress.Text = "";
                    dateTimePickerPetsDeliveryDay.Text = "";
                }
            }
        }
        #endregion
        #region Установка истории лечения для животного
        private void setPetsCureHistory(int id)
        {
            psDebitcredit.debitcredit.DefaultView.RowFilter = string.Format("CONVERT(PatientId, 'System.String') LIKE '" + id + "'");
            dataGridViewPetsHistory.DataSource = psDebitcredit.debitcredit.DefaultView;

            FillGoods();

            dataGridViewPetsHistory.Columns["Id_DebitCredit"].Visible = false;
            dataGridViewPetsHistory.Columns["GoodsName"].Visible = false;
            dataGridViewPetsHistory.Columns["Debit"].Visible = false;
            dataGridViewPetsHistory.Columns["Credit"].Visible = false;
            dataGridViewPetsHistory.Columns["PatientId"].Visible = false;
            dataGridViewPetsHistory.Columns["UserId"].Visible = false;
            dataGridViewPetsHistory.Columns["GoodsType"].Visible = false;
            dataGridViewPetsHistory.Columns["goodsNameShow"].HeaderText = "Препарат";
            dataGridViewPetsHistory.Columns["Comment"].HeaderText = "Комментарий";
            dataGridViewPetsHistory.Columns["Date"].HeaderText = "Дата";
            dataGridViewPetsHistory.Columns["Date"].DisplayIndex = 0;
            dataGridViewPetsHistory.Columns["goodsNameShow"].DisplayIndex = 1;
            dataGridViewPetsHistory.Columns["Comment"].DisplayIndex = 2;
            dataGridViewPetsHistory.Columns["Id_DebitCredit"].DisplayIndex = 3;
            dataGridViewPetsHistory.Columns["GoodsName"].DisplayIndex = 4;
            dataGridViewPetsHistory.Columns["Debit"].DisplayIndex = 5;
            dataGridViewPetsHistory.Columns["Credit"].DisplayIndex = 6;
            dataGridViewPetsHistory.Columns["PatientId"].DisplayIndex = 7;
            dataGridViewPetsHistory.Columns["UserId"].DisplayIndex = 8;
            dataGridViewPetsHistory.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewPetsHistory.ClearSelection();
        }
        #endregion
        #region Правильное отображение названий предметов
        private void FillGoods()
        {
            psGoods = ibl.getGoods();
            String goodsId;
            int rowCount = dataGridViewPetsHistory.RowCount;
            string expression;

            for (int i = 0; i < rowCount; i++)
            {
                goodsId = dataGridViewPetsHistory.Rows[i].Cells["GoodsName"].Value.ToString();
                expression = "Id_Goods = " + goodsId;
                System.Data.DataRow[] foundRows;
                foundRows = psGoods.goods.Select(expression);
                expression = foundRows[0][1].ToString();
                dataGridViewPetsHistory.Rows[i].Cells["goodsNameShow"].Value = expression;
            }
        }
        private void dataGridViewPetsHistory_Sorted(object sender, EventArgs e)
        {
            FillGoods();
        }
        #endregion
        #region Изменение информации о животном в датасете
        private void changePetInfo()
        {
            
            psAnimals.animals.FindById_Animals(id)[1] = Convert.ToInt32(psSpecies.species.Rows[comboBoxPetsSpecies.SelectedIndex][0]);
            psAnimals.animals.FindById_Animals(id)[2] = textBoxPetsNickName.Text;
            psAnimals.animals.FindById_Animals(id)[3] = textBoxPetsBreed.Text;
            psAnimals.animals.FindById_Animals(id)[4] = dateTimePickerPetsArrivalDate.Value;
            if (checkBoxPetsMaster.Checked)
            {
                psAnimals.animals.FindById_Animals(id)[5] = 0;
                psAnimals.animals.FindById_Animals(id)[6] = textBoxPetsFIO.Text;
                psAnimals.animals.FindById_Animals(id)[7] = textBoxPetsPhoneNumber.Text;
                psAnimals.animals.FindById_Animals(id)[8] = textBoxPetsAddress.Text;
                psAnimals.animals.FindById_Animals(id)[9] = dateTimePickerPetsDeliveryDay.Value;
            }
            else
            {
                psAnimals.animals.FindById_Animals(id)[5] = 1;
                psAnimals.animals.FindById_Animals(id)[6] = null;
                psAnimals.animals.FindById_Animals(id)[7] = null;
                psAnimals.animals.FindById_Animals(id)[8] = null;
                psAnimals.animals.FindById_Animals(id)[9] = default(DateTime);
            }
            if (photoName != "")
                psAnimals.animals.FindById_Animals(id)[10] = photoName;
            else
                psAnimals.animals.FindById_Animals(id)[10] = psAnimals.animals.FindById_Animals(id)[10];
            ibl.setAnimals(psAnimals);
        }
        #endregion
        #region Поведение при нажатие на чекбокс "Есть хозяин"
        private void checkBoxPetsMaster_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxPetsMaster.Checked)
            {
                textBoxPetsFIO.Enabled = true;
                textBoxPetsPhoneNumber.Enabled = true;
                textBoxPetsAddress.Enabled = true;
                dateTimePickerPetsDeliveryDay.Enabled = true;  
            }
            else
            {
                textBoxPetsFIO.Enabled = false;
                textBoxPetsPhoneNumber.Enabled = false;
                textBoxPetsAddress.Enabled = false;
                dateTimePickerPetsDeliveryDay.Enabled = false;
            }
        }
        #endregion
        #region Кнопка удаления записи из таблицы
        private void buttonPetsDelete_Click(object sender, EventArgs e)
        {
            if(dataGridViewPetsAllPets.SelectedRows.Count>0)
            {
                psAnimals.animals.FindById_Animals(id).Delete();
                ibl.setAnimals(psAnimals);
                cleanPetsAddArea();
                clearPetsAddCureArea();
                refreshAnimalsTab();
            }    
            else
            {
                MessageBox.Show("Сначала выберите животное в таблице");
            }  
        }
        #endregion
        #region Фильтрация таблицы Животных согласно указанным критериям
        private void sortPetsTable(object sender, EventArgs e)
        {
            String filter = null;
            if (checkBoxPetsIsAtHome.Checked) filter = "(CONVERT(InHere, 'System.String') LIKE '0'";
            if (checkBoxPetsIsAtShelter.Checked)
            {
                if (filter != null) filter += " or ";
                else filter += "(";
                filter += "CONVERT(InHere, 'System.String') LIKE '1')";
            }
            else {
                if (filter != null) filter += " ) ";
            }
            if (textBoxPetsSortBreed.Text != "")
            {
                if (filter != null) filter += " and ";
                filter += "Breed LIKE '*" +textBoxPetsSortBreed.Text + "*'";
            }
            if (textBoxPetsSortNickName.Text != "")
            {
                if (filter != null) filter += " and ";
                filter += "NickName LIKE '*" +textBoxPetsSortNickName.Text + "*'";
            }
            if (comboBoxSortSpecies.Text != "")
            {
                if (filter != null) filter += " and ";
                filter += "CONVERT(Species, 'System.String') LIKE '" + psSpecies.species.Rows[comboBoxSortSpecies.SelectedIndex][0].ToString() + "'";
            }
            if (filter != null)
            {
                psAnimals.animals.DefaultView.RowFilter = string.Format(filter);
                dataGridViewPetsAllPets.DataSource = psAnimals.animals.DefaultView;
            }
            else
            {
                dataGridViewPetsAllPets.DataSource = psAnimals;
                dataGridViewPetsAllPets.DataMember = "animals";
                setPetsGridView();
            }
            setPetsGridView();
            dataGridViewPetsAllPets.ClearSelection();
            cleanPetsAddArea();
            clearPetsAddCureArea();
        }
        #endregion
        #region Загрузка фото на сервер
        private void UploadFileToFTP(string strn, string strp)
        {
            FtpWebRequest ftpReq = (FtpWebRequest)WebRequest.Create(ConfigurationManager.AppSettings["ftpaddress"] + strn);

            ftpReq.UseBinary = true;
            ftpReq.Method = WebRequestMethods.Ftp.UploadFile;
            ftpReq.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["ftpUserName"], ConfigurationManager.AppSettings["ftpUserPass"]);

            byte[] b = File.ReadAllBytes(strp);
            ftpReq.ContentLength = b.Length;
            using (Stream s = ftpReq.GetRequestStream())
            {
                s.Write(b, 0, b.Length);
            }

            FtpWebResponse ftpResp = (FtpWebResponse)ftpReq.GetResponse();

            if (ftpResp != null)
            {
                if (ftpResp.StatusDescription.StartsWith("226"))
                {
                    MessageBox.Show("Фото успешно загружено на сервер", "Сообщение", MessageBoxButtons.OK);
                }
            }
        }
        #endregion
        #region Добавление фото питомца
        private void pictureBoxPetPhoto_Click(object sender, EventArgs e)
        {
            if (openFileDialogAddPetPhoto.ShowDialog() == DialogResult.Cancel)
                return;
            UploadFileToFTP(openFileDialogAddPetPhoto.SafeFileName, openFileDialogAddPetPhoto.FileName);
            photoName = ConfigurationManager.AppSettings["ftpaddress"] + openFileDialogAddPetPhoto.SafeFileName;
        }
        #endregion
        #region Разрешенные символы в поле "телефон"
        private void textBoxPetsPhoneNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            char symbol = e.KeyChar;
            if (!Char.IsDigit(symbol) && symbol != 8 && symbol != 1 && symbol != 3 && symbol != 22 && symbol != 24 && symbol != 9)
                e.Handled = true;
        }
        #endregion
        #endregion
        #region ОКНО "Персонал"
        #region Инициализация окна
        private void initStaffTab()
        {
            psUsers = ibl.getUsers();
            psPositions = ibl.getPositions();
            psDebitcredit = ibl.getDebitCredit();
            
            comboBoxStaffVacancy.DropDownStyle = ComboBoxStyle.DropDownList;
            for (int i = 0; i < psPositions.positions.Rows.Count; i++)
            {
                comboBoxStaffVacancy.Items.Add(psPositions.positions.Rows[i][1].ToString());
            }
            comboBoxStaffVacancy.Items.Add("");
            psDebitcredit.debitcredit.DefaultView.RowFilter = string.Format("CONVERT(UserId, 'System.String') LIKE '" + userId + "' and CONVERT(PatientId, 'System.String') IS NULL and CONVERT(Debit, 'System.String') LIKE '0' and CONVERT(Credit, 'System.String') LIKE '0' and Date ='"+DateTime.Today.Date.ToString("dd.MM.yyyy")+"'");
            dataGridViewStaffCharity.DataSource = psDebitcredit.debitcredit.DefaultView;
            setStaffCharity();
            dataGridViewStaffAllMembers.DataSource = psUsers;
            dataGridViewStaffAllMembers.DataMember = "users";
            dataGridViewStaffAllMembers.Columns.Add("positionsName", "Должность");
            setStaffGridView();
        }
        #endregion
        #region Обновление окна
        private void refreshStaffTab()
        {
            psUsers = ibl.getUsers();
            psPositions = ibl.getPositions();
            psDebitcredit = ibl.getDebitCredit();
            comboBoxStaffVacancy.Items.Clear();
            for (int i = 0; i < psPositions.positions.Rows.Count; i++)
            {
                comboBoxStaffVacancy.Items.Add(psPositions.positions.Rows[i][1].ToString());
            }
            comboBoxStaffVacancy.Items.Add("");
            clearStaffFilter();
            cleanStaffAddArea();
            clearStaffAddMoveArea();
            dataGridViewStaffAllMembers.DataSource = psUsers;
            dataGridViewStaffAllMembers.DataMember = "users";
            setStaffGridView();
            psDebitcredit.debitcredit.DefaultView.RowFilter = string.Format("CONVERT(UserId, 'System.String') LIKE '" + userId + "' and CONVERT(PatientId, 'System.String') IS NULL and CONVERT(Debit, 'System.String') LIKE '0' and CONVERT(Credit, 'System.String') LIKE '0' and Date ='" + DateTime.Today.Date.ToString("dd.MM.yyyy") + "'");
            dataGridViewStaffCharity.DataSource = psDebitcredit.debitcredit.DefaultView;
            setStaffCharity();
            dataGridViewStaffAllMembers.Refresh();
            dataGridViewStaffCharity.ClearSelection();
            dataGridViewStaffAllMembers.ClearSelection();   
        }
        private void sorted(object sender, EventArgs e) //Вызывается при сортировки столбцов
        {
            FillPositions();
        }
        #endregion
        #region Параметры датагрида "Персонал"
        private void setStaffGridView()
        {
            FillPositions();
            dataGridViewStaffAllMembers.ClearSelection();
            dataGridViewStaffAllMembers.Columns["Id_Users"].Visible = false;
            dataGridViewStaffAllMembers.Columns["Password"].Visible = false;
            dataGridViewStaffAllMembers.Columns["Position"].Visible = false;
            dataGridViewStaffAllMembers.Columns["Login"].HeaderText = "Логин";
            dataGridViewStaffAllMembers.Columns["FirstMiddleLastName"].HeaderText = "ФИО";
            dataGridViewStaffAllMembers.Columns["Phone"].HeaderText = "Телефон";
            dataGridViewStaffAllMembers.Columns["Address"].HeaderText = "Адрес";

            dataGridViewStaffAllMembers.Columns["Login"].DisplayIndex = 1;
            dataGridViewStaffAllMembers.Columns["FirstMiddleLastName"].DisplayIndex = 0;
            dataGridViewStaffAllMembers.Columns["Phone"].DisplayIndex = 5;
            dataGridViewStaffAllMembers.Columns["Address"].DisplayIndex = 4;
            dataGridViewStaffAllMembers.Columns["positionsName"].DisplayIndex = 3;  
                      
            dataGridViewStaffAllMembers.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }
        private void FillPositions()
        {
            psPositions = ibl.getPositions();
            String positionsId;
            int rowCount = dataGridViewStaffAllMembers.RowCount;
            string expression;

            for (int i = 0; i < rowCount; i++)
            {
                positionsId = dataGridViewStaffAllMembers.Rows[i].Cells["Position"].Value.ToString();
                expression = "Id_Positions = " + positionsId;
                System.Data.DataRow[] foundRows;
                // Use the Select method to find all rows matching the filter.
                foundRows = psPositions.positions.Select(expression);
                expression = foundRows[0][1].ToString();
                dataGridViewStaffAllMembers.Rows[i].Cells["positionsName"].Value = expression;         
            }
        }
        #endregion
        #region Очистка фильтра
        private void clearStaffFilter()
        {
            textBoxStaffSortFIO.Text = "";
            textBoxStaffSortAdres.Text = "";
            textBoxStafSortfLogin.Text = "";
            checkBoxStaffIsOnStaff.Checked = false;
        }
        #endregion
        #region Выбор элемента в датагриде "Персонал"
        private void dataGridViewStaffAllMembers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            setStaffAddArea(e.RowIndex);
        }
        private void dataGridViewStaffAllMembers_KeyUp(object sender, KeyEventArgs e)
        {
            setStaffAddArea(dataGridViewStaffAllMembers.CurrentRow.Index);
        }
        private void dataGridViewStaffAllMembers_KeyDown(object sender, KeyEventArgs e)
        {
            setStaffAddArea(dataGridViewStaffAllMembers.CurrentRow.Index);
        }
        #endregion
        #region Установка информации о выбранном сотруднике
        private void setStaffAddArea(int i)
        {
            //cleanStaffAddArea();
            if (i >= 0)
            {
                id = Convert.ToInt32(dataGridViewStaffAllMembers.CurrentRow.Cells["Id_Users"].Value);
                if (id!=1)
                {
                    comboBoxStaffVacancy.SelectedItem = psPositions.positions.Rows[Convert.ToInt32(psUsers.users.FindById_Users(id)[4]) - 1][1].ToString();
                    textBoxStaffLogin.Text = psUsers.users.FindById_Users(id)[1].ToString();
                    textBoxStaffLastName.Text = psUsers.users.FindById_Users(id)[3].ToString().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0];
                    textBoxStaffFirstName.Text = psUsers.users.FindById_Users(id)[3].ToString().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1];
                    textBoxStaffMiddleName.Text = psUsers.users.FindById_Users(id)[3].ToString().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[2];
                    textBoxStaffPhoneNumber.Text = psUsers.users.FindById_Users(id)[5].ToString();
                    textBoxStaffAddress.Text = psUsers.users.FindById_Users(id)[6].ToString();
                }
                else
                {
                    MessageBox.Show("Данного пользователя изменять запрещено", "Ошибка", MessageBoxButtons.OK);
                    dataGridViewStaffAllMembers.ClearSelection();
                }
            }
        }
        #endregion
        #region Установка благих дел сотрудника
        private void setStaffCharity()
        {
            dataGridViewStaffCharity.Columns[0].Visible = false;
            dataGridViewStaffCharity.Columns[1].Visible = false;
            dataGridViewStaffCharity.Columns[4].Visible = false;
            dataGridViewStaffCharity.Columns[5].Visible = false;
            dataGridViewStaffCharity.Columns[6].Visible = false;
            dataGridViewStaffCharity.Columns[7].Visible = false;
            dataGridViewStaffCharity.Columns["GoodsType"].Visible = false;
            dataGridViewStaffCharity.Columns[2].HeaderText = "Комментарий";
            dataGridViewStaffCharity.Columns[3].HeaderText = "Дата";
            dataGridViewStaffCharity.Columns[3].DisplayIndex = 0;
            dataGridViewStaffCharity.Columns[2].DisplayIndex = 1;
            dataGridViewStaffCharity.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewStaffCharity.ClearSelection();
        }
        #endregion
        #region Очистка области редактирования информации о персонале
        private void cleanStaffAddArea()
        {
            comboBoxStaffVacancy.SelectedIndex = -1;
            textBoxStaffLogin.Text = "";
            textBoxStaffPass.Text = "";
            textBoxStaffPass.Enabled = true;
            textBoxStaffLastName.Text = "";
            textBoxStaffFirstName.Text = "";
            textBoxStaffMiddleName.Text = "";
            textBoxStaffPhoneNumber.Text = "";
            textBoxStaffAddress.Text = "";
            checkBoxStaffNewMember.Checked = false;
        }
        
        
        #endregion
        #region Фильтрация таблицы "Персонал" согласно указанным критериям
        private void sortStaffTable(object sender, EventArgs e)
        {
            String filter = null;
            if (checkBoxStaffIsOnStaff.Checked) filter = "Position >=2";
            if (textBoxStaffSortFIO.Text != "")
            {
                if (filter != null) filter += " and ";
                filter += "FirstMiddleLastName LIKE '*" + textBoxStaffSortFIO.Text + "*'";
            }
            if (textBoxStafSortfLogin.Text != "")
            {
                if (filter != null) filter += " and ";
                filter += "Login LIKE '*" + textBoxStafSortfLogin.Text + "*'";
            }
            if (textBoxStaffSortAdres.Text != "")
            {
                if (filter != null) filter += " and ";
                filter += "Address LIKE '*" + textBoxStaffSortAdres.Text + "*'";
            }
            if (filter != null)
            {
                psUsers.users.DefaultView.RowFilter = string.Format(filter);
                dataGridViewStaffAllMembers.DataSource = psUsers.users.DefaultView;
            }
            else
            {
                dataGridViewStaffAllMembers.DataSource = psUsers;
                dataGridViewStaffAllMembers.DataMember = "users";
                setStaffGridView();
            }
            setStaffGridView();
            dataGridViewStaffAllMembers.ClearSelection();
            cleanStaffAddArea();
        }
        #endregion
        #region Волонтёру пароль не нужен
        private void comboBoxStaffVacancy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxStaffVacancy.Text == "Волонтёр")
            {
                textBoxStaffPass.Enabled = false;
                textBoxStaffPass.Text = "";
            }
            else
                textBoxStaffPass.Enabled = true;
        }
        #endregion
        #region Проверка введенных данных при добавлении сотрудника
        private bool checkIfUserExists()
        {
            //проверка есть ли уже такой предмет.
            PetShelter _psUsers = ibl.getUsers();
            System.Data.DataRow[] foundRows = _psUsers.users.Select("Login LIKE '" + textBoxStaffLogin.Text.ToString() + "'");            
            if (foundRows.Length > 0)
            {
                MessageBox.Show("Пользователь с таким логином уже существует!\nПопробуйте сменить логин или изменить существующего пользователя.");
                return true;
            }
            return false;
        }
        private bool checkStaffAddArea()
        {
            

            if (textBoxStaffLogin.Text == "")
            {
                MessageBox.Show("Введите логин");
                return false;
            }
            else if (textBoxStaffPass.Text == "" && checkBoxStaffNewMember.Checked && comboBoxStaffVacancy.Text != "Волонтёр")
            {
                MessageBox.Show("Введите пароль");
                return false;
            }
            else if (textBoxStaffLastName.Text == "")
            {
                MessageBox.Show("Введите фамилию");
                return false;
            }
            else if (textBoxStaffFirstName.Text == "")
            {
                MessageBox.Show("Введите имя");
                return false;
            }
            else if (textBoxStaffMiddleName.Text == "")
            {
                MessageBox.Show("Введите отчество");
                return false;
            }
            else if (comboBoxStaffVacancy.Text == "")
            {
                MessageBox.Show("Введите должность сотрудника");
                return false;
            }
            else if (textBoxStaffPhoneNumber.Text == "")
            {
                MessageBox.Show("Введите номер телефона сотрудника");
                return false;
            }
            else if (textBoxStaffAddress.Text == "")
            {
                MessageBox.Show("Введите адрес жительства сотрудника");
                return false;
            }
           

             /*   PetShelter _psUsers = ibl.getUsers();
                System.Data.DataRow[] foundRows = _psUsers.users.Select("Login LIKE '" + textBoxStaffLogin.Text.ToString() + "' and Position <= " + userLvl);            
                if (foundRows.Length > 0)
                {
                    MessageBox.Show("Вы не можете редактировать данного пользователя.\nВы можете редактировать пользователей с правами вашего уровня и ниже.");
                    return false;
                }
             */

            return true;
        }
        #endregion
        #region Кнопка создания или изменения информации о сотруднике
        private void buttonStaffSave_Click(object sender, EventArgs e)
        {
            if (checkBoxStaffNewMember.Checked) //Добавить сотрудника в бд
            {              
                bool f = checkStaffAddArea();
                if (f && !checkIfUserExists())
                {

                    string l = textBoxStaffLogin.Text;
                    string fio = textBoxStaffLastName.Text + ' ' + textBoxStaffFirstName.Text + ' ' + textBoxStaffMiddleName.Text;
                    int pos = Convert.ToInt32(psPositions.positions.Rows[comboBoxStaffVacancy.SelectedIndex][0]);
                    string phone = textBoxStaffPhoneNumber.Text;
                    string address = textBoxStaffAddress.Text;
                    if (comboBoxStaffVacancy.Text!= "Волонтёр")
                    {
                        string pass = CreateMD5(textBoxStaffPass.Text);
                        psUsers.users.AddusersRow(l, pass, fio, pos, phone, address);
                    }
                    else
                    {
                        psUsers.users.AddusersRow(l, null, fio, pos, phone, address);
                    }
                    ibl.setUsers(psUsers);
                }
            }
            else //Редактирование информации о сотруднике
            {
                bool f = checkStaffAddArea();
                if (f)
                {
                    changeStaffInfo();
                }
            }
            refreshStaffTab();
        }
        #region Изменение информации о сотруднике в датасете
        private void changeStaffInfo()
        {

            psUsers.users.FindById_Users(id)[1] = textBoxStaffLogin.Text;
            psUsers.users.FindById_Users(id)[3] = textBoxStaffLastName.Text + ' ' + textBoxStaffFirstName.Text + ' ' + textBoxStaffMiddleName.Text;
            psUsers.users.FindById_Users(id)[4] = Convert.ToInt32(psPositions.positions.Rows[comboBoxStaffVacancy.SelectedIndex][0]);
            psUsers.users.FindById_Users(id)[5] = textBoxStaffPhoneNumber.Text;
            psUsers.users.FindById_Users(id)[6] = textBoxStaffAddress.Text;
            if (comboBoxStaffVacancy.Text == "Волонтёр")
            {
                psUsers.users.FindById_Users(id)[2] = null;
            }
            else if (textBoxStaffPass.Text == "" && comboBoxStaffVacancy.Text != "Волонтёр")
                psUsers.users.FindById_Users(id)[2] = psUsers.users.FindById_Users(id)[2].ToString();
            else
            {
                psUsers.users.FindById_Users(id)[2] = CreateMD5(textBoxStaffPass.Text);
            }
            ibl.setUsers(psUsers);
        }
        #endregion
        #endregion
        #region Кнопка удаления записи из таблицы
        private void buttonStaffDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewStaffAllMembers.SelectedRows.Count > 0)
            {
                psUsers.users.FindById_Users(id).Delete();
                ibl.setUsers(psUsers);
                refreshStaffTab();
            }
            else
            {
                MessageBox.Show("Сначала выберите сотрудника в таблице");
            }
        }
        #endregion
        #region Кнопка добавления благого дела пользователя
        private void buttonStaffOk_Click(object sender, EventArgs e)
        {
            psDebitcredit = ibl.getDebitCredit();
            if (textBoxStaffComment.Text == "")
            {
                MessageBox.Show("Нужно указать произведенное действие", "Ошибка", MessageBoxButtons.OK);
                return;
            }
            psDebitcredit.debitcredit.AdddebitcreditRow(-1, textBoxStaffComment.Text, DateTime.Today, 0, 0, -1, userId,-1);
            ibl.setDebitCredit(psDebitcredit);
            psDebitcredit = ibl.getDebitCredit();
            psDebitcredit.debitcredit.DefaultView.RowFilter = string.Format("CONVERT(UserId, 'System.String') LIKE '" + userId + "' and CONVERT(PatientId, 'System.String') IS NULL and CONVERT(Debit, 'System.String') LIKE '0' and CONVERT(Credit, 'System.String') LIKE '0' and Date ='" + DateTime.Today.Date.ToString("dd.MM.yyyy") + "'");
            dataGridViewStaffCharity.DataSource = psDebitcredit.debitcredit.DefaultView;
            clearStaffAddMoveArea();
            setStaffCharity();
            dataGridViewStaffCharity.ClearSelection();
        }
        #endregion
        #region Очистка области добавления благого дела сотрудника
        private void clearStaffAddMoveArea()
        {
            textBoxStaffComment.Text = "";
        }
        #endregion
        #region Разрешенные символы в поле "телефон"
        private void textBoxStaffPhoneNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            char symbol = e.KeyChar;
            if (!Char.IsDigit(symbol) && symbol != 8 && symbol != 1 && symbol != 3 && symbol != 22 && symbol != 24 && symbol != 9)
                e.Handled = true;
        }
        #endregion
        #region Запрет пробелов в поле ввода ФИО
        private void textBoxesStaffFIO_keypress(object sender, KeyPressEventArgs e)
        {
            char symbol = e.KeyChar;
            if (symbol==(int)Keys.Space || symbol == 22)
                e.Handled = true;
        }
        #endregion
        #endregion
        #region ОКНО "Материальная база"
        #region Инициализация окна
        private void initGoodsTab()
        {
            psGoods = ibl.getGoods();
            psGoodstype = ibl.getGoodsType();
            psDebitcredit = ibl.getDebitCredit();
            psUsers = ibl.getUsers();
            comboBoxGoodsType.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxGoodsVolunteer.DropDownStyle = ComboBoxStyle.DropDownList;
            for (int i = 1; i < psUsers.users.Rows.Count; i++)
            {
                comboBoxGoodsVolunteer.Items.Add(psUsers.users.Rows[i][3].ToString());
            }
            for (int i = 0; i < psGoodstype.goodstype.Rows.Count; i++)
            {
                if(i!=2)
                    comboBoxGoodsType.Items.Add(psGoodstype.goodstype.Rows[i][1].ToString());
            }
            comboBoxGoodsVolunteer.Items.Add("");
            dataGridViewGoodsAllGoods.DataSource = psGoods.goods;
            setGoodsGridView();
            dataGridViewGoodsAllGoods.ClearSelection();
        }
        #endregion
        #region Обновление окна
        private void refreshGoodsTab()
        {
            psGoods = ibl.getGoods();
            psGoodstype = ibl.getGoodsType();
            psDebitcredit = ibl.getDebitCredit();
            psUsers = ibl.getUsers();
            dataGridViewGoodsAllGoods.DataSource = psGoods.goods.DefaultView;
            comboBoxGoodsType.Items.Clear();
            comboBoxGoodsVolunteer.Items.Clear();
            comboBoxGoodsType.Visible = true;
            for (int i = 1; i < psUsers.users.Rows.Count; i++)
            {
                comboBoxGoodsVolunteer.Items.Add(psUsers.users.Rows[i][3].ToString());
            }
            for (int i = 0; i < psGoodstype.goodstype.Rows.Count; i++)
            {
                if (i != 2)
                    comboBoxGoodsType.Items.Add(psGoodstype.goodstype.Rows[i][1].ToString());
            }
            comboBoxGoodsVolunteer.Items.Add("");
            dataGridViewGoodsAllGoods.DataSource = psGoods.goods;
            setGoodsGridView();
            dataGridViewGoodsAllGoods.ClearSelection();
            if (dataGridViewGoodsAllGoods.RowCount<=1) dataGridViewGoodsAllGoods.Enabled = false;
            else dataGridViewGoodsAllGoods.Enabled = true;
            clearGoodsFilter();
            cleanGoodsAddArea();
        }
        #endregion
        #region Параметры датагрида "Материально-техническая база
        private void setGoodsGridView()
        {
            dataGridViewGoodsAllGoods.Columns[0].Visible = false;
            dataGridViewGoodsAllGoods.Columns[2].Visible = false;
            dataGridViewGoodsAllGoods.Columns[1].HeaderText = "Наименование";
            dataGridViewGoodsAllGoods.Columns[3].HeaderText = "В наличии";
            dataGridViewGoodsAllGoods.Columns[4].HeaderText = "Необходимо";
            dataGridViewGoodsAllGoods.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewGoodsAllGoods.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewGoodsAllGoods.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridViewGoodsAllGoods.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
        }
        #endregion
        #region Выбор элемента в датагриде "Материально-техническая база"
        private void dataGridViewGoodsAllGoods_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            setGoodsAddArea(e.RowIndex);
        }
        private void dataGridViewGoodsAllGoods_KeyUp(object sender, KeyEventArgs e)
        {
            setGoodsAddArea(dataGridViewGoodsAllGoods.CurrentRow.Index);
        }
        private void dataGridViewGoodsAllGoods_KeyDown(object sender, KeyEventArgs e)
        {
            setGoodsAddArea(dataGridViewGoodsAllGoods.CurrentRow.Index);
        }
        #endregion
        #region Установка параметров предмета для редактирования
        private void setGoodsAddArea(int i)
        {
            //cleanGoodsAddArea();
            if (i >= 0)
            {
                id = Convert.ToInt32(dataGridViewGoodsAllGoods.CurrentRow.Cells[0].Value);
                textBoxGoodsName.Text = dataGridViewGoodsAllGoods.CurrentRow.Cells[1].Value.ToString();
                textBoxGoodsAmount.Text = dataGridViewGoodsAllGoods.CurrentRow.Cells[3].Value.ToString();
                textBoxGoodsNeeded.Text = dataGridViewGoodsAllGoods.CurrentRow.Cells[4].Value.ToString();
                string stype = psGoodstype.goodstype.Rows[Convert.ToInt32(psGoods.goods.FindById_Goods(id)[2]) - 1][1].ToString();
                if (stype != "Деньги")
                {
                    comboBoxGoodsType.SelectedItem = stype;
                    comboBoxGoodsType.Visible = true;
                }
                else
                {
                    comboBoxGoodsType.Visible = false;
                }
                
                
            }
        }
        #endregion
        #region Очистка фильтра
        private void clearGoodsFilter()
        {
            textBoxGoodsSortNameofGoods.Text = "";
            radioButtonGoodsSortIsAll.Checked = false;
            radioButtonGoodsSortIsCure.Checked = false;
            radioButtonGoodsSortIsEat.Checked = false;
            radioButtonGoodsSortIsOther.Checked = false;
        }
        #endregion
        #region Очистка области редактирования информации о предмете
        private void cleanGoodsAddArea()
        {
            textBoxDebitCreditComment.Text = "";
            comboBoxGoodsType.SelectedIndex = -1;
            comboBoxGoodsVolunteer.Text = "";
            textBoxGoodsName.Text = "";
            textBoxGoodsNeeded.Text = "";
            textBoxGoodsAmount.Text = "";
        }
        #endregion
        #region Фильтрация таблицы "Предметы" согласно указаным критериям
        private void sortGoodsTable(object sender, EventArgs e)
        {
            String filter = null;
            if (radioButtonGoodsSortIsEat.Checked) filter = "CONVERT(Type, 'System.String')  LIKE '1'";
            if (radioButtonGoodsSortIsCure.Checked) filter = "CONVERT(Type, 'System.String')  LIKE '2'";
            if (radioButtonGoodsSortIsOther.Checked) filter = "CONVERT(Type, 'System.String')  LIKE '4'";
            if (textBoxGoodsSortNameofGoods.Text != "")
            {
                if (filter != null) filter += " and ";
                filter += "NameOfGoods LIKE '*" + textBoxGoodsSortNameofGoods.Text + "*'";
            }
            if (filter != null)
            {
                psGoods.goods.DefaultView.RowFilter = string.Format(filter);
                dataGridViewGoodsAllGoods.DataSource = psGoods.goods.DefaultView;
            }
            else
            {
                dataGridViewGoodsAllGoods.DataSource = psGoods;
                dataGridViewGoodsAllGoods.DataMember = "goods";
                setGoodsGridView();
            }
            setGoodsGridView();
            dataGridViewGoodsAllGoods.ClearSelection();
            cleanGoodsAddArea();         
        }
        #endregion
        #region Проверка введенных данных при добавлении предмета
        private bool checkGoodsAddArea()
        {
            //проверка есть ли уже такой предмет.
            psGoods = ibl.getGoods();
            System.Data.DataRow[] foundRows;
            foundRows = psGoods.goods.Select("NameOfGoods LIKE '" + textBoxGoodsName.Text.ToString()+"'");
            if (foundRows.Length > 0)
            {
                MessageBox.Show("Такой предмет уже существует.\nПопробуйте сменить имя или изменить существующий.");
                return false;
            }
            if (textBoxGoodsName.Text == "")
            {
                MessageBox.Show("Введите название элемента");
                return false;
            }
            else if (comboBoxGoodsType.Text == "")
            {
                if (comboBoxGoodsType.Visible)MessageBox.Show("Введите тип элемента");
                else MessageBox.Show("Вы пытаетесь добавить еще одни деньги в таблицу. К сожалению сделать этого нельзя.\nВыберите пожалуйста другой предмет в таблице и делайте, что душе угодно.");
                return false;
            }
            else if (textBoxGoodsAmount.Text == "")
            {
                MessageBox.Show("Введите количество элемента");
                return false;
            }
            else if (textBoxGoodsNeeded.Text == "")
            {
                MessageBox.Show("Введите необходимое кол-во элемента");
                return false;
            }           
            return true;
        }
        #endregion
        #region Кнопка создания нового предмета
        private void buttonGoodsAdd_Click(object sender, EventArgs e)
        {
            bool f = checkGoodsAddArea();
            if (f)
            {
                string n = textBoxGoodsName.Text;
                int t, ga, gn;

                try
                {
                     t = Convert.ToInt32(psGoodstype.goodstype.Rows[comboBoxGoodsType.SelectedIndex][0]);
                     if (t == 3) t = 4;
                     ga = Convert.ToInt32(textBoxGoodsAmount.Text);
                     gn = Convert.ToInt32(textBoxGoodsNeeded.Text);
                    
                }
                catch (System.FormatException)
                {
                    MessageBox.Show("Кажется вы ввели неправильные данные.\nПожалуйста прoверьте их.");
                    return;
                }
                DateTime gad = dateTimePickerGoods.Value;
                string com;

                psGoods.goods.AddgoodsRow(n, t, ga, gn);
                ibl.setGoods(psGoods);
                psGoods = ibl.getGoods();
                if (textBoxDebitCreditComment.Text != "")
                    com = textBoxDebitCreditComment.Text;
                else
                    com = null;
                if (comboBoxGoodsVolunteer.Text != "")
                {
                    int gi = Convert.ToInt32(psGoods.goods.Rows[psGoods.goods.Count - 1][0]);
                    int ui = Convert.ToInt32(psUsers.users.Rows[comboBoxGoodsVolunteer.SelectedIndex][0]);
                    psDebitcredit.debitcredit.AdddebitcreditRow(gi, com, gad, 0, ga, -1, ui, t);
                }
                else
                {
                    int gi = Convert.ToInt32(psGoods.goods.Rows[psGoods.goods.Count - 1][0]);
                    psDebitcredit.debitcredit.AdddebitcreditRow(gi, com, gad, 0, ga, -1, -1, t);
                }

                try { ibl.setDebitCredit(psDebitcredit); }
                catch (Exception) 
                { };
                refreshGoodsTab();
            }
        }
        #endregion
        #region Кнопка изменения информации о предмете
        private void buttonGoodsChange_Click(object sender, EventArgs e)
        {
            if (dataGridViewGoodsAllGoods.SelectedRows.Count > 0)
            {
                int amountBefore, amountAfter;
                try{
                    amountBefore = Convert.ToInt32(psGoods.goods.FindById_Goods(id)[3].ToString()); 
                    amountAfter = Convert.ToInt32(textBoxGoodsAmount.Text);               
                }
                catch (System.FormatException)
                {
                    MessageBox.Show("Кажется вы ввели неправильные данные.\nПожалуйста првоверьте их.");
                    return;
                }
                psGoods.goods.FindById_Goods(id)[1] = textBoxGoodsName.Text;
                int goodsType;
                if (comboBoxGoodsType.SelectedIndex != -1)
                {
                    goodsType = Convert.ToInt32(psGoodstype.goodstype.Rows[comboBoxGoodsType.SelectedIndex][0]);
                    if (goodsType == 3)
                        goodsType = 4;
                }
                else goodsType = 3;
                if (!comboBoxGoodsType.Visible) goodsType = 3;

                psGoods.goods.FindById_Goods(id)[2] = goodsType;
                psGoods.goods.FindById_Goods(id)[3] = Convert.ToInt32(textBoxGoodsAmount.Text);
                psGoods.goods.FindById_Goods(id)[4] = Convert.ToInt32(textBoxGoodsNeeded.Text);
                ibl.setGoods(psGoods);
                //Сделать запись в debitcredit
                if(amountAfter!=amountBefore)
                {
                    int d = amountAfter > amountBefore ? 0 : amountBefore-amountAfter;
                    int c = amountAfter > amountBefore ? amountAfter-amountBefore : 0;
                    string n = textBoxGoodsName.Text;
                    DateTime gad = dateTimePickerGoods.Value;
                    string com;
                    if (textBoxDebitCreditComment.Text != "")
                        com = textBoxDebitCreditComment.Text;
                    else
                        com = null;
                    int gi = Convert.ToInt32(psGoods.goods.FindById_Goods(id)[0]);
                    if (goodsType == 3) gi = 1;
                    if (comboBoxGoodsVolunteer.Text != "")
                    {
                        int ui = Convert.ToInt32(psUsers.users.Rows[comboBoxGoodsVolunteer.SelectedIndex][0]);
                        psDebitcredit.debitcredit.AdddebitcreditRow(gi, com, gad, d, c, -1, ui, goodsType);
                    }
                    else
                    {
                        psDebitcredit.debitcredit.AdddebitcreditRow(gi, com, gad, d, c, -1, -1, goodsType);
                    }
                    ibl.setDebitCredit(psDebitcredit);
                }
                refreshGoodsTab();
            }
            else
            {
                MessageBox.Show("Сначала выберите предмет в таблице");
            }
        }

        #endregion
        #endregion
        #region ОКНО "Отчёты"
        #region Инициализация окна
        private void initReportsTab()
        {
            psDebitcredit = ibl.getDebitCredit();
            psGoods = ibl.getGoods();
            psGoods.goods.DefaultView.RowFilter = string.Format("CONVERT(Type, 'System.String') LIKE '3'");
            labelReportsMoneyAmount.Text = psGoods.goods.DefaultView[0][3].ToString();
            dataGridViewReportsMain.DataSource = psDebitcredit;
            dataGridViewReportsMain.DataMember = "debitcredit";
            dataGridViewReportsMain.Columns.Add("goodsNameReports", "Наименование");
            cleanReportsFilterArea();
            setReportsGridView();
        }
        #endregion
        #region Правильное отображение названий предметов
        private void FillGoodsReports()
        {
            PetShelter _psGoods = ibl.getGoods();
            String goodsId;
            int rowCount = dataGridViewReportsMain.RowCount;
            string expression;
            if (rowCount >= 1)
            {

                for (int i = 0; i < rowCount; i++)
                {

                    if (dataGridViewReportsMain.Rows[i].Cells["GoodsName"].Value.ToString() != "")
                    {
                        goodsId = dataGridViewReportsMain.Rows[i].Cells["GoodsName"].Value.ToString();
                        expression = "Id_Goods = " + goodsId;
                        System.Data.DataRow[] foundRows;
                        foundRows = _psGoods.goods.Select(expression);
                        expression = foundRows[0][1].ToString();
                        dataGridViewReportsMain.Rows[i].Cells["goodsNameReports"].Value = expression;
                    }
                    else
                        dataGridViewReportsMain.Rows[i].Cells["goodsNameReports"].Value = "";
                }
            }
        }
        private void ReportsGridSorted(object sender, EventArgs e)
        {
            FillGoodsReports();
        }
        #endregion
        #region Обновление окна
        private void refreshReportsTab()
        {
            psDebitcredit = ibl.getDebitCredit();
            psGoods = ibl.getGoods();
            dataGridViewReportsMain.DataSource = psDebitcredit;
            psGoods.goods.DefaultView.RowFilter = string.Format("CONVERT(Type, 'System.String') LIKE '3'");
            labelReportsMoneyAmount.Text = psGoods.goods.DefaultView[0][3].ToString();
            id = Convert.ToInt32(psGoods.goods.DefaultView[0][0].ToString());
            setReportsGridView();
            dataGridViewReportsMain.Refresh();
            dataGridViewReportsMain.ClearSelection();
            cleanReportsMoneyControl();
            cleanReportsFilterArea();
        }
        #endregion
        #region Параметры датагрида "Отчёты"
        private void setReportsGridView()
        {
            FillGoodsReports();            
            dataGridViewReportsMain.Columns["Id_DebitCredit"].Visible = false;
            dataGridViewReportsMain.Columns["GoodsName"].Visible = false;
            dataGridViewReportsMain.Columns["PatientId"].Visible = false;
            dataGridViewReportsMain.Columns["UserId"].Visible = false;
            dataGridViewReportsMain.Columns["goodsNameReports"].HeaderText = "Наименование";
            dataGridViewReportsMain.Columns["Comment"].HeaderText = "Комментарий";
            dataGridViewReportsMain.Columns["Date"].HeaderText = "Дата";
            dataGridViewReportsMain.Columns["Debit"].HeaderText = "Списано";
            dataGridViewReportsMain.Columns["Credit"].HeaderText = "Поступило";
            dataGridViewReportsMain.Columns["goodsNameReports"].DisplayIndex = 0;
            dataGridViewReportsMain.Columns["Comment"].DisplayIndex = 1;
            dataGridViewReportsMain.Columns["Debit"].DisplayIndex = 2;
            dataGridViewReportsMain.Columns["Credit"].DisplayIndex = 3;
            dataGridViewReportsMain.Columns["Date"].DisplayIndex = 4;
            dataGridViewReportsMain.Columns["GoodsType"].Visible = false;

            dataGridViewReportsMain.Columns["Id_DebitCredit"].DisplayIndex = 5;
            dataGridViewReportsMain.Columns["GoodsName"].DisplayIndex = 6;
            dataGridViewReportsMain.Columns["PatientId"].DisplayIndex = 7;
            dataGridViewReportsMain.Columns["UserId"].DisplayIndex = 8;

            dataGridViewReportsMain.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewReportsMain.Columns["Debit"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewReportsMain.Columns["Credit"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewReportsMain.Columns["Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            
        }
        #endregion
        #region Очистка фильтра
        private void cleanReportsFilterArea()
        {
            checkBoxReportsEat.Checked = false;
            checkBoxReportsCure.Checked = false;
            checkBoxReportsOther.Checked = false;
            checkBoxReportsMoney.Checked = false;
            checkBoxReportsOutCome.Checked = false;
            checkBoxReportsInCome.Checked = false;
            sortByDate.Checked = false;
            dateTimePickerReportsFrom.Value = DateTime.Today.AddDays(-30);
            dateTimePickerReportsTo.Value = DateTime.Today;
        }
        #endregion
        #region Очистка области списания денег
        private void cleanReportsMoneyControl()
        {
            psGoods = ibl.getGoods();
            textBoxReportsMoneyToSpend.Text = "";
            textBoxReportsComment.Text = "";
            psGoods.goods.DefaultView.RowFilter = string.Format("CONVERT(Type, 'System.String') LIKE '3'");
            labelReportsMoneyAmount.Text = psGoods.goods.DefaultView[0][3].ToString();
        }
        #endregion
        #region Кнопка формирования отчёта
        private void buttonReportsForm_Click(object sender, EventArgs e)
        {
            Reports report = new Reports();
            report.CreateReportFromVisibleItems(dataGridViewReportsMain, "Отчёт по расходам");
            cleanReportsFilterArea();
        }

        #endregion
        #region Кнопка списания денежных средств
        private void buttonReportsWriteOffMoney_Click(object sender, EventArgs e)
        {
            if (textBoxReportsMoneyToSpend.Text == "")
            {
                MessageBox.Show("Введите сумму", "Ошибка!", MessageBoxButtons.OK);
                return;
            }
            else if (textBoxReportsComment.Text == "")
            {
                MessageBox.Show("Введите комментарий", "Ошибка!", MessageBoxButtons.OK);
                return;
            }
            if (Convert.ToInt32(textBoxReportsMoneyToSpend.Text)>Convert.ToInt32(labelReportsMoneyAmount.Text))
            {
                MessageBox.Show("Недостаточно средств", "Критическая ошибка!", MessageBoxButtons.OK);
                cleanReportsMoneyControl();
                return;
            }
            int gi = Convert.ToInt32(psGoods.goods.Rows[psGoods.goods.Count - 1][0]);
            psGoods.goods.FindById_Goods(id)[3] = Convert.ToInt32(psGoods.goods.FindById_Goods(id)[3]) - Convert.ToInt32(textBoxReportsMoneyToSpend.Text);
            ibl.setGoods(psGoods);
            psDebitcredit.debitcredit.AdddebitcreditRow(gi, textBoxReportsComment.Text, DateTime.Today.Date, Convert.ToInt32(textBoxReportsMoneyToSpend.Text), 0, -1, userId, 3);
            ibl.setDebitCredit(psDebitcredit);
            cleanReportsMoneyControl();
        }
        #endregion
        #region Фильтрация таблицы Приход/Расход согласно указанным критериям
        private void SortDebitCredit(object sender, EventArgs e)
        {

            psDebitcredit = ibl.getDebitCredit();
            String filter = null;
            if (checkBoxReportsEat.Checked) filter = "CONVERT(GoodsType, 'System.String') LIKE '1'";
            if (checkBoxReportsCure.Checked)
            {
                if (filter != null) filter += " or ";
                filter += "CONVERT(GoodsType, 'System.String') LIKE '2'";
            }
            if (checkBoxReportsOther.Checked)
            {
                if (filter != null) filter += " or ";
                filter += "CONVERT(GoodsType, 'System.String') LIKE '4'";
            }
            if (checkBoxReportsMoney.Checked)
            {
                if (filter != null) filter += " or ";
                filter += "CONVERT(GoodsType, 'System.String') LIKE '3'";
            }
            /*if (checkBoxReportsHelp.Checked)
            {
                if (filter != null) filter += " or ";
                filter += "CONVERT(GoodsType, 'System.String') = 'NULL'";
            }*/
            if (filter != null) filter = "(" + filter+")";

            if (checkBoxReportsOutCome.Checked)
            {
                if (filter != null) filter += " and ";
                //filter += "CONVERT(Credit, 'System.String') LIKE '0'";
                filter += "CONVERT(Debit, 'System.String') > '0'";
            }
            if (checkBoxReportsInCome.Checked)
            {
                if (filter != null) filter += " and ";
                //filter += "CONVERT(Debit, 'System.String') LIKE '0'";
                filter += "CONVERT(Credit, 'System.String') > '0'";
            }
            if (sortByDate.Checked)
            {
                string fromDate = dateTimePickerReportsFrom.Value.ToString("#yyyy/MM/dd#");
                string toDate = dateTimePickerReportsTo.Value.ToString("#yyyy/MM/dd#");
                int f = Convert.ToInt32( dateTimePickerReportsFrom.Value.ToString("yyyyMMdd"));
                int t = Convert.ToInt32(dateTimePickerReportsTo.Value.ToString("yyyyMMdd"));
                if (t<f)
                {
                    MessageBox.Show("Диапазон дат введен не верно", "", MessageBoxButtons.OK);
                    sortByDate.Checked = false;
                    return;
                }
                
                if (filter != null) filter += " and ";
                filter += "CONVERT(Date, 'System.String') >= " + fromDate + "";
                filter += " and ";
                filter += "CONVERT(Date, 'System.String') <= " + toDate + "";
            }  

            if (filter != null)
            {
                psDebitcredit.debitcredit.DefaultView.RowFilter = string.Format(filter);
                dataGridViewReportsMain.DataSource = psDebitcredit.debitcredit.DefaultView;
            }
            else{
            dataGridViewReportsMain.DataSource = psDebitcredit;
            dataGridViewReportsMain.DataMember = "debitcredit";
            }
            setReportsGridView();
            dataGridViewReportsMain.ClearSelection();
            //cleanReportsMoneyControl();
            //cleanReportsFilterArea();
        }
        #endregion
        #region Запрет не цифр в сумме
        private void textBoxesRep_keypress(object sender, KeyPressEventArgs e)
        {
            char symbol = e.KeyChar;
            if (!Char.IsDigit(symbol) && symbol != 8)
                e.Handled = true;
        }
        #endregion
        #endregion
    }
}