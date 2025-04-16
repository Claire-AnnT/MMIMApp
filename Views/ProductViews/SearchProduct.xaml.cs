using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MMIMApp.Controllers;

namespace MMIMApp.Views.ProductViews
{
    /// <summary>
    /// Interaction logic for SearchProduct.xaml
    /// </summary>
    public partial class SearchProduct : UserControl
    {
        public SearchProduct()
        {
            InitializeComponent();
            DataContext = new SearchProductsViewModel();
        }

        private void ShowContextMenu_Click(object sender, RoutedEventArgs e)
        {
            if(sender is Button button)
            {
                button.ContextMenu.IsOpen = true;
                button.ContextMenu.StaysOpen = false;
                button.ContextMenu.PlacementTarget = button;
                button.ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            }
        }
    }
}
