using System.Windows;
using MiniMartApp.Models;

namespace MiniMartApp
{
    public partial class MainWindow : Window
    {
        public static User LoggedInUser { get; set; }

        public MainWindow()
        {
            if (LoggedInUser == null)
            {
                new LoginWindow().Show();
                Close();
                return;
            }

            InitializeComponent();
            if (LoggedInUser != null)
            {
                txtName.Text = $"{LoggedInUser.FirstName} {LoggedInUser.LastName}";
                txtUsername.Text = LoggedInUser.Username;
            }
        }

        private void GoHome_Click(object sender, RoutedEventArgs e)
        {
            // Already on home
        }

        private void OpenProducts_Click(object sender, RoutedEventArgs e)
        {
            new Products().Show();
            Close();
        }

        private void OpenReports_Click(object sender, RoutedEventArgs e)
        {
            new Reports().Show();
            Close();
        }

        private void OpenUsers_Click(object sender, RoutedEventArgs e)
        {
            new Users().Show();
            Close();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            LoggedInUser = null;
            new LoginWindow().Show();
            Close();
        }
    }
}


