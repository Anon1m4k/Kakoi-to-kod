namespace OOOShoes
{
    partial class UserControl
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
            this.PictureBoxPhoto = new System.Windows.Forms.PictureBox();
            this.GroupBoxProduct = new System.Windows.Forms.GroupBox();
            this.FinalPriceLabel = new System.Windows.Forms.Label();
            this.LabelQuantity = new System.Windows.Forms.Label();
            this.LabelUnit = new System.Windows.Forms.Label();
            this.LabelPrice = new System.Windows.Forms.Label();
            this.LabelManufacturer = new System.Windows.Forms.Label();
            this.LabelSupplier = new System.Windows.Forms.Label();
            this.LabelDescription = new System.Windows.Forms.Label();
            this.LabelName = new System.Windows.Forms.Label();
            this.LabelCategory = new System.Windows.Forms.Label();
            this.LabelDiscount = new System.Windows.Forms.Label();
            this.GroupBoxDiscount = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxPhoto)).BeginInit();
            this.GroupBoxProduct.SuspendLayout();
            this.GroupBoxDiscount.SuspendLayout();
            this.SuspendLayout();
            // 
            // PictureBoxPhoto
            // 
            this.PictureBoxPhoto.Location = new System.Drawing.Point(4, 4);
            this.PictureBoxPhoto.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.PictureBoxPhoto.Name = "PictureBoxPhoto";
            this.PictureBoxPhoto.Size = new System.Drawing.Size(176, 166);
            this.PictureBoxPhoto.TabIndex = 0;
            this.PictureBoxPhoto.TabStop = false;
            // 
            // GroupBoxProduct
            // 
            this.GroupBoxProduct.Controls.Add(this.FinalPriceLabel);
            this.GroupBoxProduct.Controls.Add(this.LabelQuantity);
            this.GroupBoxProduct.Controls.Add(this.LabelUnit);
            this.GroupBoxProduct.Controls.Add(this.LabelPrice);
            this.GroupBoxProduct.Controls.Add(this.LabelManufacturer);
            this.GroupBoxProduct.Controls.Add(this.LabelSupplier);
            this.GroupBoxProduct.Controls.Add(this.LabelDescription);
            this.GroupBoxProduct.Controls.Add(this.LabelName);
            this.GroupBoxProduct.Controls.Add(this.LabelCategory);
            this.GroupBoxProduct.Location = new System.Drawing.Point(188, 4);
            this.GroupBoxProduct.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBoxProduct.Name = "GroupBoxProduct";
            this.GroupBoxProduct.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBoxProduct.Size = new System.Drawing.Size(457, 165);
            this.GroupBoxProduct.TabIndex = 1;
            this.GroupBoxProduct.TabStop = false;
            // 
            // FinalPriceLabel
            // 
            this.FinalPriceLabel.AutoSize = true;
            this.FinalPriceLabel.Location = new System.Drawing.Point(128, 100);
            this.FinalPriceLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.FinalPriceLabel.Name = "FinalPriceLabel";
            this.FinalPriceLabel.Size = new System.Drawing.Size(0, 15);
            this.FinalPriceLabel.TabIndex = 3;
            // 
            // LabelQuantity
            // 
            this.LabelQuantity.AutoSize = true;
            this.LabelQuantity.Location = new System.Drawing.Point(7, 135);
            this.LabelQuantity.Name = "LabelQuantity";
            this.LabelQuantity.Size = new System.Drawing.Size(129, 15);
            this.LabelQuantity.TabIndex = 7;
            this.LabelQuantity.Text = "Количество на складе:";
            // 
            // LabelUnit
            // 
            this.LabelUnit.AutoSize = true;
            this.LabelUnit.Location = new System.Drawing.Point(7, 117);
            this.LabelUnit.Name = "LabelUnit";
            this.LabelUnit.Size = new System.Drawing.Size(121, 15);
            this.LabelUnit.TabIndex = 6;
            this.LabelUnit.Text = "Единица измерений:";
            // 
            // LabelPrice
            // 
            this.LabelPrice.AutoSize = true;
            this.LabelPrice.Location = new System.Drawing.Point(8, 100);
            this.LabelPrice.Name = "LabelPrice";
            this.LabelPrice.Size = new System.Drawing.Size(38, 15);
            this.LabelPrice.TabIndex = 5;
            this.LabelPrice.Text = "Цена:";
            // 
            // LabelManufacturer
            // 
            this.LabelManufacturer.AutoSize = true;
            this.LabelManufacturer.Location = new System.Drawing.Point(8, 83);
            this.LabelManufacturer.Name = "LabelManufacturer";
            this.LabelManufacturer.Size = new System.Drawing.Size(72, 15);
            this.LabelManufacturer.TabIndex = 4;
            this.LabelManufacturer.Text = "Поставщик:";
            // 
            // LabelSupplier
            // 
            this.LabelSupplier.AutoSize = true;
            this.LabelSupplier.Location = new System.Drawing.Point(8, 65);
            this.LabelSupplier.Name = "LabelSupplier";
            this.LabelSupplier.Size = new System.Drawing.Size(94, 15);
            this.LabelSupplier.TabIndex = 3;
            this.LabelSupplier.Text = "Производитель:";
            // 
            // LabelDescription
            // 
            this.LabelDescription.AutoSize = true;
            this.LabelDescription.Location = new System.Drawing.Point(7, 48);
            this.LabelDescription.Name = "LabelDescription";
            this.LabelDescription.Size = new System.Drawing.Size(105, 15);
            this.LabelDescription.TabIndex = 2;
            this.LabelDescription.Text = "Описание товара:";
            // 
            // LabelName
            // 
            this.LabelName.AutoSize = true;
            this.LabelName.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LabelName.Location = new System.Drawing.Point(319, 18);
            this.LabelName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelName.Name = "LabelName";
            this.LabelName.Size = new System.Drawing.Size(130, 15);
            this.LabelName.TabIndex = 1;
            this.LabelName.Text = "Наименование товара";
            // 
            // LabelCategory
            // 
            this.LabelCategory.AutoSize = true;
            this.LabelCategory.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LabelCategory.Location = new System.Drawing.Point(8, 18);
            this.LabelCategory.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelCategory.Name = "LabelCategory";
            this.LabelCategory.Size = new System.Drawing.Size(106, 15);
            this.LabelCategory.TabIndex = 0;
            this.LabelCategory.Text = "Категория товара";
            // 
            // LabelDiscount
            // 
            this.LabelDiscount.AutoSize = true;
            this.LabelDiscount.Location = new System.Drawing.Point(7, 70);
            this.LabelDiscount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelDiscount.Name = "LabelDiscount";
            this.LabelDiscount.Size = new System.Drawing.Size(128, 15);
            this.LabelDiscount.TabIndex = 2;
            this.LabelDiscount.Text = "Действующая скидка:";
            // 
            // GroupBoxDiscount
            // 
            this.GroupBoxDiscount.Controls.Add(this.LabelDiscount);
            this.GroupBoxDiscount.Location = new System.Drawing.Point(653, 5);
            this.GroupBoxDiscount.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBoxDiscount.Name = "GroupBoxDiscount";
            this.GroupBoxDiscount.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBoxDiscount.Size = new System.Drawing.Size(156, 165);
            this.GroupBoxDiscount.TabIndex = 3;
            this.GroupBoxDiscount.TabStop = false;
            // 
            // UserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GroupBoxProduct);
            this.Controls.Add(this.GroupBoxDiscount);
            this.Controls.Add(this.PictureBoxPhoto);
            this.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "UserControl";
            this.Size = new System.Drawing.Size(815, 175);
            this.Click += new System.EventHandler(this.UserControl_Click);
            this.DoubleClick += new System.EventHandler(this.UserControl_DoubleClick);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxPhoto)).EndInit();
            this.GroupBoxProduct.ResumeLayout(false);
            this.GroupBoxProduct.PerformLayout();
            this.GroupBoxDiscount.ResumeLayout(false);
            this.GroupBoxDiscount.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox PictureBoxPhoto;
        private System.Windows.Forms.GroupBox GroupBoxProduct;
        private System.Windows.Forms.Label LabelDiscount;
        private System.Windows.Forms.GroupBox GroupBoxDiscount;
        private System.Windows.Forms.Label LabelSupplier;
        private System.Windows.Forms.Label LabelDescription;
        private System.Windows.Forms.Label LabelName;
        private System.Windows.Forms.Label LabelCategory;
        private System.Windows.Forms.Label LabelQuantity;
        private System.Windows.Forms.Label LabelUnit;
        private System.Windows.Forms.Label LabelPrice;
        private System.Windows.Forms.Label LabelManufacturer;
        private System.Windows.Forms.Label FinalPriceLabel;
    }
}
