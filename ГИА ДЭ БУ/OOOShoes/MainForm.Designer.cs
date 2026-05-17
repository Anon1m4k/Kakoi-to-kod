namespace OOOShoes
{
    partial class MainForm
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

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.FlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.LabelFullName = new System.Windows.Forms.Label();
            this.ButtonLogout = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.TextBoxSearch = new System.Windows.Forms.TextBox();
            this.ComboBoxFilterBySupplier = new System.Windows.Forms.ComboBox();
            this.ComboBoxSort = new System.Windows.Forms.ComboBox();
            this.ButtonAdd = new System.Windows.Forms.Button();
            this.ButtonDelete = new System.Windows.Forms.Button();
            this.LabelSearch = new System.Windows.Forms.Label();
            this.LabelFilterBySupplier = new System.Windows.Forms.Label();
            this.LabelSort = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // FlowLayoutPanel
            // 
            this.FlowLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FlowLayoutPanel.AutoScroll = true;
            this.FlowLayoutPanel.BackColor = System.Drawing.Color.Chartreuse;
            this.FlowLayoutPanel.Location = new System.Drawing.Point(7, 75);
            this.FlowLayoutPanel.Name = "FlowLayoutPanel";
            this.FlowLayoutPanel.Size = new System.Drawing.Size(788, 354);
            this.FlowLayoutPanel.TabIndex = 0;
            // 
            // LabelFullName
            // 
            this.LabelFullName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelFullName.AutoSize = true;
            this.LabelFullName.Location = new System.Drawing.Point(623, 35);
            this.LabelFullName.Name = "LabelFullName";
            this.LabelFullName.Size = new System.Drawing.Size(76, 14);
            this.LabelFullName.TabIndex = 1;
            this.LabelFullName.Text = "LabelFullName";
            // 
            // ButtonLogout
            // 
            this.ButtonLogout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonLogout.BackColor = System.Drawing.Color.MediumSpringGreen;
            this.ButtonLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonLogout.Location = new System.Drawing.Point(688, 436);
            this.ButtonLogout.Name = "ButtonLogout";
            this.ButtonLogout.Size = new System.Drawing.Size(100, 36);
            this.ButtonLogout.TabIndex = 2;
            this.ButtonLogout.Text = "Выйти";
            this.ButtonLogout.UseVisualStyleBackColor = false;
            this.ButtonLogout.Click += new System.EventHandler(this.ButtonLogout_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::OOOShoes.Properties.Resources.Icon;
            this.pictureBox1.Location = new System.Drawing.Point(7, 10);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(55, 59);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // TextBoxSearch
            // 
            this.TextBoxSearch.Location = new System.Drawing.Point(68, 29);
            this.TextBoxSearch.Name = "TextBoxSearch";
            this.TextBoxSearch.Size = new System.Drawing.Size(145, 20);
            this.TextBoxSearch.TabIndex = 4;
            // 
            // ComboBoxFilterBySupplier
            // 
            this.ComboBoxFilterBySupplier.FormattingEnabled = true;
            this.ComboBoxFilterBySupplier.Items.AddRange(new object[] {
            "Все поставщики"});
            this.ComboBoxFilterBySupplier.Location = new System.Drawing.Point(219, 29);
            this.ComboBoxFilterBySupplier.Name = "ComboBoxFilterBySupplier";
            this.ComboBoxFilterBySupplier.Size = new System.Drawing.Size(149, 22);
            this.ComboBoxFilterBySupplier.TabIndex = 5;
            // 
            // ComboBoxSort
            // 
            this.ComboBoxSort.FormattingEnabled = true;
            this.ComboBoxSort.Items.AddRange(new object[] {
            "Без сортировки",
            "По наименованию (А‑Я)",
            "По наименованию (Я‑А)",
            "По цене (возрастание)",
            "По цене (убывание)",
            "По количеству (возрастание)",
            "По количеству (убывание)"});
            this.ComboBoxSort.Location = new System.Drawing.Point(374, 29);
            this.ComboBoxSort.Name = "ComboBoxSort";
            this.ComboBoxSort.Size = new System.Drawing.Size(149, 22);
            this.ComboBoxSort.TabIndex = 6;
            // 
            // ButtonAdd
            // 
            this.ButtonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ButtonAdd.BackColor = System.Drawing.Color.Transparent;
            this.ButtonAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonAdd.Location = new System.Drawing.Point(7, 437);
            this.ButtonAdd.Name = "ButtonAdd";
            this.ButtonAdd.Size = new System.Drawing.Size(100, 36);
            this.ButtonAdd.TabIndex = 7;
            this.ButtonAdd.Text = "Добавить товар";
            this.ButtonAdd.UseVisualStyleBackColor = false;
            this.ButtonAdd.Click += new System.EventHandler(this.ButtonAdd_Click);
            // 
            // ButtonDelete
            // 
            this.ButtonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ButtonDelete.BackColor = System.Drawing.Color.Transparent;
            this.ButtonDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonDelete.Location = new System.Drawing.Point(113, 437);
            this.ButtonDelete.Name = "ButtonDelete";
            this.ButtonDelete.Size = new System.Drawing.Size(100, 36);
            this.ButtonDelete.TabIndex = 8;
            this.ButtonDelete.Text = "Удалить";
            this.ButtonDelete.UseVisualStyleBackColor = false;
            this.ButtonDelete.Click += new System.EventHandler(this.ButtonDelete_Click);
            // 
            // LabelSearch
            // 
            this.LabelSearch.AutoSize = true;
            this.LabelSearch.Location = new System.Drawing.Point(68, 13);
            this.LabelSearch.Name = "LabelSearch";
            this.LabelSearch.Size = new System.Drawing.Size(37, 14);
            this.LabelSearch.TabIndex = 10;
            this.LabelSearch.Text = "Поиск";
            // 
            // LabelFilterBySupplier
            // 
            this.LabelFilterBySupplier.AutoSize = true;
            this.LabelFilterBySupplier.Location = new System.Drawing.Point(219, 13);
            this.LabelFilterBySupplier.Name = "LabelFilterBySupplier";
            this.LabelFilterBySupplier.Size = new System.Drawing.Size(117, 14);
            this.LabelFilterBySupplier.TabIndex = 11;
            this.LabelFilterBySupplier.Text = "Фильтр по поставщику";
            // 
            // LabelSort
            // 
            this.LabelSort.AutoSize = true;
            this.LabelSort.Location = new System.Drawing.Point(374, 13);
            this.LabelSort.Name = "LabelSort";
            this.LabelSort.Size = new System.Drawing.Size(64, 14);
            this.LabelSort.TabIndex = 12;
            this.LabelSort.Text = "Сортировка";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 485);
            this.Controls.Add(this.LabelSort);
            this.Controls.Add(this.LabelFilterBySupplier);
            this.Controls.Add(this.LabelSearch);
            this.Controls.Add(this.ButtonDelete);
            this.Controls.Add(this.ButtonAdd);
            this.Controls.Add(this.ComboBoxSort);
            this.Controls.Add(this.ComboBoxFilterBySupplier);
            this.Controls.Add(this.TextBoxSearch);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.ButtonLogout);
            this.Controls.Add(this.LabelFullName);
            this.Controls.Add(this.FlowLayoutPanel);
            this.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "OOO \"Обувь\"";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LoginForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel FlowLayoutPanel;
        private System.Windows.Forms.Label LabelFullName;
        private System.Windows.Forms.Button ButtonLogout;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox TextBoxSearch;
        private System.Windows.Forms.ComboBox ComboBoxFilterBySupplier;
        private System.Windows.Forms.ComboBox ComboBoxSort;
        private System.Windows.Forms.Button ButtonAdd;
        private System.Windows.Forms.Button ButtonDelete;
        private System.Windows.Forms.Label LabelSearch;
        private System.Windows.Forms.Label LabelFilterBySupplier;
        private System.Windows.Forms.Label LabelSort;
    }
}

