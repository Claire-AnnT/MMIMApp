using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using MiniMartApp.Models;

namespace MiniMartApp
{
    public partial class Products : Window
    {
        private ObservableCollection<Product> productList = new ObservableCollection<Product>();

        public Products()
        {
            InitializeComponent();
            dgProducts.ItemsSource = productList;
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddProductWindow();
            if (addWindow.ShowDialog() == true && addWindow.NewProduct != null)
                productList.Add(addWindow.NewProduct);
        }

        private void EditProduct_Click(object sender, RoutedEventArgs e)
        {
            if (dgProducts.SelectedItem is Product selected)
            {
                var editWindow = new EditProductWindow(selected);
                if (editWindow.ShowDialog() == true)
                    dgProducts.Items.Refresh();
            }
        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            if (dgProducts.SelectedItem is Product selected)
                productList.Remove(selected);
        }

        private void SearchProduct_Click(object sender, RoutedEventArgs e)
        {
            string query = txtSearch.Text?.ToLower();
            dgProducts.ItemsSource = string.IsNullOrWhiteSpace(query)
                ? productList
                : new ObservableCollection<Product>(productList.Where(p => p.Name.ToLower().Contains(query)));
        }

        private void GoHome_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }
    }
}







