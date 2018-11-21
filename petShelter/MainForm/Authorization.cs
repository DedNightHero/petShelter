using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MainForm
{
    public partial class Authorization : Form
    {
        #region Инициализация переменных
        PetShelter psUsers = new PetShelter();
        BLogic.IBL ibl = new BLogic.BLogic();
        #endregion
        #region Инициализация формы
        public Authorization()
        {
            InitializeComponent();
        }
        #endregion
        #region Генерация хэша
        public static string CreateMD5(string input)
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
        #region Переход в текстбоксы по лейблам
        private void labelLogin_Click(object sender, EventArgs e)
        {
            textBoxLogin.Focus();
        }

        private void labelPass_Click(object sender, EventArgs e)
        {
            textBoxPass.Focus();
        }
        #endregion
        #region Кнопка отображения пароля
        private void buttonShowPass_MouseEnter(object sender, EventArgs e)
        {
            if(textBoxPass.Text!="Введите пароль")
                textBoxPass.PasswordChar = '\0';
        }

        private void buttonShowPass_MouseLeave(object sender, EventArgs e)
        {
            if (textBoxPass.Text != "Введите пароль")
                textBoxPass.PasswordChar = (new TextBox() { UseSystemPasswordChar = true }).PasswordChar;
        }
        #endregion
        #region Поле ввода логина
        private void textBoxLogin_Enter(object sender, EventArgs e)
        {
            if(textBoxLogin.Text=="Введите логин")
            {
                textBoxLogin.Text = "";
                textBoxLogin.ForeColor = Color.Black;
            }
        }

        private void textBoxLogin_Leave(object sender, EventArgs e)
        {
            if(textBoxLogin.Text == "")
            {
                textBoxLogin.Text = "Введите логин";
                textBoxLogin.ForeColor = Color.Gray;
            }
        }
        #endregion
        #region Поле ввода пароля
        private void textBoxPass_Enter(object sender, EventArgs e)
        {
            if (textBoxPass.Text == "Введите пароль")
            {
                textBoxPass.PasswordChar = (new TextBox() { UseSystemPasswordChar = true }).PasswordChar;
                textBoxPass.Text = "";
                textBoxPass.ForeColor = Color.Black;
            }
        }

        private void textBoxPass_Leave(object sender, EventArgs e)
        {
            if (textBoxPass.Text == "")
            {
                textBoxPass.PasswordChar = '\0';
                textBoxPass.Text = "Введите пароль";
                textBoxPass.ForeColor = Color.Gray;
            }
        }
        #endregion
        #region Очистка полей ввода
        private void clearTextBoxes()
        {
            textBoxLogin.Text = "Введите логин";
            textBoxLogin.ForeColor = Color.Gray;
            textBoxPass.Text = "Введите пароль";
            textBoxPass.ForeColor = Color.Gray;
            textBoxPass.PasswordChar = '\0';
        }
        #endregion
        #region Фильтр разрешенных символов
        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char symbol = e.KeyChar;
            if ((symbol < 64 || symbol > 90) && (symbol < 97 || symbol > 122) && (symbol < 47 || symbol > 58) && symbol != 8 && symbol != 1 && symbol != 3 && symbol != 22 && symbol != 24 && symbol != 9)
                e.Handled = true;
        }
        #endregion
        #region Кнопка входа
        private void buttonEnter_Click(object sender, EventArgs e)
        {
            psUsers = ibl.getUsers();
            if (textBoxLogin.Text == "Введите логин" || textBoxLogin.Text == "")
            {
                MessageBox.Show("Нужно ввести логин", "Ошибка", MessageBoxButtons.OK);
                return;
            }
            if (textBoxPass.Text == "Введите пароль" || textBoxPass.Text == "")
            {
                MessageBox.Show("Нужно ввести пароль", "Ошибка", MessageBoxButtons.OK);
                return;
            }
            string login = textBoxLogin.Text;
            string password = CreateMD5(textBoxPass.Text);
            if (psUsers.users.Select("Login LIKE '" + login + "' and Password LIKE '" + password +"'").Length == 1)
            {
                int lvl = Convert.ToInt32(psUsers.users.Select("Login LIKE '" + login + "' and Password LIKE '" + password + "'")[0][4].ToString());
                int id = Convert.ToInt32(psUsers.users.Select("Login LIKE '" + login + "' and Password LIKE '" + password + "'")[0][0].ToString());
                EmployeeForm ef1 = new EmployeeForm(lvl, id);
                ef1.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Логин или пароль введены не верно", "Ошибка", MessageBoxButtons.OK);
                clearTextBoxes();
                return;
            }
        }
        #endregion
        #region Кнопка входа волонтёров
        private void buttonEnterAs_Click(object sender, EventArgs e)
        {
            psUsers = ibl.getUsers();
            if (textBoxLogin.Text == "Введите логин" || textBoxLogin.Text == "" || textBoxLogin.Text =="admin")
            {
                MessageBox.Show("Нужно ввести логин", "Ошибка", MessageBoxButtons.OK);
                return;
            }
            string login = textBoxLogin.Text;
            //if (psUsers.users.Select("Login LIKE '" + login + "' and Password is NULL").Length == 1)
            if (psUsers.users.Select("Login LIKE '" + login + "'").Length == 1)
            {
                //int id = Convert.ToInt32(psUsers.users.Select("Login LIKE '" + login + "' and Password is NULL")[0][0].ToString());
                int id = Convert.ToInt32(psUsers.users.Select("Login LIKE '" + login + "'")[0][0].ToString());
                VolunteerForm vf1 = new VolunteerForm(id);
                vf1.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Логин введен не верно", "Ошибка", MessageBoxButtons.OK);
                clearTextBoxes();
                return;
            }
        }
        #endregion
    }
}