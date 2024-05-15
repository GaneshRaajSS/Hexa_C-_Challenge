using Order_Management_System.Models;
using Order_Management_System.Repoistory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Order_Management_System.Service
{
    internal class OrderProcessorService : IOrderProcessorService
    {
        readonly IOrderManagementRepository _repository;
        readonly IOrderManagementRepository orderManagementRepository;
        public OrderProcessorService()
        {
            _repository = new OrderProcessor();
        }

        public void createProduct()
        {
            Console.WriteLine("Enter admin username:");
            string adminUsername = Console.ReadLine();
            User admin = new User(0, adminUsername, "", "");

            if (_repository.IsAdminUser(admin))
            {
                Console.WriteLine("Enter product details:");
                Console.WriteLine("Product name:");
                string productName = Console.ReadLine();
                Console.WriteLine("Product description:");
                string description = Console.ReadLine();
                Console.WriteLine("Price:");
                decimal price = decimal.Parse(Console.ReadLine());
                Console.WriteLine("Quantity:");
                int quantity = int.Parse(Console.ReadLine());
                Console.WriteLine("Type:");
                string type = Console.ReadLine();

                Product product = new Product(0, productName, description, price, quantity, type); 

                try
                {
                    _repository.CreateProduct(admin, product); 
                    Console.WriteLine("Product created successfully.");
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine($"Error creating product: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("User is not an admin.");
            }
        }
        public void getAllProducts()
        {
            List<Product> products = _repository.GetAllProducts();
            Console.WriteLine("All Products:");
            foreach (var product in products)
            {
                Console.WriteLine($"ID: {product.ProductId}, Name: {product.ProductName}, Description: {product.Description}, Price: {product.Price}, Quantity: {product.Quantity}, Type: {product.Type}");
            }
        }
        public void cancelOrder()
        {
            Console.WriteLine("Enter UserID:");
            int userId = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter OrderID:");
            int orderId = int.Parse(Console.ReadLine());

            try
            {
                _repository.CancelOrder(orderId);
                Console.WriteLine("Order cancelled successfully.");
            }
            catch (System.Exception e)
            {
                Console.WriteLine($"Error cancelling order: {e.Message}");
            }
        }
        public void getOrderByUser()
        {
            Console.WriteLine("Enter username:");
            string username = Console.ReadLine();
            User user = new User(0, username, "", ""); 

            List<Product> products = _repository.GetOrderByUser(user);
            if (products.Any())
            {
                Console.WriteLine($"Products ordered by user '{username}':");
                foreach (var product in products)
                {
                    Console.WriteLine($"ID: {product.ProductId}, Name: {product.ProductName}, Description: {product.Description}, Price: {product.Price}, Quantity: {product.Quantity}, Type: {product.Type}");
                }
            }
            else
            {
                Console.WriteLine($"No products ordered by user '{username}'.");
            }
        }
        public int CreateUserWithInput()
        {
            Console.WriteLine("Enter username:");
            string username = Console.ReadLine();
            Console.WriteLine("Enter password:");
            string password = Console.ReadLine();
            User user = new User { UserName = username, Password = password };
            return orderManagementRepository.CreateUser(user);
            
        }
       
    }
}
