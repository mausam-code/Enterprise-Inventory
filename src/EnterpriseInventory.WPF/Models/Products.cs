using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseInventory.WPF.Models;
public class Product
{
    //public int Id { get; set; }
    //public string Category { get; set; } = string.Empty;
    //public string Quantity { get; set; } = string.Empty;
    //public string Price { get; set; } = string.Empty;
    //public string Stock { get; set; } = string.Empty;
    //public DateTime CreatedDate { get; set; } = DateTime.Now;
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string SKU { get; set; } = "";
    public string Category { get; set; } = "";
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public int MinStockLevel { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;

}