using Order_Management_System.Models;
using Order_Management_System.Repoistory;
using Order_Management_System.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Management_System
{
    internal class OrderManagement
    {
        //readonly IOrderManagementRepository orderManagementRepository;
        readonly IOrderProcessorService _orderProcessorService;
        public OrderManagement()
        {
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
            Console.WriteLine("6.Exit");
            Console.Write("Select an Option: ");
            int userOption = int.Parse(Console.ReadLine());
            switch (userOption)
            {
                case 1:
                    _orderProcessorService.CreateUser();
                    break;
                case 2:
                    _orderProcessorService.createProduct();
                    break;
                case 3:
                    _orderProcessorService.cancelOrder();
                    break;
                case 4:
                    _orderProcessorService.getAllProducts();
                    break;
                case 5:
                    _orderProcessorService.getOrderByUser();
                    break;
                case 6:

                    break;
            }
        }
    }
}
