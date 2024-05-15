using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Management_System.Models
{
    internal class Product
    {
        int productId;
        string productName;
        string description;
        decimal price;
        int quantity;
        string type;

        public int ProductId { 
            get {  return productId; } 
            set {  productId = value; } 
        }
        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public decimal Price
        {
            get { return price; }
            set { price = value; }
        }
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        public Product()
        {

        }

        public Product(int productId, string productName, string description, decimal price, int quantity, string type)
        {
            ProductId = productId;
            ProductName = productName;
            Description = description;
            Price = price;
            Quantity = quantity;
            Type = type;
        }

        
        public override string ToString()
        {
            return $" Id: {productId}\n " +
                $"Name: {productName}\n " +
                $"Description: {description}\n " +
                $"price: {price}\n " +
                $"Quantity: {quantity}\n " +
                $"type: {type}";
        }
    }
}
