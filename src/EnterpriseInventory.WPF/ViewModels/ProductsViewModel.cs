using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EnterpriseInventory.WPF.Infrastructure;
using EnterpriseInventory.WPF.Models;
using EnterpriseInventory.WPF.ViewModels;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Xml.Linq;

namespace EnterpriseInventory.WPF.ViewModels;
//public partial class ProductsViewModel : BaseViewModel
public partial class ProductsViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<User> users = new();

    [ObservableProperty]
    private Product? selectedProduct;

    [ObservableProperty]
    private string newName = "";

    [ObservableProperty]
    private string newSKU = "";

    [ObservableProperty]
    private string newCategory = "";

    [ObservableProperty]
    private decimal newPrice;

    [ObservableProperty]
    private int newQuantity;

    [ObservableProperty]
    private int newMinStockLevel;

    private string _searchText = string.Empty;

    public ObservableCollection<Product> Products { get; set; } = new();
    public string SearchText
    {
        get => _searchText;
        set { _searchText = value; OnPropertyChanged(); Search(); }
    }

    public ProductsViewModel(AppDbContext appDbContext)
    {
        LoadProducts();
    }
   
    private void LoadProducts()
    {
        using var db = new AppDbContext();

        Products = new ObservableCollection<Product>(
            db.Products.OrderBy(x => x.Id).ToList());
    }

    private void Search()
    {
        using var db = new AppDbContext();

        Products.Clear();

        var filtered = db.Products
            .Where(p =>
                p.Name.Contains(SearchText) ||
                p.SKU.Contains(SearchText))
            .ToList();

        foreach (var product in filtered)
            Products.Add(product);
    }


    [RelayCommand]
    private void AddProduct()
    {
        if (string.IsNullOrWhiteSpace(NewName))
            return;

        using var db = new AppDbContext();

        var product = new Product
        {
            Name = NewName,
            SKU = NewSKU,
            Category = NewCategory,
            Price = NewPrice,
            Quantity = NewQuantity,
            MinStockLevel = NewMinStockLevel,

        };

        db.Products.Add(product);
        db.SaveChanges();

        LoadProducts();
        NewName = "";
        NewSKU = "";
        NewCategory = "";
    }
    [RelayCommand]
    private void DeleteProduct()
    {
        if (SelectedProduct == null)
            return;
        using var db = new AppDbContext();
        var product = db.Products.Find(SelectedProduct.Id);

        if (product != null)
        {
            db.Products.Remove(product);
            db.SaveChanges();
        }
        LoadProducts();
    }

    [RelayCommand]
    private void UpdateProduct(AppDbContext appDbContext)
    {
        LoadProducts();
    }
}