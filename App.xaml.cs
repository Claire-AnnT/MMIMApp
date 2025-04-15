using System.Configuration;
using System.Data;
using System.Windows;
using MMIMApp.Controllers;
namespace MMIMApp;
public partial class App : Application
{
    override protected void OnStartup(StartupEventArgs e)
    {
        MainWindow = new MainWindow()
        {
            DataContext = new MainViewModel()
        };
        
        MainWindow.Show();
        base.OnStartup(e);
    }
}