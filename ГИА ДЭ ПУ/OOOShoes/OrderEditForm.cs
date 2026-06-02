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

            TextBoxArticle.Clear();
            if (string.IsNullOrWhiteSpace(order.ArticleString))
                return;

            // Разбираем строку артикулов и количества
            var parts = order.ArticleString
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim())
                .ToArray();

            for (int i = 0; i < parts.Length; i++)
            {
                string article = parts[i];
                int quantity = 1;

                // Если следующий элемент является числом — это количество
                if (i + 1 < parts.Length && int.TryParse(parts[i + 1], out int q))
                {
                    quantity = q;
                    i++; // пропускаем количество
                }
                // В любом случае выводим строку "артикул, количество"
                TextBoxArticle.AppendText($"{article}, {quantity}{Environment.NewLine}");
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

            var lines = TextBoxArticle.Text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var articleEntries = new List<string>();

            foreach (var line in lines)
            {
                // Ожидаем строку: "артикул, количество" (разделитель – запятая)
                var parts = line.Trim().Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 0) continue;

                string article = parts[0].Trim();
                if (string.IsNullOrEmpty(article))
                    continue;

                // Проверка существования товара
                var product = DBOperations.GetProductByArticle(article);
                if (product == null)
                {
                    MessageBox.Show($"Товар с артикулом '{article}' не найден.", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int quantity = 1;               

                if (parts.Length >= 2 && int.TryParse(parts[1].Trim(), out int parsedQty) && parsedQty >= 1)
                quantity = parsedQty;

                if (quantity > product.Quantity)
                {
                    MessageBox.Show($"Недостаточно товара на складе.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Добавляем пару "артикул, количество"
                articleEntries.Add($"{article}, {quantity}");
            }

            if (articleEntries.Count == 0)
            {
                MessageBox.Show("Добавьте хотя бы один товар.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Итоговая строка: "B320R5, 2, B320R5, 2"
            string articleString = string.Join(", ", articleEntries);

            // Определение ID заказа
            int orderId;
            if (_orderId.HasValue)
                orderId = _orderId.Value;
            else
            {
                var allOrders = DBOperations.GetAllOrders();
                orderId = allOrders.Any() ? allOrders.Max(o => o.OrderId) + 1 : 1;
            }

            var order = new Orders
            {
                OrderId = orderId,
                ClientId = _currentUser?.Id ?? 1,
                ClientName = _currentUser?.FullName ?? "",
                OrderDate = dtpOrderDate.Value,
                DeliveryDate = dtpDeliveryDate.Value,
                PickupPointId = (int)cmbPickupPoint.SelectedValue,
                Status = cmbStatus.SelectedItem.ToString(),
                PickupCode = new Random().Next(100, 999),
                ArticleString = articleString
            };

            try
            {
                if (_orderId.HasValue)
                    DBOperations.UpdateOrder(order);
                else
                    DBOperations.AddOrder(order);

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