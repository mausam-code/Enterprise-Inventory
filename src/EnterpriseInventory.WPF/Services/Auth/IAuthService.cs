using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnterpriseInventory.WPF.Models;

namespace EnterpriseInventory.WPF.Services.Auth;

public interface IAuthService
{
    User? Login(string username, string password);
}
