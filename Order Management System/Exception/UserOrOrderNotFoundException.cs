using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Management_System.Exceptions
{
    internal class UserOrOrderNotFoundException : Exception
    {
        public UserOrOrderNotFoundException() : base("User or Order ID not found.") { }
        public UserOrOrderNotFoundException(string message) : base(message) { }
        public UserOrOrderNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
