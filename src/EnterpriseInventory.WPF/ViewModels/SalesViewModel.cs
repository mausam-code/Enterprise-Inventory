using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EnterpriseInventory.WPF.Infrastructure;
using EnterpriseInventory.WPF.Models;
using EnterpriseInventory.WPF.ViewModels;
using System.Collections.ObjectModel;

namespace EnterpriseInventory.WPF.ViewModels;
public partial class SalesViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Sales> sales = new();

    public SalesViewModel()
    {
        LoadSales();
    }

    private void LoadSales()
    {
        using var db = new AppDbContext();

        Sales = new ObservableCollection<Sales>(
        db.Sales.OrderBy(x => x.VendorName).ToList());
    }

    [ObservableProperty]
    private Sales? selectedSales;

    [ObservableProperty]
    private string newVendorName = "";

    [ObservableProperty]
    private string newContactPerson = "";

    [ObservableProperty]
    private string newBalance = "";

    [ObservableProperty]
    private string newPhone = "";

    [ObservableProperty]
    private string newEmail = "";

    [ObservableProperty]
    private string newAddress = "";


    [RelayCommand]
    private void AddSales()
    {
        if (string.IsNullOrWhiteSpace(NewContactPerson))
            return;

        using var db = new AppDbContext();

        var sale = new Sales
        {
            VendorName = NewVendorName,
            ContactPerson = NewContactPerson,
            Balance = NewBalance,
            Phone = NewPhone,
            Email = NewEmail,
            Address = NewAddress
        };

        db.Sales.Add(sale);
        db.SaveChanges();

        LoadSales();

        NewVendorName = "";
        NewContactPerson = "";
        NewBalance = "";
        NewPhone = "";
        NewEmail = "";
        NewAddress = "";
    }

    [RelayCommand]
    private void DeleteSales()
    {
        if (SelectedSales == null)
            return;

        using var db = new AppDbContext();

        var sale = db.Sales.Find(SelectedSales.Id);

        if (sale != null)
        {
            db.Sales.Remove(sale);
            db.SaveChanges();
        }

        LoadSales();
    }
}
