using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Management_System.Models
{
    internal class Clothing:Product
    {
        string size;
        string color;

        public string Size {  
            get { return size; } 
            set {  size = value; } 
        }
        public string Color
        {
            get { return color; }
            set { color = value; }
        }
        public Clothing(int productId, string productName, string description, decimal price, int quantity, string type, string size, string color)
            : base(productId, productName, description, price, quantity, type)
        {
            Size = size;
            Color = color;
        }
    }
}
