// EditProductWindow.xaml.cs
using System;
using System.Windows;
using MiniMartApp.Models;

namespace MiniMartApp
{
    public partial class EditProductWindow : Window
    {
        public Product UpdatedProduct { get; private set; }

        public EditProductWindow(Product product)
        {
            InitializeComponent();
            UpdatedProduct = product;

            txtProductName.Text = product.Name;
            txtBrand.Text = product.Brand;
            txtDescription.Text = product.Description;
            txtUnitPrice.Text = product.UnitPrice.ToString();
            txtUnits.Text = product.Units.ToString();
            txtMinUnits.Text = product.MinUnits.ToString();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UpdatedProduct.Name = txtProductName.Text;
                UpdatedProduct.Brand = txtBrand.Text;
                UpdatedProduct.Description = txtDescription.Text;
                UpdatedProduct.UnitPrice = decimal.Parse(txtUnitPrice.Text);
                UpdatedProduct.Units = int.Parse(txtUnits.Text);
                UpdatedProduct.MinUnits = int.Parse(txtMinUnits.Text);

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
