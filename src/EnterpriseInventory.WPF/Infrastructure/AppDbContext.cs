using Microsoft.EntityFrameworkCore;
using EnterpriseInventory.WPF.Models;

namespace EnterpriseInventory.WPF.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Product> Products => Set<Product>();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
       @"Server=(localdb)\MSSQLLocalDB;Database=EnterpriseInventoryDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}