// AddProductWindow.xaml.cs
using System;
using System.Windows;
using MiniMartApp.Models;

namespace MiniMartApp
{
    public partial class AddProductWindow : Window
    {
        public Product NewProduct { get; private set; }

        public AddProductWindow()
        {
            InitializeComponent();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NewProduct = new Product
                {
                    Name = txtProductName.Text,
                    Brand = txtBrand.Text,
                    Description = txtDescription.Text,
                    UnitPrice = decimal.Parse(txtUnitPrice.Text),
                    Units = int.Parse(txtUnits.Text),
                    MinUnits = int.Parse(txtMinUnits.Text),
                    UID = Guid.NewGuid().ToString()
                };

                DialogResult = true;
                Close();
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter valid numeric values.", "Input Error");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error");
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}

