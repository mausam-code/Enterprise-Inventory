using System.Windows;
using EnterpriseInventory.WPF.ViewModels;
using EnterpriseInventory.WPF.Views;
using EnterpriseInventory.WPF.Infrastructure;

namespace EnterpriseInventory.WPF;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        //using var db = new AppDbContext();

        //db.Database.EnsureCreated();

        base.OnStartup(e);

        var login = new LoginView
        {
            DataContext = new LoginViewModel()
        };

        var window = new Window
        {
            Content = login,
            Width = 400,
            Height = 500,
            WindowStartupLocation = WindowStartupLocation.CenterScreen,
            Title = "Login"
        };

        window.Show();
    }
}