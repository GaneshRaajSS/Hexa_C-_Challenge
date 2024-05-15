using Order_Management_System.Models;
using Order_Management_System.Repoistory;

namespace Order_Management_System
{
    internal class Program
    {
        static void Main(string[] args)
        {
            OrderManagement management  = new OrderManagement();
            
            management.Menu();
        }
    }
}
