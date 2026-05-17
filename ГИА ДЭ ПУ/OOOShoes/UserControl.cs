using OOOShoesLib;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace OOOShoes
{
    public partial class UserControl : System.Windows.Forms.UserControl
    {
        public string Article { get; private set; }
        public bool IsSelected { get; internal set; }
        private Products _product;

        public UserControl()
        {
            InitializeComponent();
            // Подписываемся на клик и двойной клик самого контрола и всех дочерних
            this.Click += UserControl_Click;
            this.DoubleClick += UserControl_DoubleClick;
            foreach (Control ctrl in Controls)
            {
                ctrl.Click += UserControl_Click;
                ctrl.DoubleClick += UserControl_DoubleClick;
            }
        }

        public void SetProduct(Products product)
        {
            _product = product;
            Article = product.Article;
            LabelName.Text = product.Name;
            LabelCategory.Text = product.Category;
            LabelDescription.Text = product.Description;
            LabelManufacturer.Text = $"Производитель: {product.Manufacturer}";
            LabelSupplier.Text = $"Поставщик: {product.Supplier}";
            LabelPrice.Text = $"Цена: {product.Price:C}";
            LabelUnit.Text = $"Ед. изм.: {product.Unit}";
            LabelQuantity.Text = $"Количество: {product.Quantity}";
            LabelDiscount.Text = $"Скидка: {product.Discount}%";

            ApplyConditionalFormatting(product);
            LoadImage(product.Photo);
        }

        private void ApplyConditionalFormatting(Products product)
        {
            if (product.Quantity == 0)
            {
                LabelQuantity.ForeColor = Color.Blue;
            }
            else
            {
                LabelQuantity.ForeColor = SystemColors.ControlText;
            }

            if (product.Discount > 15)
            {
                BackColor = ColorTranslator.FromHtml("#2E8B57");
            }
            else
            {
                BackColor = SystemColors.Control;
            }

            if (product.Discount > 0)
            {
                LabelPrice.Font = new Font(LabelPrice.Font, FontStyle.Strikeout);
                LabelPrice.ForeColor = Color.Red;
                FinalPriceLabel.Text = $"Итоговая цена: {product.Price * (100 - product.Discount) / 100:C}";
                FinalPriceLabel.ForeColor = Color.Black;
                FinalPriceLabel.AutoSize = true;
            }
            else
            {
                LabelPrice.Font = new Font(LabelPrice.Font, FontStyle.Regular);
                LabelPrice.ForeColor = SystemColors.ControlText;
                FinalPriceLabel.Text = "";
            }
        }

        private void LoadImage(string photoName)
        {
            string imageFolder = Application.StartupPath;
            string imagePath = Path.Combine(imageFolder, photoName);

            if (!string.IsNullOrEmpty(photoName) && File.Exists(imagePath))
            {
                PictureBoxPhoto.Image = Image.FromFile(imagePath);
            }
            else
            {
                string defaultImage = Path.Combine(imageFolder, "picture.png");
                if (File.Exists(defaultImage))
                    PictureBoxPhoto.Image = Image.FromFile(defaultImage);
                else
                    PictureBoxPhoto.Image = null;
            }
            PictureBoxPhoto.SizeMode = PictureBoxSizeMode.Zoom;
        }

        public string GetArticle() => Article;

        private void RefreshAppearance()
        {
            // Восстанавливаем внешний вид на основе данных товара
            BackColor = SystemColors.Control;
            if (_product.Quantity == 0)
                LabelQuantity.ForeColor = Color.Blue;
            else
                LabelQuantity.ForeColor = SystemColors.ControlText;

            if (_product.Discount > 15)
                BackColor = ColorTranslator.FromHtml("#2E8B57");

            if (_product.Discount > 0)
            {
                LabelPrice.Font = new Font(LabelPrice.Font, FontStyle.Strikeout);
                LabelPrice.ForeColor = Color.Red;
                FinalPriceLabel.Text = $"Итоговая цена: {_product.Price * (100 - _product.Discount) / 100:C}";
                FinalPriceLabel.ForeColor = Color.Black;
            }
            else
            {
                LabelPrice.Font = new Font(LabelPrice.Font, FontStyle.Regular);
                LabelPrice.ForeColor = SystemColors.ControlText;
                FinalPriceLabel.Text = "";
            }
        }

        public void SetSelected(bool selected)
        {
            if (selected)
            {
                BackColor = Color.LightSteelBlue;
            }
            else
            {
                RefreshAppearance();
            }
        }

        private void UserControl_Click(object sender, EventArgs e)
        {
            if (Parent is FlowLayoutPanel panel && panel.Parent is MainForm mainForm)
            {
                mainForm.SelectProduct(this);
            }
        }

        private void UserControl_DoubleClick(object sender, EventArgs e)
        {
            if (Parent is FlowLayoutPanel panel && panel.Parent is MainForm mainForm)
            {
                mainForm.EditProduct(this);
            }
        }
    }
}