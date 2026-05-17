using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace OOOShoesLib
{
    public class DBOperations
    {
        private const string ConnectionString = "server=127.0.0.1; uid=root; pwd=vertrigo; database=oooshoes;";

        // Получение пользователя по логину и паролю
        public static Users GetUser(string login, string password)
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

        // Получение списка всех товаров
        public static List<Products> GetAllProducts()
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
                            Photo =  reader.GetString("photo")
                        });
                    }
                }
            }
            return products;
        }
    }
}