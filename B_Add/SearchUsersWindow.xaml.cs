using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using MiniMartApp.Models;

namespace MiniMartApp
{
    public partial class SearchUsersWindow : Window
    {
        private ObservableCollection<User> allUsers;

        public SearchUsersWindow(ObservableCollection<User> users)
        {
            InitializeComponent();
            allUsers = users;
            dgUsers.ItemsSource = null;
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            string query = txtSearch.Text.ToLower();
            var filtered = allUsers.Where(u => u.Username.ToLower().Contains(query)).ToList();
            dgUsers.ItemsSource = filtered;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void GoHome_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
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
    }
}


