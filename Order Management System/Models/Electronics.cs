using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Management_System.Models
{
    internal class Electronics:Product
    {
        string brand;
        int warrantyPeriod;

        public string Brand { 
            get { return brand; } 
            set { brand = value; }
        }
        public int WarrantyPeriod { 
            get {  return warrantyPeriod; } 
            set {  warrantyPeriod = value; } 
        }
        public Electronics(int productId, string productName, string description, decimal price, int quantity, string type, string brand, int warrantyPeriod) 
            : base(productId,productName,description,price,quantity,type)
        {
            Brand = brand;
            WarrantyPeriod = warrantyPeriod;
        }
        public Electronics():base()
        {
            
        }
    }
}
