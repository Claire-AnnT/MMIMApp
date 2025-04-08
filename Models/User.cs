using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMIMApp.Models
{
    class User
    {
        public uint Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public bool IsAdmin { get; set; } = false;
        public ICollection<Product>? Products { get; set; }
        public ICollection<Category>? Categories { get; set; }

        public User(uint id, string username, string password, string firstName, string lastName, bool isAdmin)
        {
            Id = id;
            Username = username;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            IsAdmin = isAdmin;
        }

        public string getFullName()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
