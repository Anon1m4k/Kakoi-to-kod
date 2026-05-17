using OOOShoesLib;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace OOOShoes
{
    public partial class OrdersForm : Form
    {
        private Users _currentUser;
        private UserControlOrder _selectedOrderControl = null;

        public OrdersForm(Users currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
            ConfigureAccess();
            LoadOrders();
        }

        private void ConfigureAccess()
        {
            bool isAdmin = _currentUser != null && _currentUser.Role == "Администратор";
            bool isManager = _currentUser != null && _currentUser.Role == "Менеджер";

            ButtonAdd.Visible = isAdmin;
            ButtonDelete.Visible = isAdmin;

            if (!isAdmin && !isManager)
            {
                MessageBox.Show("У вас нет прав для просмотра заказов.", "Доступ запрещён",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }
        }

        private void LoadOrders()
        {
            try
            {
                var orders = DBOperations.GetAllOrders();
                DisplayOrders(orders);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки заказов: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayOrders(List<Orders> orders)
        {
            flowLayoutPanel1.Controls.Clear();
            foreach (var order in orders)
            {
                var control = new UserControlOrder();
                control.SetOrder(order);
                flowLayoutPanel1.Controls.Add(control);
            }
        }

        public void SelectOrder(UserControlOrder control)
        {
            if (_selectedOrderControl != null)
                _selectedOrderControl.SetSelected(false);
            _selectedOrderControl = control;
            _selectedOrderControl.SetSelected(true);
        }

        public void EditOrder(UserControlOrder control)
        {
            if (_currentUser == null || _currentUser.Role != "Администратор") return;

            using (var editForm = new OrderEditForm(control.OrderId, _currentUser))
            {
                if (editForm.ShowDialog() == DialogResult.OK)
                    LoadOrders();
            }
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            using (var editForm = new OrderEditForm(_currentUser))
            {
                if (editForm.ShowDialog() == DialogResult.OK)
                    LoadOrders();
            }
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            if (_selectedOrderControl == null)
            {
                MessageBox.Show("Выберите заказ для удаления.", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show($"Удалить заказ №{_selectedOrderControl.OrderId}?",
                "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    DBOperations.DeleteOrder(_selectedOrderControl.OrderId);
                    LoadOrders();
                    _selectedOrderControl = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}