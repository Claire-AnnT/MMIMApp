using System.Windows;
using System.Windows.Controls;
using MiniMartApp.Models;

namespace MiniMartApp
{
    public partial class AddUserWindow : Window
    {
        public User NewUser { get; private set; }

        public AddUserWindow()
        {
            InitializeComponent();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string firstName = txtFirstName.Text.Trim();
            string lastName = txtLastName.Text.Trim();
            string password = txtPassword.Password.Trim();
            string access = (cbAccessLevel.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(firstName) ||
                string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(access))
            {
                MessageBox.Show("Please fill in all fields.", "Validation Error");
                return;
            }

            NewUser = new User
            {
                Username = username,
                FirstName = firstName,
                LastName = lastName,
                Password = password,
                AccessLevel = access
            };

            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}

