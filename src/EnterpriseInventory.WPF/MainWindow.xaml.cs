using EnterpriseInventory.WPF.ViewModels;
using System.Windows;

namespace EnterpriseInventory.WPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainViewModel();
    }
}