using OOOShoesLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOOShoes
{
    public partial class UserControl : System.Windows.Forms.UserControl
    {
        public UserControl()
        {
            InitializeComponent();
        }
        private void LoadProducts()
        {
            var products = DBOperations.GetAllProducts();
            Controls.Clear();

            foreach (var prod in products)
            {
                var productControl = new UserControl();
                productControl.SetProduct(prod);
                Controls.Add(productControl);
            }
        }
        public void SetProduct(Products product)
        {
            // Заполнение текстовых полей
            LabelName.Text = product.Name;
            LabelCategory.Text = product.Category;
            LabelDescription.Text = product.Description;
            LabelManufacturer.Text = $"Производитель: {product.Manufacturer}";
            LabelSupplier.Text = $"Поставщик: {product.Supplier}";
            LabelPrice.Text = $"Цена: {product.Price:C}";
            LabelUnit.Text = $"Ед. изм.: {product.Unit}";
            LabelQuantity.Text = $"Количество: {product.Quantity}";
            LabelDiscount.Text = $"Скидка: {product.Discount}%";

            // Условное форматирование
           ApplyConditionalFormatting(product);

            // Загрузка изображения
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
                BackColor = SystemColors.Control;
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
        }
        private void LoadImage(string photoName)
        {
            // Путь к папке с изображениями
            string imageFolder = Application.StartupPath;

            if (!string.IsNullOrEmpty(photoName) && File.Exists(imageFolder + "\\" + photoName))
            {
                PictureBoxPhoto.Image = Image.FromFile(imageFolder + "\\" + photoName);
            }
            else
            {
                PictureBoxPhoto.Image = Image.FromFile(Application.StartupPath + @"\picture.png");
            }
            PictureBoxPhoto.SizeMode = PictureBoxSizeMode.Zoom;
        }
    }
}
