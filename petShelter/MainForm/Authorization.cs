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
    public partial class Authorization : Form
    {
        public Authorization()
        {
            InitializeComponent();
        }

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

        private void labelLogin_Click(object sender, EventArgs e)
        {
            textBoxLogin.Focus();
        }

        private void labelPass_Click(object sender, EventArgs e)
        {
            textBoxPass.Focus();
        }

        private void buttonShowPass_MouseEnter(object sender, EventArgs e)
        {
            if(textBoxPass.Text!="Введите пароль")
                textBoxPass.UseSystemPasswordChar = false;
        }

        private void buttonShowPass_MouseLeave(object sender, EventArgs e)
        {
            if (textBoxPass.Text != "Введите пароль")
                textBoxPass.UseSystemPasswordChar = true;
        }

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

        private void textBoxPass_Enter(object sender, EventArgs e)
        {
            if (textBoxPass.Text == "Введите пароль")
            {
                textBoxPass.Text = "";
                textBoxPass.ForeColor = Color.Black;
                textBoxPass.UseSystemPasswordChar = true;
            }
        }

        private void textBoxPass_Leave(object sender, EventArgs e)
        {
            if (textBoxPass.Text == "")
            {
                textBoxPass.Text = "Введите пароль";
                textBoxPass.ForeColor = Color.Gray;
                textBoxPass.UseSystemPasswordChar = false;
            }
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char symbol = e.KeyChar;
            if ((symbol < 64 || symbol > 90) && (symbol < 97 || symbol > 122) && symbol != 8 && symbol != 1 && symbol != 3 && symbol != 22 && symbol != 24)
                e.Handled = true;
        }

        private void buttonEnter_Click(object sender, EventArgs e)
        {
            if (textBoxLogin.Text == "Введите логин" || textBoxLogin.Text == "")
            {
                MessageBox.Show("Нужно ввести пароль", "Ошибка", MessageBoxButtons.OK);
                return;
            }
            string login = textBoxLogin.Text;
            string password = CreateMD5(textBoxPass.Text);
            textBoxLogin.Text = password;
            //Проверка и открытие формы сотрудника

        }

        private void buttonEnterAs_Click(object sender, EventArgs e)
        {
            if (textBoxLogin.Text == "Введите логин" || textBoxLogin.Text == "")
            {
                MessageBox.Show("Нужно ввести пароль", "Ошибка", MessageBoxButtons.OK);
                return;
            }
            string login = textBoxLogin.Text;
            //Проверка и открытие формы волонтера

        }
    }
}
