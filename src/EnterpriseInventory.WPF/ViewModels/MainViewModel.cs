using EnterpriseInventory.WPF.Services.Navigation;
using EnterpriseInventory.WPF.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace EnterpriseInventory.WPF.ViewModels;

//public partial class MainViewModel : BaseViewModel
public partial class MainViewModel : ObservableObject
{
    public string ApplicationTitle => "Enterprise Inventory System";
    public INavigationService NavigationService { get; }

    [ObservableProperty]
    private object currentView;
    public MainViewModel()
    {
        NavigationService = new NavigationService();

        //default page
        CurrentView = new DashboardView();
    }

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
    
}