//using System.Configuration;
//using System.Data;
//using System.Windows;
//using EnterpriseInventory.WPF.Views;
//using EnterpriseInventory.WPF.ViewModels;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;

//namespace EnterpriseInventory.WPF;

//public partial class App: Application
//{
//    public static IHost? AppHost { get; private set; }

//    public App(){
//        AppHost = Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
//        {
//            services.AddSingleton<MainWindow>();
//        })
//        .Build();
//    }

//    protected override async void OnStartup(StartupEventArgs e)
//    {
//        base.OnStartup(e);
//        await AppHost!.StartAsync();
//        var login = new LoginView
//        {
//            DataContext = new LoginViewModel()
//        };

//        var loginWindow = new Window
//        {
//            Content = login,
//            Width = 400,
//            Height = 500,
//            WindowStartupLocation = WindowStartupLocation.CenterScreen,
//            Title="Login"
//        };
//        loginWindow.Show();


//        //MainWindow = new Window
//        //{
//        //    Content = login,
//        //    Width = 400,
//        //    Height = 500
//        //};
//        //MainWindow.Show();


//        //var startupForm = AppHost.Services.GetRequiredService<MainWindow>();
//        //startupForm.Show();
//        //base.OnStartup(e);
//    }

//    protected override async void OnExit(ExitEventArgs e)
//    {
//        await AppHost!.StopAsync();
//        base.OnExit(e);
//    }
//}


using System.Windows;
using EnterpriseInventory.WPF.ViewModels;
using EnterpriseInventory.WPF.Views;
using EnterpriseInventory.WPF.Infrastructure;

namespace EnterpriseInventory.WPF;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        using var db = new AppDbContext();

        db.Database.EnsureCreated();

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