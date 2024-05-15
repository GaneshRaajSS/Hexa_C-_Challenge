using Order_Management_System.Models;
using Order_Management_System.Repoistory;
using Order_Management_System.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Order_Management_System
{
    internal class OrderManagement
    {
        readonly IOrderManagementRepository orderManagementRepository;
        readonly IOrderProcessorService _orderProcessorService;
        public OrderManagement()
        {
            orderManagementRepository = new OrderProcessor();
            _orderProcessorService = new OrderProcessorService();
        }

        public void Menu()
        {
            
            Console.WriteLine("Welcome to transport System!");
            Console.WriteLine("1.createUser");
            Console.WriteLine("2.createProduct");
            Console.WriteLine("3.cancelOrder");
            Console.WriteLine("4.getAllProducts");
            Console.WriteLine("5.getOrderbyUser");
            Console.WriteLine("6.Create Order");
            Console.WriteLine("7.Exit");
            Console.Write("Select an Option: ");
            int userOption = int.Parse(Console.ReadLine());
            bool FLAG = true;
            while (FLAG)
            {
                switch (userOption)
                {
                    case 1:
                        _orderProcessorService.CreateUserWithInput();
                        AskToContinue(ref FLAG);
                        break;
                    case 2:
                        _orderProcessorService.createProduct();
                        AskToContinue(ref FLAG);
                        break;
                    case 3:
                        _orderProcessorService.cancelOrder();
                        AskToContinue(ref FLAG);
                        break;
                    case 4:
                        _orderProcessorService.getAllProducts();
                        AskToContinue(ref FLAG);
                        break;
                    case 5:
                        _orderProcessorService.getOrderByUser();
                        AskToContinue(ref FLAG);
                        break;
                    case 6:
                        Console.Write("Enter username: ");
                        string username = Console.ReadLine();
                        Console.Write("Enter the number of products to order: ");
                        int numProducts = int.Parse(Console.ReadLine());
                        List<Product> products = new List<Product>();
                        for (int i = 0; i < numProducts; i++)
                        {
                            Console.WriteLine($"Enter details for product {i + 1}:");
                            Console.Write("Enter product ID: ");
                            int productId = int.Parse(Console.ReadLine());
                            Console.Write("Enter quantity: ");
                            int quantity = int.Parse(Console.ReadLine());
                            products.Add(new Product { ProductId = productId, Quantity = quantity });
                        }
                        User user = new User { UserName = username };
                        orderManagementRepository.CreateOrder(user, products);
                        AskToContinue(ref FLAG);
                        break;
                    case 7:
                        break;
                }
            }
            
        }
        static void AskToContinue(ref bool flag)
        {
            OrderManagement orderManagement = new OrderManagement();
            Console.WriteLine("Want to continue? Yes/No");
            string cnn = Console.ReadLine();
            if (cnn.ToLower() == "no")
            {
                flag = false;
            }
            else
            {
                orderManagement.Menu();
            }
        }
    }
}
