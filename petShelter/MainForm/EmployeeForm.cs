using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainForm
{
    public partial class EmployeeForm : Form
    {
        PetShelter psAnimals = new PetShelter();
        PetShelter psSpecies = new PetShelter();
        BLogic.IBL ibl = new BLogic.BLogic();
        int id;

        public EmployeeForm()
        {
            InitializeComponent();
            int tabWidth = tabMain.Width / tabMain.TabPages.Count - 1;
            tabMain.ItemSize = new Size(tabWidth, tabMain.ItemSize.Height);
            psAnimals = ibl.getAnimals();
            psSpecies = ibl.getSpecies();
        }

        private void EmployeeForm_Resize(object sender, EventArgs e)
        {
            int tabWidth = tabMain.Width / tabMain.TabPages.Count - 1;
            tabMain.ItemSize = new Size(tabWidth-1, tabMain.ItemSize.Height);
        }

        private void EmployeeForm_Load(object sender, EventArgs e)
        {
            comboBoxPetsSpecies.DropDownStyle = ComboBoxStyle.DropDownList;
            for (int i = 0; i < psSpecies.species.Rows.Count; i++)
            {
                comboBoxPetsSpecies.Items.Add(psSpecies.species.Rows[i][1].ToString());
            }

            dataGridViewPetsAllPets.DataSource = psAnimals;
            dataGridViewPetsAllPets.DataMember = "animals";
            dataGridViewPetsAllPets.Columns[0].Visible = false;
            dataGridViewPetsAllPets.Columns[6].Visible = false;
            dataGridViewPetsAllPets.Columns[7].Visible = false;
            dataGridViewPetsAllPets.Columns[8].Visible = false;
            dataGridViewPetsAllPets.Columns[9].Visible = false;
            dataGridViewPetsAllPets.Columns[1].HeaderText = "Вид";
            dataGridViewPetsAllPets.Columns[2].HeaderText = "Порода";
            dataGridViewPetsAllPets.Columns[3].HeaderText = "Кличка";
            dataGridViewPetsAllPets.Columns[4].HeaderText = "Дата поступления";
            dataGridViewPetsAllPets.Columns[5].HeaderText = "Дата выдачи";
            dataGridViewPetsAllPets.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        }

        private void dataGridViewPetsAllPets_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            setPetsAddArea(e.RowIndex);
        }

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
                    if (checkBoxPetsMaster.Checked)
                    {
                        string fio = textBoxPetsFIO.Text;
                        string pn = textBoxPetsPhoneNumber.Text;
                        string a = textBoxPetsAddress.Text;
                        DateTime dd = dateTimePickerPetsDeliveryDay.Value;
                        psAnimals.animals.AddanimalsRow(i, b, nn, ad, 0, fio, pn, a, dd);
                        ibl.setAnimals(psAnimals);
                    }
                    else
                    {
                        psAnimals.animals.AddanimalsRow(i, b, nn, ad, 1, null, null, null, default(DateTime));
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
                    changePetInfo(dataGridViewPetsAllPets.CurrentRow.Index);
                    cleanPetsAddArea();
                }
            }
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
        }
        #endregion
        #region Установка параметров животного для редактирования
        private void setPetsAddArea(int i)
        {
            cleanPetsAddArea();
            id = Convert.ToInt32(psAnimals.animals.Rows[i][0])-1;
            comboBoxPetsSpecies.SelectedItem = psSpecies.species.Rows[Convert.ToInt32(psAnimals.animals.Rows[i][1])-1][1].ToString();
            textBoxPetsBreed.Text = psAnimals.animals.Rows[i][2].ToString();
            textBoxPetsNickName.Text = psAnimals.animals.Rows[i][3].ToString();
            dateTimePickerPetsArrivalDate.Text = psAnimals.animals.Rows[i][4].ToString();
            if(psAnimals.animals.Rows[i][5].ToString()=="0")
            {
                checkBoxPetsMaster.Checked = true;
                textBoxPetsFIO.Text = psAnimals.animals.Rows[i][6].ToString();
                textBoxPetsPhoneNumber.Text = psAnimals.animals.Rows[i][7].ToString();
                textBoxPetsAddress.Text = psAnimals.animals.Rows[i][8].ToString();
                dateTimePickerPetsDeliveryDay.Text = psAnimals.animals.Rows[i][9].ToString();
            }
        }
        #endregion
        #region Обновление данных в датасетах
        private void refreshPetsInfo()
        {
            psAnimals = ibl.getAnimals();
            dataGridViewPetsAllPets.DataSource = psAnimals;
        }
        #endregion
        #region Изменение информации о животном в датасете
        private void changePetInfo(int i)
        {
            psAnimals.animals.Rows[i][1] = Convert.ToInt32(psSpecies.species.Rows[comboBoxPetsSpecies.SelectedIndex][0]);
            psAnimals.animals.Rows[i][2] = textBoxPetsBreed.Text;
            psAnimals.animals.Rows[i][3] = textBoxPetsNickName.Text;
            psAnimals.animals.Rows[i][4] = dateTimePickerPetsArrivalDate.Value;
            if (checkBoxPetsMaster.Checked)
            {
                psAnimals.animals.Rows[i][5] = 0;
                psAnimals.animals.Rows[i][6] = textBoxPetsFIO.Text;
                psAnimals.animals.Rows[i][7] = textBoxPetsPhoneNumber.Text;
                psAnimals.animals.Rows[i][8] = textBoxPetsAddress.Text;
                psAnimals.animals.Rows[i][9] = dateTimePickerPetsDeliveryDay.Value;
            }
            else
            {
                psAnimals.animals.Rows[i][5] = 1;
                psAnimals.animals.Rows[i][6] = null;
                psAnimals.animals.Rows[i][7] = null;
                psAnimals.animals.Rows[i][8] = null;
                psAnimals.animals.Rows[i][9] = default(DateTime);
            }
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
            psAnimals.animals.Rows[id].Delete();
            ibl.setAnimals(psAnimals);
            cleanPetsAddArea();
            refreshPetsInfo();
        }
        #endregion

        private void dataGridViewPetsAllPets_Leave(object sender, EventArgs e)
        {
            cleanPetsAddArea();
        }
    }
}
