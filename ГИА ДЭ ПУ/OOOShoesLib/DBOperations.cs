using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace OOOShoesLib
{
    public class DBOperations
    {
        private const string ConnectionString = "server=127.0.0.1; uid=root; pwd=vertrigo; database=oooshoes;";

        // Получение пользователя по логину и паролю
        public static Users GetUser(string login, string password)
        {
            try
            {
                using (var conn = new MySqlConnection(ConnectionString))
                {
                    conn.Open();
                    string query = "SELECT id, full_name, role, login, password FROM users WHERE login = @login AND password = @password";
                    var cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@login", login);
                    cmd.Parameters.AddWithValue("@password", password);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Users
                            {
                                Id = reader.GetInt32("id"),
                                FullName = reader.GetString("full_name"),
                                Role = reader.GetString("role"),
                                Login = reader.GetString("login"),
                                Password = reader.GetString("password")
                            };
                        }
                    }
                }
                return null;
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine($"MySQL error in GetUser: {ex.Message}");
                throw new Exception("Ошибка при обращении к базе данных. Проверьте подключение.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"General error in GetUser: {ex.Message}");
                throw new Exception("Произошла ошибка при авторизации.", ex);
            }
        }
        // Получение списка всех товаров
        public static List<Products> GetAllProducts()
        {
            try
            {
                var products = new List<Products>();
                using (var conn = new MySqlConnection(ConnectionString))
                {
                    conn.Open();
                    string query = "SELECT article, name, unit, price, supplier, manufacturer, category, discount, quantity, description, photo FROM products";
                    var cmd = new MySqlCommand(query, conn);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            products.Add(new Products
                            {
                                Article = reader.GetString("article"),
                                Name = reader.GetString("name"),
                                Unit = reader.GetString("unit"),
                                Price = reader.GetDecimal("price"),
                                Supplier = reader.GetString("supplier"),
                                Manufacturer = reader.GetString("manufacturer"),
                                Category = reader.GetString("category"),
                                Discount = reader.GetInt32("discount"),
                                Quantity = reader.GetInt32("quantity"),
                                Description = reader.GetString("description"),
                                Photo = reader.GetString("photo")
                            });
                        }
                    }
                }
                return products;
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine($"MySQL error in GetAllProducts: {ex.Message}");
                throw new Exception("Ошибка при загрузке списка товаров. Проверьте подключение к базе данных.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"General error in GetAllProducts: {ex.Message}");
                throw new Exception("Произошла ошибка при загрузке товаров.", ex);
            }
        }
        public static Products GetProductByArticle(string article)
        {
            try
            {
                using (var conn = new MySqlConnection(ConnectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM products WHERE article = @article";
                    var cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@article", article);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Products
                            {
                                Article = reader.GetString("article"),
                                Name = reader.GetString("name"),
                                Unit = reader.GetString("unit"),
                                Price = reader.GetDecimal("price"),
                                Supplier = reader.GetString("supplier"),
                                Manufacturer = reader.GetString("manufacturer"),
                                Category = reader.GetString("category"),
                                Discount = reader.GetInt32("discount"),
                                Quantity = reader.GetInt32("quantity"),
                                Description = reader.GetString("description"),
                                Photo = reader.GetString("photo")
                            };
                        }
                    }
                }
                return null;
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine($"MySQL error in GetProductByArticle: {ex.Message}");
                throw new Exception("Ошибка при получении данных товара. Проверьте подключение к базе данных.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"General error in GetProductByArticle: {ex.Message}");
                throw new Exception("Произошла ошибка при получении информации о товаре.", ex);
            }
        }
        public static void AddProduct(Products product)
        {
            try
            {
                using (var conn = new MySqlConnection(ConnectionString))
                {
                    conn.Open();
                    string query = @"INSERT INTO products 
                        (article, name, unit, price, supplier, manufacturer, category, discount, quantity, description, photo) 
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
            catch (MySqlException ex)
            {
                Debug.WriteLine($"MySQL error in AddProduct: {ex.Message}");
                if (ex.Number == 1062)
                    throw new Exception("Товар с таким артикулом уже существует.", ex);
                else
                    throw new Exception("Ошибка при добавлении товара. Проверьте правильность введённых данных.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"General error in AddProduct: {ex.Message}");
                throw new Exception("Произошла ошибка при добавлении товара.", ex);
            }
        }

        public static void UpdateProduct(Products product)
        {
            try
            {
                using (var conn = new MySqlConnection(ConnectionString))
                {
                    conn.Open();
                    string query = @"UPDATE products SET 
                        name = @name, unit = @unit, price = @price, supplier = @supplier, 
                        manufacturer = @manufacturer, category = @category, discount = @discount, 
                        quantity = @quantity, description = @description, photo = @photo 
                        WHERE article = @article";
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
            catch (MySqlException ex)
            {
                Debug.WriteLine($"MySQL error in UpdateProduct: {ex.Message}");
                throw new Exception("Ошибка при обновлении товара. Проверьте правильность введённых данных.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"General error in UpdateProduct: {ex.Message}");
                throw new Exception("Произошла ошибка при обновлении товара.", ex);
            }
        }
        public static void DeleteProduct(string article)
        {
            try
            {
                using (var conn = new MySqlConnection(ConnectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM products WHERE article = @article";
                    var cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@article", article);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine($"MySQL error in DeleteProduct: {ex.Message}");
                throw new Exception("Ошибка при удалении товара. Возможно, товар связан с другими записями.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"General error in DeleteProduct: {ex.Message}");
                throw new Exception("Произошла ошибка при удалении товара.", ex);
            }
        }
        public static int GetOrdersByProduct(string article)
        {
            try
            {
                using (var conn = new MySqlConnection(ConnectionString))
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM order_products WHERE article = @article";
                    var cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@article", article);
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine($"MySQL error in GetOrdersByProduct: {ex.Message}");
                throw new Exception("Ошибка при проверке наличия товара в заказах.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"General error in GetOrdersByProduct: {ex.Message}");
                throw new Exception("Произошла ошибка при проверке заказов.", ex);
            }
        }
        public static List<string> GetSuppliers()
        {
            try
            {
                var suppliers = new List<string>();
                using (var conn = new MySqlConnection(ConnectionString))
                {
                    conn.Open();
                    string query = "SELECT DISTINCT supplier FROM products ORDER BY supplier";
                    var cmd = new MySqlCommand(query, conn);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            suppliers.Add(reader.GetString("supplier"));
                        }
                    }
                }
                return suppliers;
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine($"MySQL error in GetSuppliers: {ex.Message}");
                throw new Exception("Ошибка при загрузке списка поставщиков.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"General error in GetSuppliers: {ex.Message}");
                throw new Exception("Произошла ошибка при получении списка поставщиков.", ex);
            }
        }
        public static List<Products> SearchProducts(string searchText)
        {
            try
            {
                var products = new List<Products>();
                using (var conn = new MySqlConnection(ConnectionString))
                {
                    conn.Open();
                    string query = @"SELECT article, name, unit, price, supplier, manufacturer, 
                                    category, discount, quantity, description, photo 
                             FROM products 
                             WHERE name LIKE @search 
                                OR description LIKE @search 
                                OR manufacturer LIKE @search 
                                OR category LIKE @search";
                    var cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@search", "%" + searchText + "%");
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            products.Add(new Products
                            {
                                Article = reader.GetString("article"),
                                Name = reader.GetString("name"),
                                Unit = reader.GetString("unit"),
                                Price = reader.GetDecimal("price"),
                                Supplier = reader.GetString("supplier"),
                                Manufacturer = reader.GetString("manufacturer"),
                                Category = reader.GetString("category"),
                                Discount = reader.GetInt32("discount"),
                                Quantity = reader.GetInt32("quantity"),
                                Description = reader.GetString("description"),
                                Photo = reader.GetString("photo")
                            });
                        }
                    }
                }
                return products;
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine($"MySQL error in SearchProducts: {ex.Message}");
                throw new Exception("Ошибка при выполнении поиска.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"General error in SearchProducts: {ex.Message}");
                throw new Exception("Произошла ошибка при поиске товаров.", ex);
            }
        }

        // Получение всех заказов с информацией о клиенте и пункте выдачи
        public static List<Orders> GetAllOrders()
        {
            var orders = new List<Orders>();
            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                string query = @"
            SELECT o.order_id, o.order_date, o.delivery_date, o.pickup_point_id, 
                   p.address AS pickup_address, o.client_id, u.full_name AS client_name,
                   o.pickup_code, o.status
            FROM orders o
            LEFT JOIN pickup_point p ON o.pickup_point_id = p.id
            LEFT JOIN users u ON o.client_id = u.id
            ORDER BY o.order_date DESC";
                var cmd = new MySqlCommand(query, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        orders.Add(new Orders
                        {
                            OrderId = reader.GetInt32("order_id"),
                            OrderDate = reader.IsDBNull(reader.GetOrdinal("order_date"))
                                            ? (DateTime?)null
                                            : reader.GetDateTime("order_date"),
                            DeliveryDate = reader.IsDBNull(reader.GetOrdinal("delivery_date"))
                                            ? (DateTime?)null
                                            : reader.GetDateTime("delivery_date"),
                            PickupPointId = reader.GetInt32("pickup_point_id"),
                            PickupAddress = reader["pickup_address"] as string ?? "",
                            ClientId = reader.GetInt32("client_id"),
                            ClientName = reader["client_name"] as string ?? "",
                            PickupCode = reader.GetInt32("pickup_code"),
                            Status = reader.GetString("status")
                        });
                    }
                }
            }
            return orders;
        }

        // Получение одного заказа по ID
        public static Orders GetOrderById(int orderId)
        {
            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                string query = @"
            SELECT o.order_id, o.order_date, o.delivery_date, o.pickup_point_id, 
                   p.address AS pickup_address, o.client_id, u.full_name AS client_name,
                   o.pickup_code, o.status
            FROM orders o
            LEFT JOIN pickup_point p ON o.pickup_point_id = p.id
            LEFT JOIN users u ON o.client_id = u.id
            WHERE o.order_id = @orderId";
                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@orderId", orderId);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Orders
                        {
                            OrderId = reader.GetInt32("order_id"),
                            OrderDate = reader.IsDBNull(reader.GetOrdinal("order_date"))
                                            ? (DateTime?)null
                                            : reader.GetDateTime("order_date"),
                            DeliveryDate = reader.IsDBNull(reader.GetOrdinal("delivery_date"))
                                            ? (DateTime?)null
                                            : reader.GetDateTime("delivery_date"),
                            PickupPointId = reader.GetInt32("pickup_point_id"),
                            PickupAddress = reader["pickup_address"] as string ?? "",
                            ClientId = reader.GetInt32("client_id"),
                            ClientName = reader["client_name"] as string ?? "",
                            PickupCode = reader.GetInt32("pickup_code"),
                            Status = reader.GetString("status")
                        };
                    }
                }
            }
            return null;
        }

        // Получение списка позиций заказа
        public static List<OrderProducts> GetOrderItems(int orderId)
        {
            var items = new List<OrderProducts>();
            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                string query = "SELECT id, order_id, article, quantity FROM order_products WHERE order_id = @orderId";
                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@orderId", orderId);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        items.Add(new OrderProducts
                        {
                            Id = reader.GetInt32("id"),
                            OrderId = reader.GetInt32("order_id"),
                            Article = reader.GetString("article"),
                            Quantity = reader.GetInt32("quantity")
                        });
                    }
                }
            }
            return items;
        }

        // Добавление нового заказа (с транзакцией)
        public static void AddOrder(Orders order, List<OrderProducts> items)
        {
            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        string insertOrder = @"
                    INSERT INTO orders (order_date, delivery_date, pickup_point_id, client_id, pickup_code, status)
                    VALUES (@orderDate, @deliveryDate, @pickupPointId, @clientId, @pickupCode, @status);
                    SELECT LAST_INSERT_ID();";
                        var cmd = new MySqlCommand(insertOrder, conn, tran);
                        cmd.Parameters.AddWithValue("@orderDate", order.OrderDate);
                        cmd.Parameters.AddWithValue("@deliveryDate", order.DeliveryDate);
                        cmd.Parameters.AddWithValue("@pickupPointId", order.PickupPointId);
                        cmd.Parameters.AddWithValue("@clientId", order.ClientId);
                        cmd.Parameters.AddWithValue("@pickupCode", order.PickupCode);
                        cmd.Parameters.AddWithValue("@status", order.Status);
                        int newOrderId = Convert.ToInt32(cmd.ExecuteScalar());

                        foreach (var item in items)
                        {
                            string insertItem = "INSERT INTO order_products (order_id, article, quantity) VALUES (@orderId, @article, @quantity)";
                            var cmdItem = new MySqlCommand(insertItem, conn, tran);
                            cmdItem.Parameters.AddWithValue("@orderId", newOrderId);
                            cmdItem.Parameters.AddWithValue("@article", item.Article);
                            cmdItem.Parameters.AddWithValue("@quantity", item.Quantity);
                            cmdItem.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
        }

        // Обновление существующего заказа
        public static void UpdateOrder(Orders order, List<OrderProducts> items)
        {
            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        string updateOrder = @"
                    UPDATE orders SET order_date = @orderDate, delivery_date = @deliveryDate,
                           pickup_point_id = @pickupPointId, client_id = @clientId,
                           pickup_code = @pickupCode, status = @status
                    WHERE order_id = @orderId";
                        var cmd = new MySqlCommand(updateOrder, conn, tran);
                        cmd.Parameters.AddWithValue("@orderId", order.OrderId);
                        cmd.Parameters.AddWithValue("@orderDate", order.OrderDate);
                        cmd.Parameters.AddWithValue("@deliveryDate", order.DeliveryDate);
                        cmd.Parameters.AddWithValue("@pickupPointId", order.PickupPointId);
                        cmd.Parameters.AddWithValue("@clientId", order.ClientId);
                        cmd.Parameters.AddWithValue("@pickupCode", order.PickupCode);
                        cmd.Parameters.AddWithValue("@status", order.Status);
                        cmd.ExecuteNonQuery();

                        string deleteItems = "DELETE FROM order_products WHERE order_id = @orderId";
                        var cmdDel = new MySqlCommand(deleteItems, conn, tran);
                        cmdDel.Parameters.AddWithValue("@orderId", order.OrderId);
                        cmdDel.ExecuteNonQuery();

                        foreach (var item in items)
                        {
                            string insertItem = "INSERT INTO order_products (order_id, article, quantity) VALUES (@orderId, @article, @quantity)";
                            var cmdItem = new MySqlCommand(insertItem, conn, tran);
                            cmdItem.Parameters.AddWithValue("@orderId", order.OrderId);
                            cmdItem.Parameters.AddWithValue("@article", item.Article);
                            cmdItem.Parameters.AddWithValue("@quantity", item.Quantity);
                            cmdItem.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
        }

        // Удаление заказа
        public static void DeleteOrder(int orderId)
        {
            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        string deleteItems = "DELETE FROM order_products WHERE order_id = @orderId";
                        var cmdDel = new MySqlCommand(deleteItems, conn, tran);
                        cmdDel.Parameters.AddWithValue("@orderId", orderId);
                        cmdDel.ExecuteNonQuery();

                        string deleteOrder = "DELETE FROM orders WHERE order_id = @orderId";
                        var cmdOrd = new MySqlCommand(deleteOrder, conn, tran);
                        cmdOrd.Parameters.AddWithValue("@orderId", orderId);
                        cmdOrd.ExecuteNonQuery();

                        tran.Commit();
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
        }

        // Получение списка клиентов (авторизированных пользователей)
        public static List<Users> GetClients()
        {
            var clients = new List<Users>();
            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                string query = "SELECT id, full_name FROM users WHERE role = 'Авторизированный клиент' ORDER BY full_name";
                var cmd = new MySqlCommand(query, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        clients.Add(new Users
                        {
                            Id = reader.GetInt32("id"),
                            FullName = reader.GetString("full_name")
                        });
                    }
                }
            }
            return clients;
        }

        // Получение списка пунктов выдачи
        public static List<PickupPoints> GetPickupPoints()
        {
            var points = new List<PickupPoints>();
            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                string query = "SELECT id, address FROM pickup_point ORDER BY address";
                var cmd = new MySqlCommand(query, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        points.Add(new PickupPoints
                        {
                            Id = reader.GetInt32("id"),
                            Address = reader.GetString("address")
                        });
                    }
                }
            }
            return points;
        }
        public static List<string> GetManufacturers()
        {
            try
            {
                var manufacturers = new List<string>();
                using (var conn = new MySqlConnection(ConnectionString))
                {
                    conn.Open();
                    string query = "SELECT DISTINCT manufacturer FROM products ORDER BY manufacturer";
                    var cmd = new MySqlCommand(query, conn);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            manufacturers.Add(reader.GetString("manufacturer"));
                        }
                    }
                }
                return manufacturers;
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine($"MySQL error in GetManufacturers: {ex.Message}");
                throw new Exception("Ошибка при загрузке списка производителей.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"General error in GetManufacturers: {ex.Message}");
                throw new Exception("Произошла ошибка при получении списка производителей.", ex);
            }
        }

        public static List<string> GetCategories()
        {
            try
            {
                var categories = new List<string>();
                using (var conn = new MySqlConnection(ConnectionString))
                {
                    conn.Open();
                    string query = "SELECT DISTINCT category FROM products ORDER BY category";
                    var cmd = new MySqlCommand(query, conn);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            categories.Add(reader.GetString("category"));
                        }
                    }
                }
                return categories;
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine($"MySQL error in GetCategories: {ex.Message}");
                throw new Exception("Ошибка при загрузке списка категорий.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"General error in GetCategories: {ex.Message}");
                throw new Exception("Произошла ошибка при получении списка категорий.", ex);
            }
        }
    }
}