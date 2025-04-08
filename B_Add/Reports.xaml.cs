using System.Collections.ObjectModel;
using System.Windows;
using MiniMartApp.Models;

namespace MiniMartApp
{
    public partial class Reports : Window
    {
        private ObservableCollection<Product> reportData = new ObservableCollection<Product>();

        public Reports()
        {
            InitializeComponent();
            dgReportPreview.ItemsSource = reportData;
        }

        private void GenerateReport_Click(object sender, RoutedEventArgs e)
        {
            reportData.Clear();
            dgReportPreview.Items.Refresh();
        }

        private void ExportPDF_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Report exported to PDF (placeholder).", "Export", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
