using Microsoft.VisualStudio.TestTools.UnitTesting;
using OOOShoesLib;
using System.Linq;

namespace OOOShoes.Tests
{
    [TestClass]
    public class DBOperationsTests
    {
        [TestMethod]
        public void GetUser_ValidCredentials_ReturnsUser()
        {
            // Arrange
            string validLogin = "94d5ous@gmail.com";      // логин администратора из дампа
            string validPassword = "uzWC67";               // его пароль

            // Act
            Users user = DBOperations.GetUser(validLogin, validPassword);

            // Assert
            Assert.IsNotNull(user, "Пользователь с корректными данными не найден.");
            Assert.AreEqual(validLogin, user.Login);
            Assert.AreEqual("Никифорова Весения Николаевна", user.FullName);
            Assert.AreEqual("Администратор", user.Role);
        }

        [TestMethod]
        public void GetUser_InvalidPassword_ReturnsNull()
        {
            // Arrange
            string validLogin = "94d5ous@gmail.com";
            string invalidPassword = "wrong";

            // Act
            Users user = DBOperations.GetUser(validLogin, invalidPassword);

            // Assert
            Assert.IsNull(user, "При неверном пароле должен возвращаться null.");
        }

        [TestMethod]
        public void GetUser_NonexistentLogin_ReturnsNull()
        {
            // Arrange
            string nonexistentLogin = "nonexistent@mail.com";
            string anyPassword = "123";

            // Act
            Users user = DBOperations.GetUser(nonexistentLogin, anyPassword);

            // Assert
            Assert.IsNull(user, "При несуществующем логине должен возвращаться null.");
        }

        [TestMethod]
        public void GetAllProducts_ReturnsList()
        {
            // Act
            var products = DBOperations.GetAllProducts();

            // Assert
            Assert.IsNotNull(products, "Список товаров не должен быть null.");
            Assert.IsTrue(products.Count > 0, "В базе данных должны быть товары.");
        }

        [TestMethod]
        public void GetAllProducts_ProductsHaveValidData()
        {
            // Act
            var products = DBOperations.GetAllProducts();

            // Assert
            foreach (var product in products)
            {
                Assert.IsFalse(string.IsNullOrEmpty(product.Article), "Артикул не должен быть пустым.");
                Assert.IsTrue(product.Price > 0, "Цена должна быть положительной.");
                Assert.IsTrue(product.Quantity >= 0, "Количество не может быть отрицательным.");
            }
        }
    }
}