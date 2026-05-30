using EnterpriseInventory.WPF.Services.Navigation;
using EnterpriseInventory.WPF.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;

namespace EnterpriseInventory.WPF.ViewModels;

//public partial class MainViewModel : BaseViewModel
public partial class MainViewModel : ObservableObject
{
    public string ApplicationTitle => "Enterprise Inventory System";
    public INavigationService NavigationService { get; }

    [ObservableProperty]
    private object currentView;

    [ObservableProperty]
    private bool isLoggedIn = true;
    public MainViewModel()
    {
        NavigationService = new NavigationService();

        //default page
        CurrentView = new DashboardView();
    }
    [RelayCommand]
    private void Logout()
    {
        IsLoggedIn = false;

        CurrentView = new LogoutView();
    }
    //[RelayCommand]
    //private void Logout()
    //{

    //    var loginWindow = new LogoutView();
    //    //if (Application.Current.MainWindow) == main{
    //    //    main.Close();
    //    //}
    //    CurrentView = loginWindow;
    //    Application.Current.MainWindow?.Close();

    //}

    [RelayCommand]
    private void ShowDashboard()
    {
        CurrentView = new DashboardView();
    }
    [RelayCommand]
    private void ShowProducts()
    {
        CurrentView = new ProductsView();
    }
    [RelayCommand]
    private void ShowSales()
    {
        CurrentView = new SalesView();
    }

    [RelayCommand]
    private void ShowReports()
    {
        CurrentView = new ReportsView();
    }
    [RelayCommand]
    private void ShowUserManagement()
    {
        CurrentView = new UserManagement();
    }
}