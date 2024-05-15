using Order_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Order_Management_System.Util;
using System.Data;
using Order_Management_System.Exceptions;

namespace Order_Management_System.Repoistory
{
    internal class OrderProcessor : IOrderManagementRepository
    {
        SqlConnection sqlConnection = null;
        SqlCommand cmd = null;
        public OrderProcessor() 
        {
            sqlConnection = new SqlConnection(DbConnUtil.GetConnectionString());
            cmd = new SqlCommand();
            cmd.Connection = sqlConnection;
        }
        public void CreateProduct(User user, Product product)
        {
            if (!IsAdminUser(user))
            {
                throw new UnauthorizedAccessException("User is not authorized to create products.");
            }
            int productId = InsertProduct(product);
        }
        public bool IsAdminUser(User user)
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "select count(*) from [User] where username = @username and role = 'Admin'";
            cmd.Parameters.AddWithValue("@username", user.UserName);
            cmd.Connection.Open();
            int adminCount = (int)cmd.ExecuteScalar();
            cmd.Connection.Close();

            return adminCount > 0;
        }

        public void CancelOrder(int orderId)
        {
            try
            {
                if (!OrderExists(orderId))
                {
                    throw new UserOrOrderNotFoundException("User or Order ID not Found");
                }
                cmd.Connection.Open();
                DeleteOrderDetails(orderId);
                cmd.CommandText = "DELETE FROM [Order] WHERE orderId = @orderId";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@orderId", orderId);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Order cancelled successfully.");
                cmd.Connection.Close();
            }
            catch (UserOrOrderNotFoundException)
            {
                throw new UserOrOrderNotFoundException("User or Order ID not Found");
            }
        }
        public bool OrderExists(int orderId)
        {
            cmd.CommandText = "SELECT COUNT(*) FROM [Order] WHERE orderId = @orderId";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@orderId", orderId);
            cmd.Connection.Open();
            int orderCount = (int)cmd.ExecuteScalar();
            cmd.Connection.Close();

            return orderCount > 0;
        }

        private void DeleteOrderDetails(int orderId)
        {
            cmd.CommandText = "DELETE FROM OrderDetails WHERE orderId = @orderId";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@orderId", orderId);
            cmd.ExecuteNonQuery();
        }
        public int CreateUser(User user)
        {
            cmd.CommandText = "Insert into [User] values (@username, @password, @role); Select SCOPE_IDENTITY();";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@username", user.UserName);
            cmd.Parameters.AddWithValue("@password", user.Password);
            cmd.Parameters.AddWithValue("@role", "User");
            cmd.Connection.Open();
            int createUserStatus = Convert.ToInt32(cmd.ExecuteScalar());
            cmd.Connection.Close();
            return createUserStatus;
        }
        public List<Product> GetAllProducts()
        {
            List<Product> productList = new List<Product>();

            cmd.CommandText = "Select * from Product";
            cmd.CommandText = "Select * from Product";
            cmd.Parameters.Clear();
            cmd.Connection.Open();
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Product product = new Product
                    {
                        ProductId = Convert.ToInt32(reader["productId"]),
                        ProductName = Convert.ToString(reader["productName"]),
                        Description = Convert.ToString(reader["description"]),
                        Price = Convert.ToDecimal(reader["price"]),
                        Quantity = Convert.ToInt32(reader["quantityInStock"]),
                        Type = Convert.ToString(reader["type"])
                    };
                    productList.Add(product);
                }
            }
            cmd.Connection.Close();

            return productList;
        }

        public List<Product> GetOrderByUser(User user)
        {
            int userId = GetUserIdByUsername(user.UserName);
            if (userId == -1)
            {
                return new List<Product>();
            }
            return GetOrdersByUserId(userId);
        }

        public int InsertProduct(Product product)
        {
            cmd.Parameters.Clear();
            cmd.CommandText = @"insert into Product values (@productName, @description, @price, @quantityInStock, @type); select SCOPE_IDENTITY();";
            cmd.Parameters.AddWithValue("@productName", product.ProductName);
            cmd.Parameters.AddWithValue("@description", product.Description);
            cmd.Parameters.AddWithValue("@price", product.Price);
            cmd.Parameters.AddWithValue("@quantityInStock", product.Quantity);
            cmd.Parameters.AddWithValue("@type", product.Type);

            cmd.Connection.Open();
            int productId = Convert.ToInt32(cmd.ExecuteScalar());
            cmd.Connection.Close();

            return productId;
        }


        public int GetUserIdByUsername(string username)
        {
            cmd.CommandText = "select userId from [User] where username = @username";
            cmd.Parameters.AddWithValue("@username", username);

            cmd.Connection.Open();
            int userId = Convert.ToInt32(cmd.ExecuteScalar());
            cmd.Connection.Close();

            return userId;
        }
        public List<Product> GetOrdersByUserId(int userId)
        {
            List<Product> orderProducts = new List<Product>();

            cmd.CommandText = @"select p.* from Product p join OrderDetails od on p.productId = od.productId join [Order] o on od.orderId = o.orderId where o.userId = @userId";
            cmd.Parameters.AddWithValue("@userId", userId);

            cmd.Connection.Open();
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Product product = new Product(
                        Convert.ToInt32(reader["productId"]),
                        Convert.ToString(reader["productName"]),
                        Convert.ToString(reader["description"]),
                        Convert.ToDecimal(reader["price"]),
                        Convert.ToInt32(reader["quantityInStock"]),
                        Convert.ToString(reader["type"])
                    );
                    orderProducts.Add(product);
                }
            }
            cmd.Connection.Close();

            return orderProducts;
        }
        public void CreateOrder(User user, List<Product> products)
        {
            try
            {
                int userId = GetUserIfExists(user);

                if (userId != -1)
                {
                    using (SqlConnection connection = new SqlConnection(DbConnUtil.GetConnectionString()))
                    {
                        connection.Open();
                        int orderId = CreateOrderForUser(connection, userId);
                        foreach (Product product in products)
                        {
                            CreateOrderDetails(connection, orderId, product);
                        }

                        Console.WriteLine("Order created successfully.");
                    }
                }
                else
                {
                    Console.WriteLine("User does not exist. Cannot create order.");
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"Error creating order: {ex.Message}");
            }
        }

        private int GetUserIfExists(User user)
        {
            int userId = -1;

            using (SqlConnection connection = new SqlConnection(DbConnUtil.GetConnectionString()))
            {
                connection.Open();
                string query = "SELECT userId FROM [User] WHERE username = @username";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", user.UserName);
                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        userId = Convert.ToInt32(result);
                    }
                }
            }

            return userId;
        }

        private int CreateOrderForUser(SqlConnection connection, int userId)
        {
            int orderId = -1;
            string query = "INSERT INTO [Order] (userId) VALUES (@userId); SELECT SCOPE_IDENTITY();";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@userId", userId);
                orderId = Convert.ToInt32(command.ExecuteScalar());
            }

            return orderId;
        }

        private void CreateOrderDetails(SqlConnection connection, int orderId, Product product)
        {
            string query = "INSERT INTO OrderDetails (orderId, productId, quantity) VALUES (@orderId, @productId, @quantity);";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@orderId", orderId);
                command.Parameters.AddWithValue("@productId", product.ProductId);
                command.Parameters.AddWithValue("@quantity", product.Quantity);
                command.ExecuteNonQuery();
            }
        }


    }
}
