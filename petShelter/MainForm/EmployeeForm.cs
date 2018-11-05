using System;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

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
        }
        #endregion
        #region Переключение между вкладками
        private void tabMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabMain.SelectedIndex == 0)
            {
                initAnimalsTab();
                cleanPetsAddArea();
                clearPetsAddCureArea();
                cleanPetsSortArea();
                return;
            }
            else if (tabMain.SelectedIndex == 1)
            {
                initStaffTab();
                clearStaffFilter();
                cleanStaffAddArea();
                return;
            }
            else if (tabMain.SelectedIndex == 2)
            {
                initGoodsTab();
                clearGoodsFilter();
                cleanGoodsAddArea();
                dataGridViewGoodsAllGoods.ClearSelection();
                return;
            }
            else if (tabMain.SelectedIndex == 3)
            {

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
            comboBoxPetsSpecies.Items.Clear();
            comboBoxSortSpecies.Items.Clear();
            for (int i = 0; i < psSpecies.species.Rows.Count; i++)
            {
                comboBoxSortSpecies.Items.Add(psSpecies.species.Rows[i][1].ToString());
                comboBoxPetsSpecies.Items.Add(psSpecies.species.Rows[i][1].ToString());
            }
            comboBoxSortSpecies.Items.Add("");
            comboBoxPetsCure.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxPetsCure.Items.Clear();
            for (int i = 0; i < psGoods.goods.Rows.Count; i++)
            {
                if (psGoods.goods.Rows[i][2].ToString()=="2")
                    comboBoxPetsCure.Items.Add(psGoods.goods.Rows[i][1].ToString());
            }
            dataGridViewPetsAllPets.DataSource = psAnimals;
            dataGridViewPetsAllPets.DataMember = "animals";
            dataGridViewPetsHistory.DataSource = 0;
            setPetsGridView();
        }
        #endregion
        #region Параметры датагрида "Животные"
        private void setPetsGridView()
        {
            dataGridViewPetsAllPets.ClearSelection();
            dataGridViewPetsAllPets.Columns[0].Visible = false;
            dataGridViewPetsAllPets.Columns[6].Visible = false;
            dataGridViewPetsAllPets.Columns[7].Visible = false;
            dataGridViewPetsAllPets.Columns[8].Visible = false;
            dataGridViewPetsAllPets.Columns[10].Visible = false;
            dataGridViewPetsAllPets.Columns[5].Visible = false;
            dataGridViewPetsAllPets.Columns[1].Visible = false;
            //dataGridViewPetsAllPets.Columns[1].HeaderText = "Вид";
            dataGridViewPetsAllPets.Columns[2].HeaderText = "Порода";
            dataGridViewPetsAllPets.Columns[3].HeaderText = "Кличка";
            dataGridViewPetsAllPets.Columns[4].HeaderText = "Дата поступления";
            dataGridViewPetsAllPets.Columns[9].HeaderText = "Дата выдачи";
            dataGridViewPetsAllPets.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
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
                    DateTime ad = dateTimePickerPetsArrivalDate.Value;
                    string pp;
                    if (openFileDialogAddPetPhoto.FileName != "")
                        pp = openFileDialogAddPetPhoto.FileName.ToString();
                    else
                        pp = null;
                    if (checkBoxPetsMaster.Checked)
                    {
                        string fio = textBoxPetsFIO.Text;
                        string pn = textBoxPetsPhoneNumber.Text;
                        string a = textBoxPetsAddress.Text;
                        DateTime dd = dateTimePickerPetsDeliveryDay.Value;
                        psAnimals.animals.AddanimalsRow(i, b, nn, ad, 0, fio, pn, a, dd, pp);
                        ibl.setAnimals(psAnimals);
                    }
                    else
                    {
                        psAnimals.animals.AddanimalsRow(i, b, nn, ad, 1, null, null, null, default(DateTime), pp);
                        ibl.setAnimals(psAnimals);
                    }
                    dataGridViewPetsAllPets.Refresh();
                    cleanPetsAddArea();
                }
            }
            else //Редактирование информации о животном
            {
                bool f = checkPetsAddArea();
                if (f)
                {
                    changePetInfo();
                    cleanPetsAddArea();
                }
            }
        }
        #endregion
        #region Очистка области внесения препарата животному
        private void clearPetsAddCureArea()
        {
            comboBoxPetsCure.SelectedIndex=-1;
            textBoxPetsComment.Text = "";
            dataGridViewPetsHistory.DataSource = 0;
        }
        #endregion
        #region Очистка фильтра
        private void cleanPetsSortArea()
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
            psDebitcredit.debitcredit.AdddebitcreditRow(gn, textBoxPetsComment.Text, DateTime.Today, 1, 0, id, userId);
            ibl.setDebitCredit(psDebitcredit);
            clearPetsAddCureArea();
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
            textBoxPetsFIO.Text = "";
            textBoxPetsPhoneNumber.Text = "";
            textBoxPetsAddress.Text = "";
            checkBoxPetsNewAnimal.Checked = false;
            pictureBoxPetPhoto.Image = Properties.Resources.d1;
        }
        #endregion
        #region Установка параметров животного для редактирования
        private void setPetsAddArea(int i)
        {
            cleanPetsAddArea();
            if (i>=0)
            {
                id=Convert.ToInt32(dataGridViewPetsAllPets.CurrentRow.Cells[0].Value);
                comboBoxPetsSpecies.SelectedItem = psSpecies.species.Rows[Convert.ToInt32(psAnimals.animals.FindById_Animals(id)[1]) - 1][1].ToString();
                textBoxPetsBreed.Text = psAnimals.animals.FindById_Animals(id)[2].ToString();
                textBoxPetsNickName.Text = psAnimals.animals.FindById_Animals(id)[3].ToString();
                dateTimePickerPetsArrivalDate.Text = psAnimals.animals.FindById_Animals(id)[4].ToString();
                if (psAnimals.animals.FindById_Animals(id)[10].ToString() == "")
                    pictureBoxPetPhoto.Image = Properties.Resources.d1;
                else
                {
                    pictureBoxPetPhoto.ImageLocation = psAnimals.animals.FindById_Animals(id)[10].ToString();
                }
                if (psAnimals.animals.FindById_Animals(id)[5].ToString() == "0")
                {
                    checkBoxPetsMaster.Checked = true;
                    textBoxPetsFIO.Text = psAnimals.animals.FindById_Animals(id)[6].ToString();
                    textBoxPetsPhoneNumber.Text = psAnimals.animals.FindById_Animals(id)[7].ToString();
                    textBoxPetsAddress.Text = psAnimals.animals.FindById_Animals(id)[8].ToString();
                    dateTimePickerPetsDeliveryDay.Text = psAnimals.animals.FindById_Animals(id)[9].ToString();
                }
            }
        }
        #endregion
        #region Установка истории лечения для животного
        private void setPetsCureHistory(int id)
        {
            psDebitcredit.debitcredit.DefaultView.RowFilter = string.Format("CONVERT(PatientId, 'System.String') LIKE '" + id + "'");
            dataGridViewPetsHistory.DataSource = psDebitcredit.debitcredit.DefaultView;
            dataGridViewPetsHistory.Refresh();
            dataGridViewPetsHistory.Columns[0].Visible = false;
            dataGridViewPetsHistory.Columns[4].Visible = false;
            dataGridViewPetsHistory.Columns[5].Visible = false;
            dataGridViewPetsHistory.Columns[6].Visible = false;
            dataGridViewPetsHistory.Columns[7].Visible = false;
            dataGridViewPetsHistory.Columns[1].HeaderText = "Препарат";
            dataGridViewPetsHistory.Columns[2].HeaderText = "Комментарий";
            dataGridViewPetsHistory.Columns[3].HeaderText = "Дата";
            dataGridViewPetsHistory.Columns[1].DisplayIndex = 1;
            dataGridViewPetsHistory.Columns[3].DisplayIndex = 0;
            dataGridViewPetsHistory.Columns[2].DisplayIndex = 2;
            dataGridViewPetsHistory.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewPetsHistory.ClearSelection();
        }
        #endregion
        #region Изменение информации о животном в датасете
        private void changePetInfo()
        {
            psAnimals.animals.FindById_Animals(id)[1] = Convert.ToInt32(psSpecies.species.Rows[comboBoxPetsSpecies.SelectedIndex][0]);
            psAnimals.animals.FindById_Animals(id)[2] = textBoxPetsBreed.Text;
            psAnimals.animals.FindById_Animals(id)[3] = textBoxPetsNickName.Text;
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
            if (openFileDialogAddPetPhoto.FileName != "")
                psAnimals.animals.FindById_Animals(id)[10] = openFileDialogAddPetPhoto.FileName.ToString();
            else
                psAnimals.animals.FindById_Animals(id)[10] = null;
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
                dataGridViewPetsAllPets.Refresh();
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
            if (filter != null) psAnimals.animals.DefaultView.RowFilter = string.Format(filter);
            else
            {
                dataGridViewPetsAllPets.DataSource = psAnimals;
                dataGridViewPetsAllPets.DataMember = "animals";
                setPetsGridView();
                return;
            }
            dataGridViewPetsAllPets.DataSource = psAnimals.animals.DefaultView;
            setPetsGridView();
            dataGridViewPetsAllPets.ClearSelection();
            cleanPetsAddArea();
        }
        #endregion
        #region Добавление фото питомца
        private void pictureBoxPetPhoto_Click(object sender, EventArgs e)
        {
            if (openFileDialogAddPetPhoto.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = openFileDialogAddPetPhoto.FileName;
        }
        #endregion
        #endregion

        #region ОКНО "Персонал"
        #region Инициализация окна
        private void initStaffTab()
        {
            psUsers = ibl.getUsers();
            psPositions = ibl.getPositions();
            dataGridViewStaffAllMembers.DataSource = psUsers;
            dataGridViewStaffAllMembers.DataMember = "users";
            setStaffGridView();
            setStaffCharity();

            comboBoxStaffVacancy.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxStaffVacancy.Items.Clear();

            for (int i = 0; i < psPositions.positions.Rows.Count; i++)
            {
                comboBoxStaffVacancy.Items.Add(psPositions.positions.Rows[i][1].ToString());
            }
            comboBoxStaffVacancy.Items.Add("");
        }
        #endregion
        #region Параметры датагрида "Персонал"
        private void setStaffGridView()
        {
            dataGridViewStaffAllMembers.ClearSelection();
            dataGridViewStaffAllMembers.Columns[0].Visible = false;
            dataGridViewStaffAllMembers.Columns[2].Visible = false;
            //dataGridViewStaffAllMembers.Columns[4].Visible = false;
            dataGridViewStaffAllMembers.Columns[1].HeaderText = "Логин";
            dataGridViewStaffAllMembers.Columns[3].HeaderText = "ФИО";
            dataGridViewStaffAllMembers.Columns[5].HeaderText = "Телефон";
            dataGridViewStaffAllMembers.Columns[6].HeaderText = "Адрес";
            dataGridViewStaffAllMembers.Columns[4].HeaderText = "Должность";
            dataGridViewStaffAllMembers.Columns[1].DisplayIndex = 1;
            dataGridViewStaffAllMembers.Columns[3].DisplayIndex = 0;
            dataGridViewStaffAllMembers.Columns[5].DisplayIndex = 5;
            dataGridViewStaffAllMembers.Columns[6].DisplayIndex = 4;
            dataGridViewStaffAllMembers.Columns[4].DisplayIndex = 3;
            dataGridViewStaffAllMembers.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }
        #endregion
        #region Очистка фильтра
        private void clearStaffFilter()
        {
            textBoxStaffSortLastName.Text = "";
            textBoxStaffSortMiddleName.Text = "";
            textBoxStafSortfFirstName.Text = "";
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
            cleanStaffAddArea();
            if (i >= 0)
            {
                id = Convert.ToInt32(dataGridViewStaffAllMembers.CurrentRow.Cells[0].Value);
                comboBoxStaffVacancy.SelectedItem = psPositions.positions.Rows[Convert.ToInt32(psUsers.users.FindById_Users(id)[4]) - 1][1].ToString();
                textBoxStaffLogin.Text = psUsers.users.FindById_Users(id)[1].ToString();
                //textBoxStaffPass.Text =;
                textBoxStaffLastName.Text = psUsers.users.FindById_Users(id)[3].ToString().Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries)[0];
                textBoxStaffFirstName.Text = psUsers.users.FindById_Users(id)[3].ToString().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1];
                textBoxStaffMiddleName.Text = psUsers.users.FindById_Users(id)[3].ToString().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[2];
                textBoxStaffPhoneNumber.Text = psUsers.users.FindById_Users(id)[5].ToString();
                textBoxStaffAddress.Text = psUsers.users.FindById_Users(id)[6].ToString();
            }
        }
        #endregion
        #region Установка благих дел сотрудника
        private void setStaffCharity()
        {
            psDebitcredit = ibl.getDebitCredit();
            psDebitcredit.debitcredit.DefaultView.RowFilter = string.Format("CONVERT(UserId, 'System.String') LIKE '" + userId + "' and CONVERT(PatientId, 'System.String') IS NULL and CONVERT(Debit, 'System.String') LIKE '0' and CONVERT(Credit, 'System.String') LIKE '0'");
            dataGridViewStaffCharity.DataSource = psDebitcredit.debitcredit.DefaultView;
            dataGridViewStaffCharity.Refresh();
            dataGridViewStaffCharity.Columns[0].Visible = false;
            dataGridViewStaffCharity.Columns[1].Visible = false;
            dataGridViewStaffCharity.Columns[4].Visible = false;
            dataGridViewStaffCharity.Columns[5].Visible = false;
            dataGridViewStaffCharity.Columns[6].Visible = false;
            dataGridViewStaffCharity.Columns[7].Visible = false;
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
            if (textBoxStaffSortLastName.Text != "")
            {
                if (filter != null) filter += " and ";
                filter += "FirstMiddleLastName LIKE '*" + textBoxStaffSortLastName.Text + "*'";
            }
            if (textBoxStafSortfFirstName.Text != "")
            {
                if (filter != null) filter += " and ";
                filter += "FirstMiddleLastName LIKE '*" + textBoxStafSortfFirstName.Text + "*'";
            }
            if (textBoxStaffSortMiddleName.Text != "")
            {
                if (filter != null) filter += " and ";
                filter += "FirstMiddleLastName LIKE '*" + textBoxStaffSortMiddleName.Text + "*'";
            }
            if (filter != null) psUsers.users.DefaultView.RowFilter = string.Format(filter);
            else
            {
                dataGridViewStaffAllMembers.DataSource = psUsers;
                dataGridViewStaffAllMembers.DataMember = "users";
                setStaffGridView();
                return;
            }
            dataGridViewStaffAllMembers.DataSource = psUsers.users.DefaultView;
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
            return true;
        }
        #endregion
        #region Кнопка создания или изменения информации о сотруднике
        private void buttonStaffSave_Click(object sender, EventArgs e)
        {
            if (checkBoxStaffNewMember.Checked) //Добавить сотрудника в бд
            {
                bool f = checkStaffAddArea();
                if (f)
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
                        ibl.setUsers(psUsers);
                    }
                    else
                    {
                        psUsers.users.AddusersRow(l, null, fio, pos, phone, address);
                        ibl.setUsers(psUsers);
                    }
                    dataGridViewStaffAllMembers.Refresh();
                    cleanStaffAddArea();
                }
            }
            else //Редактирование информации о животном
            {
                bool f = checkStaffAddArea();
                if (f)
                {
                    changeStaffInfo();
                    dataGridViewStaffAllMembers.Refresh();
                    cleanStaffAddArea();
                }
            }
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
                dataGridViewStaffAllMembers.Refresh();
                cleanStaffAddArea();
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
            psDebitcredit.debitcredit.AdddebitcreditRow(-1, textBoxStaffComment.Text, DateTime.Today, 0, 0, -1, userId);
            ibl.setDebitCredit(psDebitcredit);
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
            comboBoxGoodsType.Items.Clear();
            comboBoxGoodsVolunteer.Items.Clear();
            for (int i = 0; i < psUsers.users.Rows.Count; i++)
            {
                comboBoxGoodsVolunteer.Items.Add(psUsers.users.Rows[i][3].ToString());
            }
            for (int i = 0; i < (psGoodstype.goodstype.Rows.Count - 1); i++)
            {
                comboBoxGoodsType.Items.Add(psGoodstype.goodstype.Rows[i][1].ToString());
            }
            comboBoxGoodsVolunteer.Items.Add("");
            //Убираем ДЕНЬГИ из выдачи
            psGoods.goods.DefaultView.RowFilter = string.Format("CONVERT(Type, 'System.String')  NOT LIKE '4'");
            dataGridViewGoodsAllGoods.DataSource = psGoods.goods.DefaultView;
            //dataGridViewGoodsAllGoods.DataMember = "goods";
            setGoodsGridView();

            /*dataGridViewGoodsAllGoods.DataSource = psGoods;
            dataGridViewGoodsAllGoods.DataMember = "goods";
            setGoodsGridView();*/
            dataGridViewGoodsAllGoods.ClearSelection();
        }
        #endregion
        #region Параметры датагрида "Материально-техническая база
        private void setGoodsGridView()
        {
            dataGridViewGoodsAllGoods.Columns[0].Visible = false;
            dataGridViewGoodsAllGoods.Columns[1].HeaderText = "Наименование";
            dataGridViewGoodsAllGoods.Columns[2].Visible = false;
            dataGridViewGoodsAllGoods.Columns[3].HeaderText = "В наличии";
            dataGridViewGoodsAllGoods.Columns[4].HeaderText = "Необходимо";
            //dataGridViewGoodsAllGoods.Columns[5].HeaderText = "Дата поступления";
            dataGridViewGoodsAllGoods.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewGoodsAllGoods.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
           // dataGridViewGoodsAllGoods.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
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
            cleanGoodsAddArea();
            if (i >= 0)
            {
                id = Convert.ToInt32(dataGridViewGoodsAllGoods.CurrentRow.Cells[0].Value);
                textBoxGoodsName.Text = dataGridViewGoodsAllGoods.CurrentRow.Cells[1].Value.ToString();
                textBoxGoodsAmount.Text = dataGridViewGoodsAllGoods.CurrentRow.Cells[3].Value.ToString();
                textBoxGoodsNeeded.Text = dataGridViewGoodsAllGoods.CurrentRow.Cells[4].Value.ToString();
                comboBoxGoodsType.SelectedItem = psGoodstype.goodstype.Rows[Convert.ToInt32(psGoods.goods.FindById_Goods(id)[2]) - 1][1].ToString();
                //Поиск последнего внесения изменения этого товара Не надо этого. Эту дату если что во вкладедке с приходом/расходом смореть будут
                /*var strExpr = "GoodsName = " + dataGridViewGoodsAllGoods.CurrentRow.Cells[1].Value.ToString();
                DataTable dt = psDebitcredit.debitcredit; // refer to your table of interest within the DataSet
                dt.Select(strExpr);
                dateTimePickerGoods.Text = dt.Rows[0]["date"].ToString(); //Получение даты для первого */
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
            if (radioButtonGoodsSortIsOther.Checked) filter = "CONVERT(Type, 'System.String')  LIKE '3'";
            if (filter == null) filter = "CONVERT(Type, 'System.String')  NOT LIKE '4'";
            if (textBoxGoodsSortNameofGoods.Text != "")
            {
                if (filter != null) filter += " and ";
                filter += "NameOfGoods LIKE '*" + textBoxGoodsSortNameofGoods.Text + "*'";
            }        
            if (filter!=null) psGoods.goods.DefaultView.RowFilter = string.Format(filter);
            else
            {
                dataGridViewGoodsAllGoods.DataSource = psGoods;
                dataGridViewGoodsAllGoods.DataMember = "goods";
                setGoodsGridView();
                return;
            }
            dataGridViewGoodsAllGoods.DataSource = psGoods.goods.DefaultView;
            setGoodsGridView();
            dataGridViewGoodsAllGoods.ClearSelection();
            cleanGoodsAddArea();         
        }
        #endregion
        #region Проверка введенных данных при добавлении предмета
        private bool checkGoodsAddArea()
        {
            if (textBoxGoodsName.Text == "")
            {
                MessageBox.Show("Введите название элемента");
                return false;
            }
            else if (comboBoxGoodsType.Text == "")
            {
                MessageBox.Show("Введите тип элемента");
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
                int t = Convert.ToInt32(psGoodstype.goodstype.Rows[comboBoxGoodsType.SelectedIndex][0]);
                int ga = Convert.ToInt32(textBoxGoodsAmount.Text);
                int gn = Convert.ToInt32(textBoxGoodsNeeded.Text);
                DateTime gad = dateTimePickerGoods.Value;
                string com;

                psGoods.goods.AddgoodsRow(n, t, ga, gn);
                ibl.setGoods(psGoods);
                if (textBoxDebitCreditComment.Text != "")
                    com = textBoxDebitCreditComment.Text;
                else
                    com = null;
                if (comboBoxGoodsVolunteer.Text != "")
                {
                    int gi = Convert.ToInt32(psGoods.goods.Rows[psGoods.goods.Count - 1][0]);
                    int ui = Convert.ToInt32(psUsers.users.Rows[comboBoxGoodsVolunteer.SelectedIndex][0]);
                    psDebitcredit.debitcredit.AdddebitcreditRow(gi, com, gad, 0, ga, -1, ui);
                }
                else
                {
                    int gi = Convert.ToInt32(psGoods.goods.Rows[psGoods.goods.Count - 1][0]);
                    psDebitcredit.debitcredit.AdddebitcreditRow(gi, com, gad, 0, ga, -1, -1);
                }

                ibl.setDebitCredit(psDebitcredit);
                dataGridViewGoodsAllGoods.Refresh();
                cleanGoodsAddArea();
            }
        }
        #endregion
        #region Кнопка изменения информации о предмете
        private void buttonGoodsChange_Click(object sender, EventArgs e)
        {
            if (dataGridViewGoodsAllGoods.SelectedRows.Count > 0)
            {
                int amountBefore = Convert.ToInt32(psGoods.goods.FindById_Goods(id)[3].ToString()); 
                int amountAfter = Convert.ToInt32(textBoxGoodsAmount.Text);
                psGoods.goods.FindById_Goods(id)[1] = textBoxGoodsName.Text;
                psGoods.goods.FindById_Goods(id)[2] = Convert.ToInt32(psGoodstype.goodstype.Rows[comboBoxGoodsType.SelectedIndex][0]);
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
                    if (comboBoxGoodsVolunteer.Text != "")
                    {
                        int gi = Convert.ToInt32(psGoods.goods.Rows[psGoods.goods.Count - 1][0]);
                        int ui = Convert.ToInt32(psUsers.users.Rows[comboBoxGoodsVolunteer.SelectedIndex][0]);
                        psDebitcredit.debitcredit.AdddebitcreditRow(gi, com, gad, d, c, -1, ui);
                    }
                    else
                    {
                        int gi = Convert.ToInt32(psGoods.goods.Rows[psGoods.goods.Count - 1][0]);
                        psDebitcredit.debitcredit.AdddebitcreditRow(gi, com, gad, d, c, -1, -1);
                    }
                    ibl.setDebitCredit(psDebitcredit);
                }
                dataGridViewGoodsAllGoods.Refresh();
                cleanGoodsAddArea();
            }
            else
            {
                MessageBox.Show("Сначала выберите предмет в таблице");
            }
        }

        #endregion

        #endregion

        #region ОКНО "Отчёты"

        #endregion
    }
}