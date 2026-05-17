using OOOShoesLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace OOOShoes
{
    public partial class MainForm : Form
    {
        private Users _currentUser;
        private UserControl _selectedProductControl = null;

        public MainForm(Users user)
        {
            InitializeComponent();
            _currentUser = user;
            DisplayUserInfo();
            this.FormClosing += LoginForm_FormClosing;

            // Подписка на события для поиска/фильтрации/сортировки
            TextBoxSearch.TextChanged += (s, e) => ApplyFilterAndSort();
            ComboBoxFilterBySupplier.SelectedIndexChanged += (s, e) => ApplyFilterAndSort();
            ComboBoxSort.SelectedIndexChanged += (s, e) => ApplyFilterAndSort();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Загрузка списка поставщиков для фильтра
            ComboBoxFilterBySupplier.Items.Clear();
            ComboBoxFilterBySupplier.Items.Add("Все поставщики");
            foreach (var supplier in DBOperations.GetSuppliers())
            {
                ComboBoxFilterBySupplier.Items.Add(supplier);
            }
            ComboBoxFilterBySupplier.SelectedIndex = 0;

            // Настройка видимости кнопок управления
            bool isAdmin = _currentUser != null && _currentUser.Role == "Администратор";
            bool isManager = _currentUser != null && _currentUser.Role == "Менеджер";
            bool canManage = isAdmin; // только админ может добавлять/удалять
            bool canUseFilters = isAdmin || isManager; // фильтры доступны админу и менеджеру

            ButtonAdd.Visible = canManage;
            ButtonDelete.Visible = canManage;

            // Видимость элементов поиска, фильтрации и сортировки
            TextBoxSearch.Visible = canUseFilters;
            ComboBoxFilterBySupplier.Visible = canUseFilters;
            ComboBoxSort.Visible = canUseFilters;
            LabelSearch.Visible = canUseFilters;
            LabelFilterBySupplier.Visible = canUseFilters;
            LabelSort.Visible = canUseFilters;

            ApplyFilterAndSort(); // первоначальная загрузка
        }

        private void ApplyFilterAndSort()
        {
            string searchText = TextBoxSearch.Text.Trim();
            string selectedSupplier = ComboBoxFilterBySupplier.SelectedItem?.ToString();
            string sortOption = ComboBoxSort.SelectedItem?.ToString();

            List<Products> products;

            // Поиск (если есть текст)
            if (!string.IsNullOrEmpty(searchText))
            {
                products = DBOperations.SearchProducts(searchText);
            }
            else
            {
                products = DBOperations.GetAllProducts();
            }

            // Фильтрация по поставщику
            if (selectedSupplier != null && selectedSupplier != "Все поставщики")
            {
                products = products.Where(p => p.Supplier == selectedSupplier).ToList();
            }

            // Сортировка
            switch (sortOption)
            {
                case "По наименованию (А-Я)":
                    products = products.OrderBy(p => p.Name).ToList();
                    break;
                case "По наименованию (Я-А)":
                    products = products.OrderByDescending(p => p.Name).ToList();
                    break;
                case "По цене (возрастание)":
                    products = products.OrderBy(p => p.Price).ToList();
                    break;
                case "По цене (убывание)":
                    products = products.OrderByDescending(p => p.Price).ToList();
                    break;
                case "По количеству (возрастание)":
                    products = products.OrderBy(p => p.Quantity).ToList();
                    break;
                case "По количеству (убывание)":
                    products = products.OrderByDescending(p => p.Quantity).ToList();
                    break;
                default:
                    // без сортировки – порядок по артикулу
                    products = products.OrderBy(p => p.Article).ToList();
                    break;
            }

            // Отображение товаров
            DisplayProducts(products);
            _selectedProductControl = null;
        }

        private void DisplayProducts(List<Products> products)
        {
            FlowLayoutPanel.Controls.Clear();
            foreach (var prod in products)
            {
                var productControl = new UserControl();
                productControl.SetProduct(prod);
                productControl.Click += ProductControl_Click;
                productControl.DoubleClick += ProductControl_DoubleClick;
                FlowLayoutPanel.Controls.Add(productControl);
            }
        }

        private void ProductControl_Click(object sender, EventArgs e)
        {
            var control = sender as UserControl;
            if (control != null)
            {
                SelectProduct(control);
            }
        }

        private void ProductControl_DoubleClick(object sender, EventArgs e)
        {
            var control = sender as UserControl;
            if (control != null)
            {
                EditProduct(control);
            }
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

        public void SelectProduct(UserControl control)
        {
            if (_selectedProductControl != null)
            {
                _selectedProductControl.SetSelected(false);
            }
            _selectedProductControl = control;
            _selectedProductControl.SetSelected(true);
        }

        public void EditProduct(UserControl control)
        {
            // Только администратор может редактировать
            if (_currentUser == null || _currentUser.Role != "Администратор")
                return;

            string article = control.GetArticle();
            using (var editForm = new ProductEditForm(article))
            {
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    ApplyFilterAndSort();
                }
            }
        }

        private void ButtonLogout_Click(object sender, EventArgs e)
        {
            // Возврат к окну авторизации
            LoginForm loginForm = new LoginForm();
            this.Hide();
            loginForm.Show();
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            using (var editForm = new ProductEditForm())
            {
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    ApplyFilterAndSort(); // обновить список
                }
            }
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            if (_selectedProductControl == null)
            {
                MessageBox.Show("Выберите товар для удаления.", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string article = _selectedProductControl.GetArticle();
            int orderCount = DBOperations.GetOrdersByProduct(article);
            if (orderCount > 0)
            {
                MessageBox.Show("Невозможно удалить товар, так как он присутствует в заказах.",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Удалить выбранный товар?", "Подтверждение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var product = DBOperations.GetProductByArticle(article);
                if (product != null && product.Photo != "picture.png")
                {
                    string photoPath = Path.Combine(Application.StartupPath, product.Photo);
                    if (File.Exists(photoPath))
                        File.Delete(photoPath);
                }

                DBOperations.DeleteProduct(article);
                ApplyFilterAndSort();
            }
        }
    }
}