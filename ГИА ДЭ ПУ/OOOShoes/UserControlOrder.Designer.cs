namespace OOOShoes
{
    partial class UserControlOrder
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.LabelPickupPoint = new System.Windows.Forms.Label();
            this.LabelOrderDate = new System.Windows.Forms.Label();
            this.LabelId = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.LabelDeliveryDate = new System.Windows.Forms.Label();
            this.GroupBoxOrder = new System.Windows.Forms.GroupBox();
            this.LabelStatus = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.GroupBoxOrder.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.LabelStatus);
            this.groupBox1.Controls.Add(this.LabelId);
            this.groupBox1.Controls.Add(this.LabelOrderDate);
            this.groupBox1.Controls.Add(this.LabelPickupPoint);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(369, 97);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // LabelPickupPoint
            // 
            this.LabelPickupPoint.AutoSize = true;
            this.LabelPickupPoint.Location = new System.Drawing.Point(6, 51);
            this.LabelPickupPoint.Name = "LabelPickupPoint";
            this.LabelPickupPoint.Size = new System.Drawing.Size(126, 15);
            this.LabelPickupPoint.TabIndex = 3;
            this.LabelPickupPoint.Text = "Адрес пункта выдачи";
            // 
            // LabelOrderDate
            // 
            this.LabelOrderDate.AutoSize = true;
            this.LabelOrderDate.Location = new System.Drawing.Point(6, 69);
            this.LabelOrderDate.Name = "LabelOrderDate";
            this.LabelOrderDate.Size = new System.Drawing.Size(70, 15);
            this.LabelOrderDate.TabIndex = 4;
            this.LabelOrderDate.Text = "Дата заказа";
            // 
            // LabelId
            // 
            this.LabelId.AutoSize = true;
            this.LabelId.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LabelId.Location = new System.Drawing.Point(6, 15);
            this.LabelId.Name = "LabelId";
            this.LabelId.Size = new System.Drawing.Size(93, 15);
            this.LabelId.TabIndex = 0;
            this.LabelId.Text = "Артикул заказа";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.LabelDeliveryDate);
            this.groupBox2.Location = new System.Drawing.Point(378, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(147, 97);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            // 
            // LabelDeliveryDate
            // 
            this.LabelDeliveryDate.AutoSize = true;
            this.LabelDeliveryDate.Location = new System.Drawing.Point(6, 33);
            this.LabelDeliveryDate.Name = "LabelDeliveryDate";
            this.LabelDeliveryDate.Size = new System.Drawing.Size(85, 15);
            this.LabelDeliveryDate.TabIndex = 5;
            this.LabelDeliveryDate.Text = "Дата доставки";
            // 
            // GroupBoxOrder
            // 
            this.GroupBoxOrder.Controls.Add(this.groupBox2);
            this.GroupBoxOrder.Controls.Add(this.groupBox1);
            this.GroupBoxOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GroupBoxOrder.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.GroupBoxOrder.Location = new System.Drawing.Point(0, 0);
            this.GroupBoxOrder.Name = "GroupBoxOrder";
            this.GroupBoxOrder.Size = new System.Drawing.Size(531, 106);
            this.GroupBoxOrder.TabIndex = 0;
            this.GroupBoxOrder.TabStop = false;
            // 
            // LabelStatus
            // 
            this.LabelStatus.AutoSize = true;
            this.LabelStatus.Location = new System.Drawing.Point(9, 35);
            this.LabelStatus.Name = "LabelStatus";
            this.LabelStatus.Size = new System.Drawing.Size(81, 15);
            this.LabelStatus.TabIndex = 5;
            this.LabelStatus.Text = "Статус заказа";
            // 
            // UserControlOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GroupBoxOrder);
            this.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "UserControlOrder";
            this.Size = new System.Drawing.Size(531, 106);
            this.Click += new System.EventHandler(this.UserControlOrder_Click);
            this.DoubleClick += new System.EventHandler(this.UserControlOrder_DoubleClick);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.GroupBoxOrder.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label LabelId;
        private System.Windows.Forms.Label LabelOrderDate;
        private System.Windows.Forms.Label LabelPickupPoint;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label LabelDeliveryDate;
        private System.Windows.Forms.GroupBox GroupBoxOrder;
        private System.Windows.Forms.Label LabelStatus;
    }
}