using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOOShoes
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            FormClosing += LoginForm_FormClosing;
        }
        private void ButtonEnter_Click(object sender, EventArgs e)
        {
            string login = TextBoxLogin.Text;
            string password = TextBoxPassword.Text;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите логин и пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var user = OOOShoesLib.DBOperations.GetUser(login, password);
            if (user != null)
            {
                var mainForm = new MainForm(user);
                mainForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ButtonEnterGuest_Click(object sender, EventArgs e)
        {
            var mainForm = new MainForm(null); // null означает гость
            mainForm.Show();
            this.Hide();
        }
        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
