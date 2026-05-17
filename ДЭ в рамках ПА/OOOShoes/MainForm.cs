using OOOShoesLib;
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
    public partial class MainForm : Form
    {
        private Users _currentUser;

        public MainForm(Users user)
        {
            InitializeComponent();
            _currentUser = user;
            DisplayUserInfo();
            LoadProducts();
            this.FormClosing += LoginForm_FormClosing;
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void DisplayUserInfo()
        {
            if (_currentUser != null)
                LabelFullName.Text = _currentUser.FullName;
            else
                LabelFullName.Text = "Гость";
        }

        private void LoadProducts()
        {
            try
            {
                List<Products> products = DBOperations.GetAllProducts();
                FlowLayoutPanel.Controls.Clear(); // используем имя поля

                foreach (var prod in products)
                {
                    var productControl = new UserControl();
                    productControl.SetProduct(prod); // метод, который заполнит контрол данными
                    FlowLayoutPanel.Controls.Add(productControl);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке товаров: {ex.Message}", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonLogout_Click(object sender, EventArgs e)
        {
            // Возврат к окну авторизации
            LoginForm loginForm = new LoginForm();
            this.Hide();
            loginForm.Show();
        }
    }
}
