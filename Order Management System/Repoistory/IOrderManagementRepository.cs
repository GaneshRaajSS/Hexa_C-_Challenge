    using Order_Management_System.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading.Tasks;

    namespace Order_Management_System.Repoistory
    {
        internal interface IOrderManagementRepository
        {
            void CreateOrder(User user, List<Product> products);
            void CancelOrder(int orderId);
            void CreateProduct(User user, Product product);
            int CreateUser(User user);
            List<Product> GetAllProducts();
            List<Product> GetOrderByUser(User user);
            int UserExists(User user);
            public int CreateOrderForUser(int userId);
            public void CreateOrderDetails(int orderId, Product product);
            public bool OrderExists(int orderId);
            //public void DeleteOrder(int orderId);
            public bool IsAdminUser(User user);
            public int InsertProduct(Product product);
            public int GetUserIdByUsername(string username);
            public List<Product> GetOrdersByUserId(int userId);

        }
    }
