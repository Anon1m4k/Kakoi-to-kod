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
    public partial class ProductEditForm : Form
    {
        private bool _isEditMode = false;
        private string _originalArticle;
        private string _oldPhoto;
        public ProductEditForm()
        {
            InitializeComponent();
            this.Text = "Добавление товара";
            LoadComboBoxes();
        }
        public ProductEditForm(string article) : this()
        {
            _isEditMode = true;
            _originalArticle = article;
            this.Text = "Редактирование товара";
            LoadProductData(article);
            txtArticle.ReadOnly = true; // артикул не редактируется при изменении
        }
        private void LoadComboBoxes()
        {
            // Загрузка поставщиков
            cmbSupplier.Items.Clear();
            foreach (var supplier in DBOperations.GetSuppliers())
                cmbSupplier.Items.Add(supplier);

            // Загрузка производителей
            cmbManufacturer.Items.Clear();
            foreach (var manufacturer in DBOperations.GetManufacturers())
                cmbManufacturer.Items.Add(manufacturer);

            // Загрузка категорий
            cmbCategory.Items.Clear();
            foreach (var category in DBOperations.GetCategories())
                cmbCategory.Items.Add(category);
        }

        private void LoadProductData(string article)
        {
            var product = DBOperations.GetProductByArticle(article);
            if (product == null) return;

            txtArticle.Text = product.Article;
            txtName.Text = product.Name;
            txtUnit.Text = product.Unit;
            nudPrice.Value = product.Price;
            cmbSupplier.Text = product.Supplier;
            cmbManufacturer.Text = product.Manufacturer;
            cmbCategory.Text = product.Category;
            nudDiscount.Value = product.Discount;
            nudQuantity.Value = product.Quantity;
            txtDescription.Text = product.Description;
            _oldPhoto = product.Photo;

            string photoPath = Path.Combine(Application.StartupPath, product.Photo);
            if (!string.IsNullOrEmpty(product.Photo) && File.Exists(photoPath))
            {
                using (var fs = new FileStream(photoPath, FileMode.Open, FileAccess.Read))
                using (var img = Image.FromStream(fs))
                {
                    picPhoto.Image = new Bitmap(img);
                }
            }
            else
            {
                string defaultPath = Path.Combine(Application.StartupPath, "picture.png");
                if (File.Exists(defaultPath))
                {
                    using (var fs = new FileStream(defaultPath, FileMode.Open, FileAccess.Read))
                    using (var img = Image.FromStream(fs))
                    {
                        picPhoto.Image = new Bitmap(img);
                    }
                }
                else picPhoto.Image = null;
            }
            picPhoto.Tag = null; // сбрасываем метку "new"
        }

        private void btnSelectPhoto_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Загружаем без блокировки файла
                using (var fs = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                using (var img = Image.FromStream(fs))
                {
                    picPhoto.Image = new Bitmap(img);
                }
                picPhoto.Tag = "new";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Валидация
            if (string.IsNullOrWhiteSpace(txtArticle.Text) ||
                string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtUnit.Text) ||
                nudPrice.Value <= 0)
            {
                MessageBox.Show("Заполните все обязательные поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Проверка уникальности артикула при добавлении
            if (!_isEditMode)
            {
                var existing = DBOperations.GetProductByArticle(txtArticle.Text.Trim());
                if (existing != null)
                {
                    MessageBox.Show("Товар с таким артикулом уже существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // Сохранение изображения
            string photoFileName;
            if (picPhoto.Image != null && picPhoto.Tag != null && picPhoto.Tag.ToString() == "new")
            {
                // Если выбрано новое изображение, сохраняем его
                photoFileName = SaveImageToFile(txtArticle.Text.Trim());
            }
            else if (_isEditMode && !string.IsNullOrEmpty(_oldPhoto))
            {
                photoFileName = _oldPhoto;
            }
            else
            {
                photoFileName = "picture.png";
            }

            // Создание объекта товара
            var product = new Products
            {
                Article = txtArticle.Text.Trim(),
                Name = txtName.Text.Trim(),
                Unit = txtUnit.Text.Trim(),
                Price = nudPrice.Value,
                Supplier = cmbSupplier.Text.Trim(),
                Manufacturer = cmbManufacturer.Text.Trim(),
                Category = cmbCategory.Text.Trim(),
                Discount = (int)nudDiscount.Value,
                Quantity = (int)nudQuantity.Value,
                Description = txtDescription.Text.Trim(),
                Photo = photoFileName
            };

            try
            {
                if (_isEditMode)
                {
                    DBOperations.UpdateProduct(product);
                }
                else
                {
                    DBOperations.AddProduct(product);
                }
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private string SaveImageToFile(string article)
        {
            string imageFolder = Path.Combine(Application.StartupPath);


            string extension = Path.GetExtension(openFileDialog.FileName);
            string fileName = $"{article}{extension}";
            string filePath = Path.Combine(imageFolder, fileName);

            // Удаление старого фото, если оно существует и не заглушка
            if (_isEditMode && !string.IsNullOrEmpty(_oldPhoto) && _oldPhoto != "picture.png")
            {
                string oldPath = Path.Combine(Application.StartupPath, _oldPhoto);
                if (File.Exists(oldPath))
                    File.Delete(oldPath);
            }

            using (Image img = picPhoto.Image)
            {
                int newWidth = 500;
                int newHeight = 500;
                using (Bitmap resized = new Bitmap(img, new Size(newWidth, newHeight)))
                {
                    resized.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
            }

            return fileName;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}