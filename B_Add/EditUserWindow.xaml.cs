using System.Windows;
using MiniMartApp.Models;

namespace MiniMartApp
{
    public partial class EditUserWindow : Window
    {
        private User editingUser;

        public EditUserWindow(User user)
        {
            InitializeComponent();
            editingUser = user;

            txtUsername.Text = editingUser.Username;
            txtFirstName.Text = editingUser.FirstName;
            txtLastName.Text = editingUser.LastName;
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) ||
                string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                string.IsNullOrWhiteSpace(txtLastName.Text) ||
                string.IsNullOrWhiteSpace(txtCurrentPassword.Password) ||
                string.IsNullOrWhiteSpace(txtNewPassword.Password))
            {
                MessageBox.Show("All fields must be filled out.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (txtCurrentPassword.Password != editingUser.Password)
            {
                MessageBox.Show("Incorrect current password.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            editingUser.Username = txtUsername.Text;
            editingUser.FirstName = txtFirstName.Text;
            editingUser.LastName = txtLastName.Text;
            editingUser.Password = txtNewPassword.Password;

            MessageBox.Show("User updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}





