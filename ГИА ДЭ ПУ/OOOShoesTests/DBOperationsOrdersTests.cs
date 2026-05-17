using Microsoft.VisualStudio.TestTools.UnitTesting;
using OOOShoesLib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OOOShoes.Tests
{
    [TestClass]
    public class DBOperationsOrdersTests
    {
        // Вспомогательные методы для создания уникальных идентификаторов
        private string GenerateUniqueArticle() => "TEST" + DateTime.Now.Ticks.ToString().Substring(10);
        private int _tempOrderId = -1;

        [TestMethod]
        public void GetClients_ReturnsNonEmptyList()
        {
            // Act
            var clients = DBOperations.GetClients();

            // Assert
            Assert.IsNotNull(clients, "Список клиентов не должен быть null.");
            Assert.IsTrue(clients.Count > 0, "В базе должны быть клиенты (роль 'Авторизированный клиент').");
        }

        [TestMethod]
        public void GetPickupPoints_ReturnsNonEmptyList()
        {
            // Act
            var points = DBOperations.GetPickupPoints();

            // Assert
            Assert.IsNotNull(points, "Список пунктов выдачи не должен быть null.");
            Assert.IsTrue(points.Count > 0, "В базе должны быть пункты выдачи.");
        }

        [TestMethod]
        public void GetAllOrders_ReturnsNonEmptyList()
        {
            // Act
            var orders = DBOperations.GetAllOrders();

            // Assert
            Assert.IsNotNull(orders, "Список заказов не должен быть null.");
            Assert.IsTrue(orders.Count > 0, "В базе должны быть заказы (по дампу есть как минимум 10).");
        }

        [TestMethod]
        public void GetOrderById_ExistingOrder_ReturnsOrder()
        {
            // Arrange – берём первый заказ из списка
            var orders = DBOperations.GetAllOrders();
            Assert.IsTrue(orders.Count > 0, "Нет заказов для теста.");
            int existingOrderId = orders.First().OrderId;

            // Act
            var order = DBOperations.GetOrderById(existingOrderId);

            // Assert
            Assert.IsNotNull(order, "Заказ с существующим ID должен быть найден.");
            Assert.AreEqual(existingOrderId, order.OrderId);
            Assert.IsNotNull(order.ClientName, "Имя клиента не должно быть пустым.");
            Assert.IsNotNull(order.PickupAddress, "Адрес пункта выдачи не должен быть пустым.");
        }

        [TestMethod]
        public void GetOrderById_NonExistingOrder_ReturnsNull()
        {
            // Arrange
            int nonExistingId = -999;

            // Act
            var order = DBOperations.GetOrderById(nonExistingId);

            // Assert
            Assert.IsNull(order, "Для несуществующего ID должен возвращаться null.");
        }

        [TestMethod]
        public void GetOrderItems_ExistingOrder_ReturnsItems()
        {
            // Arrange – берём заказ, который заведомо имеет позиции (например, order_id = 1 из дампа)
            int orderIdWithItems = 1; // в дампе order_id=1 имеет две позиции

            // Act
            var items = DBOperations.GetOrderItems(orderIdWithItems);

            // Assert
            Assert.IsNotNull(items, "Список позиций не должен быть null.");
            Assert.IsTrue(items.Count > 0, "Заказ должен содержать позиции.");
            foreach (var item in items)
            {
                Assert.IsFalse(string.IsNullOrEmpty(item.Article), "Артикул не должен быть пустым.");
                Assert.IsTrue(item.Quantity > 0, "Количество должно быть положительным.");
            }
        }

        [TestMethod]
        public void AddOrder_NewOrder_AddsSuccessfully()
        {
            // Arrange – создаём тестовый заказ
            var clients = DBOperations.GetClients();
            Assert.IsTrue(clients.Count > 0, "Нет клиентов для теста.");
            int clientId = clients.First().Id;

            var points = DBOperations.GetPickupPoints();
            Assert.IsTrue(points.Count > 0, "Нет пунктов выдачи для теста.");
            int pickupPointId = points.First().Id;

            // Для списка товаров используем существующий артикул из дампа
            string testArticle = "А112Т4"; // существующий товар
            var items = new List<OrderProducts>
            {
                new OrderProducts { Article = testArticle, Quantity = 2 }
            };

            var order = new Orders
            {
                ClientId = clientId,
                OrderDate = DateTime.Today,
                DeliveryDate = DateTime.Today.AddDays(5),
                PickupPointId = pickupPointId,
                Status = "Новый",
                PickupCode = new Random().Next(100, 999)
            };

            // Act
            DBOperations.AddOrder(order, items);

            // Assert – проверяем, что заказ появился в БД
            var allOrders = DBOperations.GetAllOrders();
            var addedOrder = allOrders.FirstOrDefault(o => o.ClientId == clientId &&
                                                           o.OrderDate == order.OrderDate &&
                                                           o.PickupPointId == pickupPointId);
            Assert.IsNotNull(addedOrder, "Добавленный заказ не найден.");
            _tempOrderId = addedOrder.OrderId; // сохраняем для возможной очистки

            // Проверяем позиции
            var addedItems = DBOperations.GetOrderItems(_tempOrderId);
            Assert.AreEqual(1, addedItems.Count);
            Assert.AreEqual(testArticle, addedItems[0].Article);
            Assert.AreEqual(2, addedItems[0].Quantity);

            // Cleanup – удаляем созданный заказ (будет вызван в конце теста)
        }

        [TestMethod]
        public void UpdateOrder_ExistingOrder_UpdatesCorrectly()
        {
            // Сначала добавим тестовый заказ, чтобы потом обновить его
            var clients = DBOperations.GetClients();
            int clientId = clients.First().Id;
            var points = DBOperations.GetPickupPoints();
            int pickupPointId = points.First().Id;
            string testArticle = "А112Т4";

            var items = new List<OrderProducts>
            {
                new OrderProducts { Article = testArticle, Quantity = 2 }
            };

            var order = new Orders
            {
                ClientId = clientId,
                OrderDate = DateTime.Today,
                DeliveryDate = DateTime.Today.AddDays(5),
                PickupPointId = pickupPointId,
                Status = "Новый",
                PickupCode = 111
            };
            DBOperations.AddOrder(order, items);
            var allOrders = DBOperations.GetAllOrders();
            var addedOrder = allOrders.FirstOrDefault(o => o.ClientId == clientId &&
                                                           o.OrderDate == order.OrderDate &&
                                                           o.PickupPointId == pickupPointId);
            Assert.IsNotNull(addedOrder);
            int orderId = addedOrder.OrderId;

            // Изменяем данные заказа
            order.OrderId = orderId;
            order.Status = "Завершен";
            order.DeliveryDate = DateTime.Today.AddDays(10);
            order.PickupCode = 222;

            // Изменяем состав – добавляем второй товар
            string secondArticle = "B320R5";
            var updatedItems = new List<OrderProducts>
            {
                new OrderProducts { Article = testArticle, Quantity = 3 },
                new OrderProducts { Article = secondArticle, Quantity = 1 }
            };

            // Act
            DBOperations.UpdateOrder(order, updatedItems);

            // Assert
            var updatedOrder = DBOperations.GetOrderById(orderId);
            Assert.IsNotNull(updatedOrder);
            Assert.AreEqual("Завершен", updatedOrder.Status);
            Assert.AreEqual(DateTime.Today.AddDays(10), updatedOrder.DeliveryDate);
            Assert.AreEqual(222, updatedOrder.PickupCode);

            var updatedItemsList = DBOperations.GetOrderItems(orderId);
            Assert.AreEqual(2, updatedItemsList.Count);
            Assert.IsTrue(updatedItemsList.Any(i => i.Article == testArticle && i.Quantity == 3));
            Assert.IsTrue(updatedItemsList.Any(i => i.Article == secondArticle && i.Quantity == 1));

            // Cleanup – удаляем заказ
            DBOperations.DeleteOrder(orderId);
        }

        [TestMethod]
        public void DeleteOrder_ExistingOrder_RemovesOrder()
        {
            // Создаём заказ
            var clients = DBOperations.GetClients();
            int clientId = clients.First().Id;
            var points = DBOperations.GetPickupPoints();
            int pickupPointId = points.First().Id;
            string testArticle = "А112Т4";

            var items = new List<OrderProducts>
            {
                new OrderProducts { Article = testArticle, Quantity = 1 }
            };

            var order = new Orders
            {
                ClientId = clientId,
                OrderDate = DateTime.Today,
                DeliveryDate = DateTime.Today.AddDays(5),
                PickupPointId = pickupPointId,
                Status = "Новый",
                PickupCode = 333
            };
            DBOperations.AddOrder(order, items);
            var allOrders = DBOperations.GetAllOrders();
            var addedOrder = allOrders.FirstOrDefault(o => o.ClientId == clientId &&
                                                           o.OrderDate == order.OrderDate &&
                                                           o.PickupPointId == pickupPointId);
            Assert.IsNotNull(addedOrder);
            int orderId = addedOrder.OrderId;

            // Act
            DBOperations.DeleteOrder(orderId);

            // Assert
            var deletedOrder = DBOperations.GetOrderById(orderId);
            Assert.IsNull(deletedOrder, "Заказ должен быть удалён.");

            // Проверяем, что позиции тоже удалены (каскадно)
            var itemsAfterDelete = DBOperations.GetOrderItems(orderId);
            Assert.AreEqual(0, itemsAfterDelete.Count, "Позиции заказа должны быть удалены.");
        }

        [TestCleanup]
        public void Cleanup()
        {
            // Если в процессе теста был создан заказ и его ID сохранён в _tempOrderId, удаляем его
            if (_tempOrderId != -1)
            {
                try
                {
                    DBOperations.DeleteOrder(_tempOrderId);
                }
                catch { /* игнорируем ошибки очистки */ }
                _tempOrderId = -1;
            }
        }
    }
}