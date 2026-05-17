using OOOShoesLib;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace OOOShoes
{
    public partial class UserControlOrder : System.Windows.Forms.UserControl
    {

        private Orders _order;
        public int OrderId => _order?.OrderId ?? 0;

        public UserControlOrder()
        {
            InitializeComponent();
            // Рекурсивная подписка на клик и двойной клик для всех вложенных контролов
            SubscribeToClickEvents(this);
            this.BackColor = SystemColors.Control;
        }

        private void SubscribeToClickEvents(Control parent)
        {
            parent.Click += UserControlOrder_Click;
            parent.DoubleClick += UserControlOrder_DoubleClick;
            foreach (Control child in parent.Controls)
            {
                SubscribeToClickEvents(child);
            }
        }

        public void SetOrder(Orders order)
        {
            _order = order;
            GroupBoxOrder.Text = $"Артикул заказа: {order.OrderId}";
            LabelId.Text = $"Артикул заказа: {order.OrderId}";
            LabelStatus.Text = order.Status;
            LabelPickupPoint.Text = $"Адрес: {order.PickupAddress}";
            LabelOrderDate.Text = order.OrderDate.HasValue
                ? $"Дата заказа: {order.OrderDate.Value:dd.MM.yyyy}"
                : "Дата заказа: не указана";
            LabelDeliveryDate.Text = order.DeliveryDate.HasValue
                ? $"Дата выдачи: {order.DeliveryDate.Value:dd.MM.yyyy}"
                : "Дата выдачи: не указана";
        }

        public void SetSelected(bool selected)
        {
            if (selected)
                this.BackColor = Color.LightSteelBlue;
            else
                this.BackColor = SystemColors.Control;
        }

        private void UserControlOrder_Click(object sender, EventArgs e)
        {
            if (Parent is FlowLayoutPanel panel && panel.Parent is OrdersForm ordersForm)
                ordersForm.SelectOrder(this);
        }

        private void UserControlOrder_DoubleClick(object sender, EventArgs e)
        {
            if (Parent is FlowLayoutPanel panel && panel.Parent is OrdersForm ordersForm)
                ordersForm.EditOrder(this);
        }
    }
}