using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EnterpriseInventory.WPF.Infrastructure;
using EnterpriseInventory.WPF.Models;
using EnterpriseInventory.WPF.ViewModels;
using System.Collections.ObjectModel;

namespace EnterpriseInventory.WPF.ViewModels;
public partial class UserManagementViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<User> users = new();

    [ObservableProperty]
    private User? selectedUser;

    [ObservableProperty]
    private string newUsername = "";

    [ObservableProperty]
    private string newPassword = "";

    [ObservableProperty]
    private string newEmail = "";

    [ObservableProperty]
    private string newRole = "User";

    public UserManagementViewModel()
    {
        LoadUsers();
    }

    private void LoadUsers()
    {
        using var db = new AppDbContext();

        Users = new ObservableCollection<User>(
            db.Users.OrderBy(x => x.Username).ToList());
    }

    [RelayCommand]
    private void AddUser()
    {
        if (string.IsNullOrWhiteSpace(NewUsername))
            return;

        using var db = new AppDbContext();

        var user = new User
        {
            Username = NewUsername,
            Password = NewPassword,
            Email = NewEmail,
            Role = NewRole
        };

        db.Users.Add(user);
        db.SaveChanges();

        LoadUsers();

        NewUsername = "";
        NewPassword = "";
        NewEmail = "";
    }

    [RelayCommand]
    private void DeleteUser()
    {
        if (SelectedUser == null)
            return;

        using var db = new AppDbContext();

        var user = db.Users.Find(SelectedUser.Id);

        if (user != null)
        {
            db.Users.Remove(user);
            db.SaveChanges();
        }

        LoadUsers();
    }
}
