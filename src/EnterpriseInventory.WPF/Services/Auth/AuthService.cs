using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnterpriseInventory.WPF.Models;
using EnterpriseInventory.WPF.Infrastructure;

namespace EnterpriseInventory.WPF.Services.Auth;

public class AuthService: IAuthService
{
    //Later replace with DB(EF Core)
    private List<User> _users = new()
    {
        new User {Id= 1, Username = "admin", Password="12345", Role = "Admin"},
        new User {Id = 2, Username = "cashier", Password = "12345", Role = "Cashier"}
    };

    public User? Login(string username, string password)
    {
        using var db = new AppDbContext();
        //return _users.FirstOrDefault(x =>
        //x.Username == username && x.Password == password);
        return db.Users.FirstOrDefault(x => x.Username == username && x.Password == password);
    }

}
