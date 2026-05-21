using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EnterpriseInventory.WPF.Services.Auth;
using EnterpriseInventory.WPF.Views;
using System.Windows;
using System.Windows.Controls;

namespace EnterpriseInventory.WPF.ViewModels;

public partial class LoginViewModel : ObservableObject
{
    private readonly IAuthService _authService;

    [ObservableProperty]
    private string username;

    [ObservableProperty]
    private string password;

    public LoginViewModel()
    {
        _authService = new AuthService();
    }

    [RelayCommand]
    //private void Login()
    //{
    //    var user = _authService.Login(username, Password);
    private void Login(object parameter)
    {
        var passwordBox = (PasswordBox)parameter;

        var password = passwordBox.Password;

        var user = _authService.Login(Username, password);

        if (user != null)
        {
            var main = new MainWindow();
            Application.Current.MainWindow = main;
            main.Show();

            foreach(Window w in Application.Current.Windows)
            {
                if (w != main) w.Close();
            }
        }
        else
        {
            MessageBox.Show("Invalid Credentials");
        }
    }
}