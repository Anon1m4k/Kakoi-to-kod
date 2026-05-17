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
    }
}