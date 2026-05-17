using Microsoft.VisualStudio.TestTools.UnitTesting;
using OOOShoesLib;
using System;
using System.Linq;
using System.Transactions;

namespace OOOShoes.Tests
{
    [TestClass]
    public class DBOperationsTests
    {
        private const string TestArticle = "TEST001";
        private const string TestProductName = "Тестовый товар";
        private const string TestSupplier = "Тестовый поставщик";
        private const string TestManufacturer = "Тестовый производитель";
        private const string TestCategory = "Тестовая категория";
        private const decimal TestPrice = 1000.00m;
        private const int TestQuantity = 10;
        private const int TestDiscount = 5;

        [TestMethod]
        public void GetSuppliers_ReturnsNonEmptyList()
        {
            // Arrange & Act
            var suppliers = DBOperations.GetSuppliers();

            // Assert
            Assert.IsNotNull(suppliers, "Список поставщиков не должен быть null.");
            Assert.IsTrue(suppliers.Count > 0, "В базе должны быть поставщики.");
        }

        [TestMethod]
        public void SearchProducts_WithExistingKeyword_ReturnsProducts()
        {
            // Arrange – используем существующий товар из дампа, например "Ботинки"
            string keyword = "Ботинки";

            // Act
            var results = DBOperations.SearchProducts(keyword);

            // Assert
            Assert.IsNotNull(results, "Результат поиска не должен быть null.");
            Assert.IsTrue(results.Count > 0, "Должен быть найден хотя бы один товар.");
            Assert.IsTrue(results.Any(p => p.Name.Contains(keyword) || p.Description.Contains(keyword)),
                "Результаты должны содержать товары с указанным ключевым словом.");
        }

        [TestMethod]
        public void GetProductByArticle_WithExistingArticle_ReturnsProduct()
        {
            // Arrange – существующий артикул из дампа
            string existingArticle = "А112Т4";

            // Act
            var product = DBOperations.GetProductByArticle(existingArticle);

            // Assert
            Assert.IsNotNull(product, "Товар с существующим артикулом должен быть найден.");
            Assert.AreEqual(existingArticle, product.Article);
        }

        [TestMethod]
        public void GetProductByArticle_WithNonExistingArticle_ReturnsNull()
        {
            // Arrange
            string nonExistingArticle = "NONEXISTENT123";

            // Act
            var product = DBOperations.GetProductByArticle(nonExistingArticle);

            // Assert
            Assert.IsNull(product, "Для несуществующего артикула должен возвращаться null.");
        }

        [TestMethod]
        public void AddProduct_NewProduct_AddsSuccessfully()
        {
            // Генерируем уникальный артикул, чтобы не конфликтовать с существующими
            string uniqueArticle = "TEST" + DateTime.Now.Ticks.ToString().Substring(10);
            var newProduct = new Products
            {
                Article = uniqueArticle,
                Name = TestProductName,
                Unit = "шт.",
                Price = TestPrice,
                Supplier = TestSupplier,
                Manufacturer = TestManufacturer,
                Category = TestCategory,
                Discount = TestDiscount,
                Quantity = TestQuantity,
                Description = "Тестовое описание",
                Photo = "picture.png"
            };

            // Act
            DBOperations.AddProduct(newProduct);

            // Assert – проверяем, что товар добавился
            var addedProduct = DBOperations.GetProductByArticle(uniqueArticle);
            Assert.IsNotNull(addedProduct, "Товар должен быть добавлен.");
            Assert.AreEqual(TestProductName, addedProduct.Name);
            Assert.AreEqual(TestPrice, addedProduct.Price);
            Assert.AreEqual(TestSupplier, addedProduct.Supplier);

            // Cleanup – удаляем добавленный товар
            DBOperations.DeleteProduct(uniqueArticle);
        }

        [TestMethod]
        public void AddProduct_DuplicateArticle_ThrowsException()
        {
            // Arrange – используем существующий артикул
            string existingArticle = "А112Т4";
            var duplicateProduct = new Products
            {
                Article = existingArticle,
                Name = "Дубликат",
                Unit = "шт.",
                Price = 500,
                Supplier = "Поставщик",
                Manufacturer = "Производитель",
                Category = "Категория",
                Discount = 0,
                Quantity = 1,
                Description = "",
                Photo = "picture.png"
            };

            // Act & Assert
            var ex = Assert.ThrowsException<Exception>(() => DBOperations.AddProduct(duplicateProduct));
            StringAssert.Contains(ex.Message, "уже существует", "Сообщение должно указывать на дубликат.");
        }

