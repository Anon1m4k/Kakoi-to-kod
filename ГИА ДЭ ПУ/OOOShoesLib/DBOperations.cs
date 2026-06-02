using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace OOOShoesLib
{
    public class DBOperations
    {
        private const string ConnectionString = "server=127.0.0.1; uid=root; pwd=vertrigo; database=oooshoes;";
        public static Users GetUser(string login, string password)
        {
            try
            {
                using (var conn = new MySqlConnection(ConnectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM users WHERE `Логин` = @login AND `Пароль` = @password";
                    var cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@login", login);
                    cmd.Parameters.AddWithValue("@password", password);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Users
                            {
                                Id = reader.GetInt32("Id"),
                                FullName = reader.GetString("ФИО"),
                                Role = reader.GetString("Роль сотрудника"),
                                Login = reader.GetString("Логин"),
                                Password = reader.GetString("Пароль")
                            };
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"GetUser error: {ex.Message}");
                throw new Exception("Ошибка авторизации.", ex);
            }
        }
        public static List<Products> GetAllProducts()
        {
            var products = new List<Products>();
            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                string query = "SELECT * FROM products";
                var cmd = new MySqlCommand(query, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        products.Add(MapToProduct(reader));
                }
            }
            return products;
        }
        public static Products GetProductByArticle(string article)
        {
            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                string query = "SELECT * FROM products WHERE `Артикул` = @article";
                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@article", article);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                        return MapToProduct(reader);
                }
            }
            return null;
        }
        public static void AddProduct(Products product)
        {
            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                string query = @"INSERT INTO products 
                    (`Артикул`, `Наименование товара`, `Единица измерения`, `Цена`, `Поставщик`, `Производитель`, 
                     `Категория товара`, `Действующая скидка`, `Кол-во на складе`, `Описание товара`, `Фото`) 
                    VALUES (@article, @name, @unit, @price, @supplier, @manufacturer, @category, @discount, @quantity, @description, @photo)";
                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@article", product.Article);
                cmd.Parameters.AddWithValue("@name", product.Name);
                cmd.Parameters.AddWithValue("@unit", product.Unit);
                cmd.Parameters.AddWithValue("@price", product.Price);
                cmd.Parameters.AddWithValue("@supplier", product.Supplier);
                cmd.Parameters.AddWithValue("@manufacturer", product.Manufacturer);
                cmd.Parameters.AddWithValue("@category", product.Category);
                cmd.Parameters.AddWithValue("@discount", product.Discount);
                cmd.Parameters.AddWithValue("@quantity", product.Quantity);
                cmd.Parameters.AddWithValue("@description", product.Description);
                cmd.Parameters.AddWithValue("@photo", product.Photo);
                cmd.ExecuteNonQuery();
            }
        }
        public static void UpdateProduct(Products product)
        {
            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                string query = @"UPDATE products SET 
                    `Наименование товара` = @name,
                    `Единица измерения` = @unit,
                    `Цена` = @price,
                    `Поставщик` = @supplier,
                    `Производитель` = @manufacturer,
                    `Категория товара` = @category,
                    `Действующая скидка` = @discount,
                    `Кол-во на складе` = @quantity,
                    `Описание товара` = @description,
                    `Фото` = @photo
                    WHERE `Артикул` = @article";
                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@article", product.Article);
                cmd.Parameters.AddWithValue("@name", product.Name);
                cmd.Parameters.AddWithValue("@unit", product.Unit);
                cmd.Parameters.AddWithValue("@price", product.Price);
                cmd.Parameters.AddWithValue("@supplier", product.Supplier);
                cmd.Parameters.AddWithValue("@manufacturer", product.Manufacturer);
                cmd.Parameters.AddWithValue("@category", product.Category);
                cmd.Parameters.AddWithValue("@discount", product.Discount);
                cmd.Parameters.AddWithValue("@quantity", product.Quantity);
                cmd.Parameters.AddWithValue("@description", product.Description);
                cmd.Parameters.AddWithValue("@photo", product.Photo);
                cmd.ExecuteNonQuery();
            }
        }
        public static void DeleteProduct(string article)
        {
            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                string query = "DELETE FROM products WHERE `Артикул` = @article";
                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@article", article);
                cmd.ExecuteNonQuery();
            }
        }
        public static int GetOrdersByProduct(string article)
        {
            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM orders WHERE FIND_IN_SET(@article, REPLACE(`Артикул заказа`, ' ', '')) > 0";
                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@article", article);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        public static List<Orders> GetAllOrders()
        {
            var orders = new List<Orders>();
            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                string query = @"
                    SELECT o.*, p.`Адрес` AS pickup_address
                    FROM orders o
                    LEFT JOIN pickup_point p ON o.`Адрес пункта выдачи` = p.Id
                    ORDER BY o.`Дата заказа` DESC";
                var cmd = new MySqlCommand(query, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        orders.Add(MapToOrder(reader));
                }
            }
            return orders;
        }
        public static Orders GetOrderById(int orderId)
        {
            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                string query = @"
                    SELECT o.*, p.`Адрес` AS pickup_address
                    FROM orders o
                    LEFT JOIN pickup_point p ON o.`Адрес пункта выдачи` = p.Id
                    WHERE o.`Номер заказа` = @orderId";
                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@orderId", orderId);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                        return MapToOrder(reader);
                }
            }
            return null;
        }
        public static void AddOrder(Orders order)
        {
            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                string query = @"
                    INSERT INTO orders 
                    (`Номер заказа`, `Артикул заказа`, `Дата заказа`, `Дата доставки`, `Адрес пункта выдачи`, 
                     `Id_клиента`, `ФИО авторизированного клиента`, `Код для получения`, `Статус заказа`)
                    VALUES (@orderId, @articleString, @orderDate, @deliveryDate, @pickupPointId,
                            @clientId, @clientName, @pickupCode, @status)";
                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@orderId", order.OrderId);
                cmd.Parameters.AddWithValue("@articleString", order.ArticleString);
                cmd.Parameters.AddWithValue("@orderDate", order.OrderDate?.ToString("dd.MM.yyyy") ?? "");
                cmd.Parameters.AddWithValue("@deliveryDate", order.DeliveryDate?.ToString("dd.MM.yyyy") ?? "");
                cmd.Parameters.AddWithValue("@pickupPointId", order.PickupPointId);
                cmd.Parameters.AddWithValue("@clientId", order.ClientId);
                cmd.Parameters.AddWithValue("@clientName", order.ClientName ?? "");
                cmd.Parameters.AddWithValue("@pickupCode", order.PickupCode);
                cmd.Parameters.AddWithValue("@status", order.Status);
                cmd.ExecuteNonQuery();
            }
        }
        public static void UpdateOrder(Orders order)
        {
            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                string query = @"
                    UPDATE orders SET 
                        `Артикул заказа` = @articleString,
                        `Дата заказа` = @orderDate,
                        `Дата доставки` = @deliveryDate,
                        `Адрес пункта выдачи` = @pickupPointId,
                        `Id_клиента` = @clientId,
                        `ФИО авторизированного клиента` = @clientName,
                        `Код для получения` = @pickupCode,
                        `Статус заказа` = @status
                    WHERE `Номер заказа` = @orderId";
                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@orderId", order.OrderId);
                cmd.Parameters.AddWithValue("@articleString", order.ArticleString);
                cmd.Parameters.AddWithValue("@orderDate", order.OrderDate?.ToString("dd.MM.yyyy") ?? "");
                cmd.Parameters.AddWithValue("@deliveryDate", order.DeliveryDate?.ToString("dd.MM.yyyy") ?? "");
                cmd.Parameters.AddWithValue("@pickupPointId", order.PickupPointId);
                cmd.Parameters.AddWithValue("@clientId", order.ClientId);
                cmd.Parameters.AddWithValue("@clientName", order.ClientName ?? "");
                cmd.Parameters.AddWithValue("@pickupCode", order.PickupCode);
                cmd.Parameters.AddWithValue("@status", order.Status);
                cmd.ExecuteNonQuery();
            }
        }
        public static void DeleteOrder(int orderId)
        {
            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                string query = "DELETE FROM orders WHERE `Номер заказа` = @orderId";
                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@orderId", orderId);
                cmd.ExecuteNonQuery();
            }
        }
        public static List<string> GetSuppliers()
        {
            var list = new List<string>();
            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                string query = "SELECT DISTINCT `Поставщик` FROM products ORDER BY `Поставщик`";
                var cmd = new MySqlCommand(query, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        list.Add(reader.GetString(0));
                }
            }
            return list;
        }
        public static List<string> GetManufacturers()
        {
            var list = new List<string>();
            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                string query = "SELECT DISTINCT `Производитель` FROM products ORDER BY `Производитель`";
                var cmd = new MySqlCommand(query, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        list.Add(reader.GetString(0));
                }
            }
            return list;
        }
        public static List<string> GetCategories()
        {
            var list = new List<string>();
            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                string query = "SELECT DISTINCT `Категория товара` FROM products ORDER BY `Категория товара`";
                var cmd = new MySqlCommand(query, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        list.Add(reader.GetString(0));
                }
            }
            return list;
        }
        public static List<PickupPoints> GetPickupPoints()
        {
            var points = new List<PickupPoints>();
            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                string query = "SELECT * FROM pickup_point ORDER BY `Адрес`";
                var cmd = new MySqlCommand(query, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        points.Add(new PickupPoints
                        {
                            Id = reader.GetInt32("Id"),
                            Address = reader.GetString("Адрес")
                        });
                    }
                }
            }
            return points;
        }
        public static List<Users> GetClients()
        {
            var clients = new List<Users>();
            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                string query = "SELECT `Id`, `ФИО` FROM users WHERE `Роль сотрудника` = 'Авторизированный клиент' ORDER BY `ФИО`";
                var cmd = new MySqlCommand(query, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        clients.Add(new Users
                        {
                            Id = reader.GetInt32("Id"),
                            FullName = reader.GetString("ФИО")
                        });
                    }
                }
            }
            return clients;
        }
        public static List<Products> SearchProducts(string searchText)
        {
            var products = new List<Products>();
            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                string query = @"
                    SELECT * FROM products 
                    WHERE `Наименование товара` LIKE @search 
                       OR `Описание товара` LIKE @search 
                       OR `Производитель` LIKE @search 
                       OR `Категория товара` LIKE @search";
                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@search", "%" + searchText + "%");
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        products.Add(MapToProduct(reader));
                }
            }
            return products;
        }
        private static Products MapToProduct(MySqlDataReader reader)
        {
            return new Products
            {
                Article = reader.GetString("Артикул"),
                Name = reader.GetString("Наименование товара"),
                Unit = reader.GetString("Единица измерения"),
                Price = reader.GetDecimal("Цена"),
                Supplier = reader.GetString("Поставщик"),
                Manufacturer = reader.GetString("Производитель"),
                Category = reader.GetString("Категория товара"),
                Discount = reader.GetInt32("Действующая скидка"),
                Quantity = reader.GetInt32("Кол-во на складе"),
                Description = reader.GetString("Описание товара"),
                Photo = reader.GetString("Фото")
            };
        }
        private static Orders MapToOrder(MySqlDataReader reader)
        {
            return new Orders
            {
                OrderId = reader.GetInt32("Номер заказа"),
                ArticleString = reader.GetString("Артикул заказа"),
                OrderDate = ParseDate(reader, "Дата заказа"),
                DeliveryDate = ParseDate(reader, "Дата доставки"),
                PickupPointId = reader.GetInt32("Адрес пункта выдачи"),
                ClientId = reader.GetInt32("Id_клиента"),
                ClientName = reader.GetString("ФИО авторизированного клиента"),
                PickupCode = reader.GetInt32("Код для получения"),
                Status = reader.GetString("Статус заказа"),
                PickupAddress = reader.IsDBNull(reader.GetOrdinal("pickup_address")) ? "" : reader.GetString("pickup_address")
            };
        }
        private static DateTime? ParseDate(MySqlDataReader reader, string columnName)
        {
            string dateStr = reader.GetString(columnName);
            if (DateTime.TryParseExact(dateStr, "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime dt))
                return dt;
            return null;
        }
    }
}