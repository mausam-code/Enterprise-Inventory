using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EnterpriseInventory.WPF.Infrastructure;
using EnterpriseInventory.WPF.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace EnterpriseInventory.WPF.ViewModels;

public partial class DashboardViewModel : ObservableObject
{

    [ObservableProperty]
    private int totalProducts;

    [ObservableProperty]
    private int totalUsers;

    [ObservableProperty]
    private int totalSales;
    public DashboardViewModel()
    {
        LoadDashboardData();
    }

    private void LoadDashboardData()
    {
        using var db = new AppDbContext();

        TotalProducts = db.Products.Count();
        TotalUsers = db.Users.Count();
        TotalSales = db.Sales.Count();
    }
}
