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
            openFileDialogAddPetPhoto.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            psAnimals = ibl.getAnimals();
            psSpecies = ibl.getSpecies();
            
        }

        private void EmployeeForm_Resize(object sender, EventArgs e)
        {
            /*int tabWidth = tabMain.Width / tabMain.TabPages.Count - 1;
            tabMain.ItemSize = new Size(tabWidth-1, tabMain.ItemSize.Height);*/
        }

        private void EmployeeForm_Load(object sender, EventArgs e)
        {
            comboBoxPetsSpecies.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxSortSpecies.DropDownStyle = ComboBoxStyle.DropDownList;


            for (int i = 0; i < psSpecies.species.Rows.Count; i++)
            {
                comboBoxSortSpecies.Items.Add(psSpecies.species.Rows[i][1].ToString());
                comboBoxPetsSpecies.Items.Add(psSpecies.species.Rows[i][1].ToString());
            }
            comboBoxSortSpecies.Items.Add("");

            dataGridViewPetsAllPets.DataSource = psAnimals;
            dataGridViewPetsAllPets.DataMember = "animals";
            setPetsGridView();
            dataGridViewPetsAllPets.ClearSelection();

            initGoodsTab();

            



        }
        #region ОКНО Животные
        private void setPetsGridView()
        {
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
        #region Обновление данных в датасетах
        private void refreshPetsInfo()
        {
            psAnimals = ibl.getAnimals();
            dataGridViewPetsAllPets.DataSource = psAnimals;
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
                psAnimals.animals.Rows[id].Delete();
                ibl.setAnimals(psAnimals);
                cleanPetsAddArea();
                refreshPetsInfo();
            }    
                else
            {
                MessageBox.Show("Сначала выберите животное в таблице");
            }  
        }
        #endregion
        #region Сортировка таблицы Животных согласно указанным критериям
        private void sortPetsTable(object sender, EventArgs e)
        {
            /*if (comboBoxSortSpecies.Text != "" && textBoxPetsSortBreed.Text != "" && textBoxPetsSortNickName.Text != "" && checkBoxPetsIsAtShelter.Checked && checkBoxPetsIsAtHome.Checked)
            {
                psAnimals.animals.DefaultView.RowFilter = string.Format("CONVERT(Species, 'System.String') LIKE '{0}' and Breed LIKE '{1}' and NickName LIKE '{2}' or CONVERT(InHere, 'System.String') LIKE '1' and CONVERT(InHere, 'System.String') LIKE '0'", psSpecies.species.Rows[comboBoxSortSpecies.SelectedIndex][0].ToString(), textBoxPetsSortBreed.Text, textBoxPetsSortNickName.Text);
            }
            else if (comboBoxSortSpecies.Text != "" && textBoxPetsSortBreed.Text != "" && textBoxPetsSortNickName.Text != "" && checkBoxPetsIsAtShelter.Checked)
            {
                psAnimals.animals.DefaultView.RowFilter = string.Format("CONVERT(Species, 'System.String') LIKE '{0}' and Breed LIKE '{1}' and NickName LIKE '{2}' and CONVERT(InHere, 'System.String') LIKE '1'", psSpecies.species.Rows[comboBoxSortSpecies.SelectedIndex][0].ToString(), textBoxPetsSortBreed.Text, textBoxPetsSortNickName.Text);
            }
            else if (comboBoxSortSpecies.Text != "" && textBoxPetsSortBreed.Text != "" && textBoxPetsSortNickName.Text != "" && checkBoxPetsIsAtHome.Checked)
            {
                psAnimals.animals.DefaultView.RowFilter = string.Format("CONVERT(Species, 'System.String') LIKE '{0}' and Breed LIKE '{1}' and NickName LIKE '{2}' and CONVERT(InHere, 'System.String') LIKE '0'", psSpecies.species.Rows[comboBoxSortSpecies.SelectedIndex][0].ToString(), textBoxPetsSortBreed.Text, textBoxPetsSortNickName.Text);
            }
            else if (comboBoxSortSpecies.Text != "" && textBoxPetsSortBreed.Text != "" && checkBoxPetsIsAtHome.Checked && checkBoxPetsIsAtShelter.Checked)
            {
                psAnimals.animals.DefaultView.RowFilter = string.Format("CONVERT(Species, 'System.String') LIKE '{0}' and Breed LIKE '{1}' and (CONVERT(InHere, 'System.String') LIKE '0' or CONVERT(InHere, 'System.String') LIKE '1')", psSpecies.species.Rows[comboBoxSortSpecies.SelectedIndex][0].ToString(), textBoxPetsSortBreed.Text);
            }
            else if (comboBoxSortSpecies.Text != "" && checkBoxPetsIsAtHome.Checked && textBoxPetsSortNickName.Text != "" && checkBoxPetsIsAtShelter.Checked)
            {
                psAnimals.animals.DefaultView.RowFilter = string.Format("CONVERT(Species, 'System.String') LIKE '{0}' and NickName LIKE '{1}' and (CONVERT(InHere, 'System.String') LIKE '0' or CONVERT(InHere, 'System.String') LIKE '1')", psSpecies.species.Rows[comboBoxSortSpecies.SelectedIndex][0].ToString(), textBoxPetsSortNickName.Text);
            }
            else if (checkBoxPetsIsAtHome.Checked && textBoxPetsSortBreed.Text != "" && textBoxPetsSortNickName.Text != "" && checkBoxPetsIsAtShelter.Checked)
            {
                psAnimals.animals.DefaultView.RowFilter = string.Format("Breed LIKE '{0}' and NickName LIKE '{1}' and (CONVERT(InHere, 'System.String') LIKE '0' or CONVERT(InHere, 'System.String') LIKE '1')", textBoxPetsSortBreed.Text, textBoxPetsSortNickName.Text);
            }
            else if (comboBoxSortSpecies.Text != "" && textBoxPetsSortBreed.Text != "" && checkBoxPetsIsAtHome.Checked)
            {
                psAnimals.animals.DefaultView.RowFilter = string.Format("CONVERT(Species, 'System.String') LIKE '{0}' and Breed LIKE '{1}' and CONVERT(InHere, 'System.String') LIKE '0'", psSpecies.species.Rows[comboBoxSortSpecies.SelectedIndex][0].ToString(), textBoxPetsSortBreed.Text);
            }
            else if (comboBoxSortSpecies.Text != "" && checkBoxPetsIsAtHome.Checked && textBoxPetsSortNickName.Text != "")
            {
                psAnimals.animals.DefaultView.RowFilter = string.Format("CONVERT(Species, 'System.String') LIKE '{0}' and CONVERT(InHere, 'System.String') LIKE '0' and NickName LIKE '{1}'", psSpecies.species.Rows[comboBoxSortSpecies.SelectedIndex][0].ToString(), textBoxPetsSortNickName.Text);
            }
            else if (comboBoxSortSpecies.Text != "" && checkBoxPetsIsAtHome.Checked && checkBoxPetsIsAtShelter.Checked)
            {
                psAnimals.animals.DefaultView.RowFilter = string.Format("CONVERT(Species, 'System.String') LIKE '{0}' and ( CONVERT(InHere, 'System.String') LIKE '0' or CONVERT(InHere, 'System.String') LIKE '1')", psSpecies.species.Rows[comboBoxSortSpecies.SelectedIndex][0].ToString());
            }
            else if (checkBoxPetsIsAtHome.Checked && textBoxPetsSortBreed.Text != "" && textBoxPetsSortNickName.Text != "")
            {
                psAnimals.animals.DefaultView.RowFilter = string.Format("CONVERT(InHere, 'System.String') LIKE '0' and Breed LIKE '{0}' and NickName LIKE '{1}'", textBoxPetsSortBreed.Text, textBoxPetsSortNickName.Text);
            }
            else if (checkBoxPetsIsAtHome.Checked && textBoxPetsSortBreed.Text != "" && checkBoxPetsIsAtShelter.Checked)
            {
                psAnimals.animals.DefaultView.RowFilter = string.Format("(CONVERT(InHere, 'System.String') LIKE '0' or CONVERT(InHere, 'System.String') LIKE '1') and Breed LIKE '{0}'", textBoxPetsSortBreed.Text);
            }
            else if (checkBoxPetsIsAtHome.Checked && textBoxPetsSortNickName.Text != "" && checkBoxPetsIsAtShelter.Checked)
            {
                psAnimals.animals.DefaultView.RowFilter = string.Format("NickName LIKE '{0}' and (CONVERT(InHere, 'System.String') LIKE '0' or CONVERT(InHere, 'System.String') LIKE '1')", textBoxPetsSortNickName.Text);
            }
            else if (comboBoxSortSpecies.Text != "" && textBoxPetsSortBreed.Text != "" && textBoxPetsSortNickName.Text != "")
            {
                psAnimals.animals.DefaultView.RowFilter = string.Format("CONVERT(Species, 'System.String') LIKE '{0}' and Breed LIKE '{1}' and NickName LIKE '{2}'", psSpecies.species.Rows[comboBoxSortSpecies.SelectedIndex][0].ToString(), textBoxPetsSortBreed.Text, textBoxPetsSortNickName.Text);
            }
            else if (comboBoxSortSpecies.Text != "" && textBoxPetsSortBreed.Text != "" && checkBoxPetsIsAtShelter.Checked)
            {
                psAnimals.animals.DefaultView.RowFilter = string.Format("CONVERT(Species, 'System.String') LIKE '{0}' and Breed LIKE '{1}' and CONVERT(InHere, 'System.String') LIKE '1'", psSpecies.species.Rows[comboBoxSortSpecies.SelectedIndex][0].ToString(), textBoxPetsSortBreed.Text);
            }
            else if (comboBoxSortSpecies.Text != "" && checkBoxPetsIsAtShelter.Checked && textBoxPetsSortNickName.Text != "")
            {
                psAnimals.animals.DefaultView.RowFilter = string.Format("CONVERT(Species, 'System.String') LIKE '{0}' and CONVERT(InHere, 'System.String') LIKE '1' and NickName LIKE '{1}'", psSpecies.species.Rows[comboBoxSortSpecies.SelectedIndex][0].ToString(), textBoxPetsSortNickName.Text);
            }
            else if (checkBoxPetsIsAtShelter.Checked && textBoxPetsSortBreed.Text != "" && textBoxPetsSortNickName.Text != "")
            {
                psAnimals.animals.DefaultView.RowFilter = string.Format("CONVERT(InHere, 'System.String') LIKE '1' and Breed LIKE '{0}' and NickName LIKE '{1}'", textBoxPetsSortBreed.Text, textBoxPetsSortNickName.Text);
            }
            else if (comboBoxSortSpecies.Text != "" && textBoxPetsSortBreed.Text != "")
            {
                psAnimals.animals.DefaultView.RowFilter = string.Format("CONVERT(Species, 'System.String') LIKE '{0}' and Breed LIKE '{1}'", psSpecies.species.Rows[comboBoxSortSpecies.SelectedIndex][0].ToString(), textBoxPetsSortBreed.Text);
            }
            else if (comboBoxSortSpecies.Text != "" && textBoxPetsSortNickName.Text != "")
            {
                psAnimals.animals.DefaultView.RowFilter = string.Format("CONVERT(Species, 'System.String') LIKE '{0}' and NickName LIKE '{1}'", psSpecies.species.Rows[comboBoxSortSpecies.SelectedIndex][0].ToString(), textBoxPetsSortNickName.Text);
            }
            else if (textBoxPetsSortBreed.Text != "" && textBoxPetsSortNickName.Text != "")
            {
                psAnimals.animals.DefaultView.RowFilter = string.Format("Breed LIKE '{0}' and NickName LIKE '{1}'", textBoxPetsSortBreed.Text, textBoxPetsSortNickName.Text);
            }
            else if (comboBoxSortSpecies.Text != "" && checkBoxPetsIsAtShelter.Checked)
            {
                psAnimals.animals.DefaultView.RowFilter = string.Format("CONVERT(Species, 'System.String') LIKE '{0}' and CONVERT(InHere, 'System.String') LIKE '1'", psSpecies.species.Rows[comboBoxSortSpecies.SelectedIndex][0].ToString());
            }
            else if (checkBoxPetsIsAtShelter.Checked && textBoxPetsSortBreed.Text != "")
            {
                psAnimals.animals.DefaultView.RowFilter = string.Format("CONVERT(InHere, 'System.String') LIKE '1' and Breed LIKE '{0}'", textBoxPetsSortBreed.Text);
            }
            else if (checkBoxPetsIsAtShelter.Checked && textBoxPetsSortNickName.Text != "")
            {
                psAnimals.animals.DefaultView.RowFilter = string.Format("CONVERT(InHere, 'System.String') LIKE '1' and NickName LIKE '{0}'", textBoxPetsSortNickName.Text);
            }
            else if (comboBoxSortSpecies.Text != "" && checkBoxPetsIsAtHome.Checked)
            {
                psAnimals.animals.DefaultView.RowFilter = string.Format("CONVERT(Species, 'System.String') LIKE '{0}' and CONVERT(InHere, 'System.String') LIKE '0'", psSpecies.species.Rows[comboBoxSortSpecies.SelectedIndex][0].ToString());
            }
            else if (checkBoxPetsIsAtHome.Checked && textBoxPetsSortBreed.Text != "")
            {
                psAnimals.animals.DefaultView.RowFilter = string.Format("CONVERT(InHere, 'System.String') LIKE '0' and Breed LIKE '{0}'", textBoxPetsSortBreed.Text);
            }
            else if (checkBoxPetsIsAtHome.Checked && textBoxPetsSortNickName.Text != "")
            {
                psAnimals.animals.DefaultView.RowFilter = string.Format("CONVERT(InHere, 'System.String') LIKE '0' and NickName LIKE '{0}'", textBoxPetsSortNickName.Text);
            }
            else if (checkBoxPetsIsAtShelter.Checked && checkBoxPetsIsAtHome.Checked)
            {
                psAnimals.animals.DefaultView.RowFilter = string.Format("CONVERT(InHere, 'System.String') LIKE '1' or CONVERT(InHere, 'System.String') LIKE '0'");
            }
            else if (comboBoxSortSpecies.Text != "")
            {
                psAnimals.animals.DefaultView.RowFilter = string.Format("CONVERT(Species, 'System.String') LIKE '{0}'", psSpecies.species.Rows[comboBoxSortSpecies.SelectedIndex][0].ToString());
            }
            else if (textBoxPetsSortBreed.Text != "")
            {
                psAnimals.animals.DefaultView.RowFilter = string.Format("Breed LIKE '{0}'", textBoxPetsSortBreed.Text);
            }
            else if(textBoxPetsSortNickName.Text!="")
            {
                psAnimals.animals.DefaultView.RowFilter = string.Format("NickName LIKE '{0}'", textBoxPetsSortNickName.Text);
            }
            else if (checkBoxPetsIsAtShelter.Checked)
            {
                psAnimals.animals.DefaultView.RowFilter = string.Format("CONVERT(InHere, 'System.String') LIKE '1'");
            }
            else if (checkBoxPetsIsAtHome.Checked)
            {
                psAnimals.animals.DefaultView.RowFilter = string.Format("CONVERT(InHere, 'System.String') LIKE '0'");
            }*/

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
        #endregion

        #region ОКНО Материально-техническая база
        
        PetShelter psGoods = new PetShelter();
        PetShelter psGoodstype = new PetShelter();
        PetShelter psDebitcredit = new PetShelter();
        PetShelter psUsers = new PetShelter();

        private void dataGridViewGoodsAllGoods_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            setGoodsAddArea(e.RowIndex);

        }       
        private void initGoodsTab()
        {
            psGoods = ibl.getGoods();
            psGoodstype = ibl.getGoodsType();
            psDebitcredit = ibl.getDebitCredit();
            psUsers = ibl.getUsers();

            comboBoxGoodsType.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxGoodsVolunteer.DropDownStyle = ComboBoxStyle.DropDownList;
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
        private void cleanGoodsAddArea()
        {
            comboBoxGoodsType.SelectedIndex = -1;
            comboBoxGoodsVolunteer.Text = "";
            textBoxGoodsName.Text = "";
            textBoxGoodsNeeded.Text = "";
            textBoxGoodsAmount.Text = "";
        }
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

        private void buttonGoodsAdd_Click(object sender, EventArgs e)
        {
            
            bool f = checkGoodsAddArea();
            if (f)
            {
            //Проверка, есть ли уже в базе этот продукт
            PetShelter psGoodsFoChecking = new PetShelter();
            psGoodsFoChecking = ibl.getGoods();
            String filter = "NameOfGoods LIKE '" + textBoxGoodsName.Text + "'";
            psGoodsFoChecking.goods.DefaultView.RowFilter = string.Format(filter);
            if (psGoodsFoChecking.goods.Count != 0)
            {
                MessageBox.Show("Этот предмет уже есть в базе! Попробуйте изменить.");
                return;
            }


            dataGridViewGoodsAllGoods.DataSource = psGoods.goods.DefaultView;
            setGoodsGridView();
            dataGridViewGoodsAllGoods.ClearSelection();
            cleanGoodsAddArea();   


                //if (checkBoxAddAsNewGood.Checked)
                //{
                string n = textBoxGoodsName.Text;
                int t = Convert.ToInt32(psGoodstype.goodstype.Rows[comboBoxGoodsType.SelectedIndex][0]);
                int ga = Convert.ToInt32(textBoxGoodsAmount.Text);
                int gn = Convert.ToInt32(textBoxGoodsNeeded.Text);
                DateTime gad = dateTimePickerGoods.Value;

                psGoods.goods.AddgoodsRow(n, t, ga, gn);
                ibl.setGoods(psGoods);

                if (comboBoxGoodsVolunteer.Text != "")
                {
                    int gi = Convert.ToInt32(psGoods.goods.Rows[psGoods.goods.Count - 1][0]);
                    int ui = Convert.ToInt32(psUsers.users.Rows[comboBoxGoodsVolunteer.SelectedIndex][0]);
                    psDebitcredit.debitcredit.AdddebitcreditRow(gi, null, gad, 0, ga, -1, ui);
                }
                else
                {
                    int gi = Convert.ToInt32(psGoods.goods.Rows[psGoods.goods.Count - 1][0]);
                    psDebitcredit.debitcredit.AdddebitcreditRow(gi, null, gad, 0, ga, -1, -1);
                }

                ibl.setDebitCredit(psDebitcredit);
                dataGridViewGoodsAllGoods.Refresh();
                cleanGoodsAddArea();
                //}
            }
   
        }

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
                    if (comboBoxGoodsVolunteer.Text != "")
                    {
                        int gi = Convert.ToInt32(psGoods.goods.Rows[psGoods.goods.Count - 1][0]);
                        int ui = Convert.ToInt32(psUsers.users.Rows[comboBoxGoodsVolunteer.SelectedIndex][0]);
                        psDebitcredit.debitcredit.AdddebitcreditRow(gi, null, gad, d, c, -1, ui);
                    }
                    else
                    {
                        int gi = Convert.ToInt32(psGoods.goods.Rows[psGoods.goods.Count - 1][0]);
                        psDebitcredit.debitcredit.AdddebitcreditRow(gi, null, gad, d, c, -1, -1);
                    }
                    ibl.setDebitCredit(psDebitcredit);
                }
                dataGridViewGoodsAllGoods.Refresh();
                cleanGoodsAddArea();

            }
            else
            {
                MessageBox.Show("Сначала выберите животное в таблице");
            }
        }

        #endregion

        private void tableLayoutPanelAfterGoodTable_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridViewPetsAllPets_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label35_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxPetPhoto_Click(object sender, EventArgs e)
        {
            if (openFileDialogAddPetPhoto.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = openFileDialogAddPetPhoto.FileName;

        }
    }
}