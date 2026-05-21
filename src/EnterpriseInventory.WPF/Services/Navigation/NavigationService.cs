//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.WebSockets;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Controls;
using System.Windows;

//namespace EnterpriseInventory.WPF.Services.Navigation;
//public class NavigationService : INavigationService
//{
//    private object _currentView;
//    public object CurrentView
//    {
//        get => _currentView;
//        private set => _currentView = value;
//    }

//    public void NavigateTo<T>() where T: class
//    {
//        var viewType = typeof(T);
//        var view = Activator.CreateInstance(viewType);
//        CurrentView = view;

//        //Notify UI manually via binding (we improve later with MVVM)
//        Application.Current.MainWindow.DataContext = Application.Current.MainWindow.DataContext;
//    }
//}

using EnterpriseInventory.WPF.Services.Navigation;
using EnterpriseInventory.WPF.ViewModels;

public class NavigationService : INavigationService
{
    private object _currentView;

    public object CurrentView
    {
        get => _currentView;
        private set => _currentView = value;
    }

    public void NavigateTo<T>() where T : class
    {
        CurrentView = Activator.CreateInstance(typeof(T));

        if (Application.Current.MainWindow?.DataContext is MainViewModel vm)
        {
            vm.CurrentView = CurrentView;
        }
    }
}
