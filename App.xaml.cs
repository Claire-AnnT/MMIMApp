using System.Configuration;
using System.Data;
using System.Windows;
using MMIMApp.Controllers;
using MMIMApp.Data;
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
        MMIMAppContext.SeedTestData();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        base.OnExit(e);

        // Clear your database here
        ClearDatabase();
    }

    private void ClearDatabase()
    {
        using var context = new MMIMAppContext();

        // Optional: Clear relationships first if needed (e.g. FK constraints)
        context.Products.RemoveRange(context.Products);
        context.Categories.RemoveRange(context.Categories);

        context.SaveChanges();
        context.Database.EnsureDeleted();
    }
}