using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseInventory.WPF.Services.Navigation;
public interface INavigationService
{
    void NavigateTo<T>() where T : class;
    object CurrentView { get; }
}