        [TestMethod]
        public void UpdateProduct_ExistingProduct_UpdatesCorrectly()
        {
            // Сначала добавим тестовый товар, который потом обновим
            string uniqueArticle = "UPD" + DateTime.Now.Ticks.ToString().Substring(10);
            var product = new Products
            {
                Article = uniqueArticle,
                Name = "Старое имя",
                Unit = "шт.",
                Price = 100,
                Supplier = "Старый поставщик",
                Manufacturer = "Старый производитель",
                Category = "Старая категория",
                Discount = 0,
                Quantity = 1,
                Description = "Старое описание",
                Photo = "picture.png"
            };
            DBOperations.AddProduct(product);

            // Изменяем данные
            product.Name = "Новое имя";
            product.Price = 200;
            product.Supplier = "Новый поставщик";
            product.Manufacturer = "Новый производитель";
            product.Category = "Новая категория";
            product.Discount = 10;
            product.Quantity = 5;
            product.Description = "Новое описание";

            // Act
            DBOperations.UpdateProduct(product);

            // Assert
            var updated = DBOperations.GetProductByArticle(uniqueArticle);
            Assert.IsNotNull(updated);
            Assert.AreEqual("Новое имя", updated.Name);
            Assert.AreEqual(200, updated.Price);
            Assert.AreEqual("Новый поставщик", updated.Supplier);
            Assert.AreEqual("Новый производитель", updated.Manufacturer);
            Assert.AreEqual("Новая категория", updated.Category);
            Assert.AreEqual(10, updated.Discount);
            Assert.AreEqual(5, updated.Quantity);
            Assert.AreEqual("Новое описание", updated.Description);

            // Cleanup
            DBOperations.DeleteProduct(uniqueArticle);
        }

        [TestMethod]
        public void DeleteProduct_ExistingProduct_RemovesProduct()
        {
            // Добавляем товар
            string uniqueArticle = "DEL" + DateTime.Now.Ticks.ToString().Substring(10);
            var product = new Products
            {
                Article = uniqueArticle,
                Name = "Для удаления",
                Unit = "шт.",
                Price = 10,
                Supplier = "Поставщик",
                Manufacturer = "Производитель",
                Category = "Категория",
                Discount = 0,
                Quantity = 1,
                Description = "",
                Photo = "picture.png"
            };
            DBOperations.AddProduct(product);

            // Act
            DBOperations.DeleteProduct(uniqueArticle);

            // Assert
            var deleted = DBOperations.GetProductByArticle(uniqueArticle);
            Assert.IsNull(deleted, "Товар должен быть удалён.");
        }

        [TestMethod]
        public void GetOrdersByProduct_WithProductInOrders_ReturnsPositiveCount()
        {
            // Используем товар, который точно есть в заказах (из дампа)
            string articleInOrders = "А112Т4";

            // Act
            int count = DBOperations.GetOrdersByProduct(articleInOrders);

            // Assert
            Assert.IsTrue(count > 0, "Товар должен присутствовать хотя бы в одном заказе.");
        }

        [TestMethod]
        public void GetOrdersByProduct_WithNewProduct_ReturnsZero()
        {
            // Добавляем новый товар и проверяем, что он не в заказах
            string uniqueArticle = "ORD" + DateTime.Now.Ticks.ToString().Substring(10);
            var product = new Products
            {
                Article = uniqueArticle,
                Name = "Без заказов",
                Unit = "шт.",
                Price = 10,
                Supplier = "Поставщик",
                Manufacturer = "Производитель",
                Category = "Категория",
                Discount = 0,
                Quantity = 1,
                Description = "",
                Photo = "picture.png"
            };
            DBOperations.AddProduct(product);

            // Act
            int count = DBOperations.GetOrdersByProduct(uniqueArticle);

            // Assert
            Assert.AreEqual(0, count, "Новый товар не должен присутствовать в заказах.");

            // Cleanup
            DBOperations.DeleteProduct(uniqueArticle);
        }
    }
}