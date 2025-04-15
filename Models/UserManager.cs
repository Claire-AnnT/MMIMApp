using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMIMApp.Models
{
    public class UserManager
    {
        public static ObservableCollection<User> users = new ObservableCollection<User>();
        

        public static ObservableCollection<User> GetUsers()
        {
            return users;
        }

        public static void AddUser(User user)
        {
            var existingUser = users.FirstOrDefault(u => u.Username == user.Username);
            if (existingUser != null)
            {
                throw new ArgumentException($"User with username {user.Username} already exists.");
            }
            users.Add(user);
        }

        public static void DeleteUser(uint userID)
        {
            var user = users.FirstOrDefault(u => u.Id == userID);
            if (user != null)
            {
                users.Remove(user);
            }
            else
            {
                throw new ArgumentException($"User with ID {userID} not found.");
            }
        }

        public static void UpdateUser(uint userID, User updatedUser)
        {
            var user = users.FirstOrDefault(u => u.Id == userID);
            if (user != null)
            {
                user.Username = updatedUser.Username;
                user.Password = updatedUser.Password;
                user.FirstName = updatedUser.FirstName;
                user.LastName = updatedUser.LastName;
                user.IsAdmin = updatedUser.IsAdmin;
            }
            else
            {
                throw new ArgumentException($"User with ID {userID} not found.");
            }
        }
    }
}
