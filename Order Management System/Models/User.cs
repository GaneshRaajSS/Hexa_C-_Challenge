using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Management_System.Models
{
    internal class User
    {

        int userId;
        string username;
        string password;
        string role;

        public int UserId { 
            get { return userId;} 
            set { userId = value; } 
        }
        public string UserName { 
            get { return username; } 
            set { username = value; } 
        }
        public string Password { 
            get { return password; } 
            set { password = value; } 
        }
        public string Role { 
            get { return role; } 
            set { role = value; } 
        }
        public User()
        {

        }
        public User(int userId, string userName, string password, string role) 
        { 
            UserId = userId;
            UserName = userName;
            Password = password;
            Role = role;
        }
        
    }
}
