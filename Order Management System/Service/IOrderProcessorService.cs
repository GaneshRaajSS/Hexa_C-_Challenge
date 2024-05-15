using Order_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Management_System.Service
{
    internal interface IOrderProcessorService
    {
        public int UserExists();
        public void cancelOrder();
        public void getOrderByUser();
        public void createProduct();
        public void getAllProducts();
        public void CreateUser();
    }
}
