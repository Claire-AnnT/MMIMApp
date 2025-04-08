using System.Windows;
using System.Collections.ObjectModel;
using System.Linq;
using MiniMartApp.Models;

namespace MiniMartApp
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Password.Trim();

            if (username == "admin" && password == "admin123")
            {
                MainWindow.LoggedInUser = new User
                {
                    Username = "admin",
                    FirstName = "System",
                    LastName = "Administrator",
                    Password = "admin123",
                    AccessLevel = "Admin"
                };
                new MainWindow().Show();
                Close();
                return;
            }

            if (App.Current.Properties["AllUsers"] is ObservableCollection<User> users)
            {
                var user = users.FirstOrDefault(u => u.Username == username && u.Password == password);
                if (user != null)
                {
                    MainWindow.LoggedInUser = user;
                    new MainWindow().Show();
                    Close();
                    return;
                }
            }

            MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            new SignUpWindow().Show();
            Close();
        }
    }
}





