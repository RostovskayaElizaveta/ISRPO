using System.Collections.Generic;
using System.Data.SqlClient;

namespace PR1
{
    internal class DatabaseHelper
    {
        private string connectionString = @"Data Source=DESKTOP-12A54C2\SQLEXPRESS;Initial Catalog=supermarket;Integrated Security=True";

        public List<Product> GetProducts()
        {
            var products = new List<Product>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT id, name, price FROM products";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            products.Add(new Product
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Price = reader.GetDecimal(2)
                            });
                        }
                    }
                }
            }

            return products;
        }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public override string ToString()
        {
            return $"{Name} - {Price:C}";
        }
    }
}
