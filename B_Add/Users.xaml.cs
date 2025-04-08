using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using MiniMartApp.Models;

namespace MiniMartApp
{
    public partial class Users : Window
    {
        private ObservableCollection<User> userList = new ObservableCollection<User>();

        public Users()
        {
            InitializeComponent();
            dgUsers.ItemsSource = userList;
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddUserWindow();
            if (addWindow.ShowDialog() == true && addWindow.NewUser != null)
                userList.Add(addWindow.NewUser);
        }

        private void EditUser_Click(object sender, RoutedEventArgs e)
        {
            if (dgUsers.SelectedItem is User selected)
            {
                var editWindow = new EditUserWindow(selected);
                if (editWindow.ShowDialog() == true)
                    dgUsers.Items.Refresh();
            }
        }

        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (dgUsers.SelectedItem is User selected)
            {
                var result = MessageBox.Show($"Are you sure you want to delete user '{selected.Username}'?", "Confirm Delete", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    userList.Remove(selected);
                }
            }
        }

        private void OpenSearchUsers_Click(object sender, RoutedEventArgs e)
        {
            new SearchUsersWindow(userList).Show();
        }

        private void GoHome_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        private void OpenProducts_Click(object sender, RoutedEventArgs e)
        {
            new Products().Show();
            this.Close();
        }

        private void OpenReports_Click(object sender, RoutedEventArgs e)
        {
            new Reports().Show();
            this.Close();
        }

        private void OpenUsers_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You are already on the Users page.");
        }
    }
}