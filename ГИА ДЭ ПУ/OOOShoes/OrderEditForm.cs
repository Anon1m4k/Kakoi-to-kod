using OOOShoesLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace OOOShoes
{
    public partial class OrderEditForm : Form
    {
        private int? _orderId;
        private Users _currentUser;

        public OrderEditForm(Users currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
            this.Text = "Добавление заказа";
        }

        public OrderEditForm(int orderId, Users currentUser) : this(currentUser)
        {
            _orderId = orderId;
            this.Text = "Редактирование заказа";
        }

        private void OrderEditForm_Load(object sender, EventArgs e)
        {
            LoadComboBoxes();
            if (_orderId.HasValue)
                LoadOrderData(_orderId.Value);
            else
            {
                // Установка значений по умолчанию
                dtpOrderDate.Value = DateTime.Today;
                dtpDeliveryDate.Value = DateTime.Today.AddDays(7);
                cmbStatus.SelectedIndex = 0; // "Новый"
            }
        }

        private void LoadComboBoxes()
        {
            // Загрузка пунктов выдачи
            cmbPickupPoint.DataSource = DBOperations.GetPickupPoints();
            cmbPickupPoint.DisplayMember = "Address";
            cmbPickupPoint.ValueMember = "Id";
            cmbPickupPoint.SelectedIndex = -1;
        }

        private void LoadOrderData(int orderId)
        {
            var order = DBOperations.GetOrderById(orderId);
            if (order == null) return;

            dtpOrderDate.Value = order.OrderDate ?? DateTime.Today;
            dtpDeliveryDate.Value = order.DeliveryDate ?? DateTime.Today.AddDays(7);
            cmbPickupPoint.SelectedValue = order.PickupPointId;
            cmbStatus.SelectedItem = order.Status;

            // Загрузка позиций заказа в многострочное текстовое поле
            var items = DBOperations.GetOrderItems(orderId);
            TextBoxArticle.Clear();
            foreach (var item in items)
            {
                TextBoxArticle.Text += $"{item.Article} {item.Quantity}{Environment.NewLine}";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Валидация
            if (cmbPickupPoint.SelectedValue == null)
            {
                MessageBox.Show("Выберите пункт выдачи.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmbStatus.SelectedItem == null)
            {
                MessageBox.Show("Выберите статус заказа.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Парсинг позиций из TextBoxArticle
            var items = new List<OrderProducts>();
            var lines = TextBoxArticle.Text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                var parts = line.Trim().Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 0) continue;
                string article = parts[0];
                int quantity = 1;
                if (parts.Length >= 2 && !int.TryParse(parts[1], out quantity))
                {
                    quantity = 1; // если не число, ставим 1
                }
                // Проверяем существование товара
                var product = DBOperations.GetProductByArticle(article);
                if (product == null)
                {
                    MessageBox.Show($"Товар с артикулом '{article}' не найден.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                items.Add(new OrderProducts
                {
                    Article = article,
                    Quantity = quantity
                });
            }

            // Сбор данных заказа
            var order = new Orders
            {
                OrderId = _orderId ?? 0,
                ClientId = _currentUser?.Id ?? 1,
                OrderDate = dtpOrderDate.Value,
                DeliveryDate = dtpDeliveryDate.Value,
                PickupPointId = (int)cmbPickupPoint.SelectedValue,
                Status = cmbStatus.SelectedItem.ToString(),
                PickupCode = new Random().Next(100, 999) // генерация кода получения
            };

            try
            {
                if (_orderId.HasValue)
                    DBOperations.UpdateOrder(order, items);
                else
                    DBOperations.AddOrder(order, items);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}