using System.Configuration;
using System.Data;
using System.Windows;
namespace MMIMApp;
public partial class App : Application
{
    override protected void OnStartup(StartupEventArgs e)
    {
        MainWindow = new MainWindow();
        MainWindow.Show();
        base.OnStartup(e);
        // Initialize the application
    }
}