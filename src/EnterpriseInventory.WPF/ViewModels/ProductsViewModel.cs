using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EnterpriseInventory.WPF.Infrastructure;
using EnterpriseInventory.WPF.Models;
using EnterpriseInventory.WPF.ViewModels;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace EnterpriseInventory.WPF.ViewModels;
public partial class ProductsViewModel : BaseViewModel
//public partial class ProductsViewModel : ObservableObject
{
    private readonly AppDbContext _context;
    
    //[ObservableProperty]
    private Product _selectedProduct = new();

    //[ObservableProperty]
    private string _searchText = string.Empty;

    public ObservableCollection<Product> Products { get; set; } = new();

    //[ObservableProperty]
    //private Product selectedProduct = new();

    //[ObservableProperty]
    //private string searchText = "";
    public Product SelectedProduct
    {
        get => _selectedProduct;
        set { _selectedProduct = value; OnPropertyChanged(); }
    }

    public string SearchText
    {
        get => _searchText;
        set { _searchText = value; OnPropertyChanged(); Search(); }
    }


    public ProductsViewModel(AppDbContext context)
    {
        _context = context;
        //AddCommand = new RelayCommand(_ => AddProduct());
        //UpdateCommand = new RelayCommand(_ => UpdateProduct());
        //DeleteCommand = new RelayCommand(_ => DeleteProduct());
        LoadProducts();
    }

    private void LoadProducts()
    {
        Products.Clear();
        foreach (var product in _context.Products.ToList())
            Products.Add(product);
    }

    private void Search()
    {
        Products.Clear();
        var filtered = _context.Products.Where(p => p.Name.Contains(SearchText) || p.SKU.Contains(SearchText)).ToList();
        foreach (var product in filtered)
            Products.Add(product);
    }

    [RelayCommand]
    //private void AddProduct()
    //{
    //    var product = new Product();
    //    _context.Products.Add(product);
    //    _context.SaveChanges();
    //    LoadProducts();
    //}
    private void AddProduct()
    {
        var product = new Product
        {
            Name = "New Product",
            SKU = $"SKU-{DateTime.Now.Ticks}",
            Category = "General",
            Price = 0,
            Quantity = 1,
            MinStockLevel = 1,
            CreatedDate = DateTime.Now
        };

        _context.Products.Add(product);
        _context.SaveChanges();

        LoadProducts();
    }

    [RelayCommand]
    private void UpdateProduct()
    {
        if (SelectedProduct != null)
        {
            _context.Products.Update(SelectedProduct);
            _context.SaveChanges();
        }
    }
    [RelayCommand]
    private void DeleteProduct()
    {
        if (SelectedProduct != null)
        {
            _context.Products.Remove(SelectedProduct);
            _context.SaveChanges();
            LoadProducts();
        }
    }
}
