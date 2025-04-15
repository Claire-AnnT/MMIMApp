using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MMIMApp.Models
{
    public class User
    {
        [Key]
        public uint Id { get; set; }
        [Required]
        public string Username { get; set; } = null!;
        [Required,MaxLength(16), MinLength(8)]
        public string Password { get; set; } = null!;
        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        public string LastName { get; set; } = null!;
        [Required]
        public bool IsAdmin { get; set; } = false;
        public ICollection<Product>? Products { get; set; }
        public ICollection<Category>? Categories { get; set; }

        public User(uint id, string username, string password, string firstName, string lastName, bool isAdmin)
        {
            Id = id;
            Username = username;
            SetPassword(password);
            FirstName = firstName;
            LastName = lastName;
            IsAdmin = isAdmin;
        }            public bool IsPasswordValid(string password)
        {
            if (password == null)
            {
                throw new ArgumentNullException(nameof(password), "Password cannot be null");
            }
            if (password.Length < 8 || password.Length > 16)
            {
                throw new ArgumentException("Password must be between 8 and 16 characters long");
            }
            return HashPassword(password) == Password;
        }

        public string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);

                StringBuilder builder = new StringBuilder();

                foreach(byte b in hashBytes)
                {
                    builder.Append(b.ToString("x2"));
                }

                return builder.ToString();
            }

        }

        public void SetPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException(nameof(password), "Password cannot be null or whitespace");

            Password = HashPassword(password);
        }

        public string GetFullName() => $"{FirstName} {LastName}";

    }
}
