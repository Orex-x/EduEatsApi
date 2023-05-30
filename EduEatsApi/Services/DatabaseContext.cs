using EduEatsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EduEatsApi.Services;

public class DatabaseContext : DbContext
{
    public DbSet<Category> Categorys { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Combo> Combos { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductBasket> ProductBaskets { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<BankCard> BankCards { get; set; }
    public DbSet<FavoriteProduct> FavoriteProducts { get; set; }

    public DatabaseContext()
    {
        try
        {
            Database.EnsureCreated();
        }
        catch (Exception)
        {
            // ignored
        }
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        /*optionsBuilder.UseNpgsql(
            "host=172.21.0.2;port=5432;database=EduEats;username=postgres;password=333");*/
        
        optionsBuilder.UseNpgsql(
            "host=localhost;port=5432;database=EduEats;username=postgres;password=333");
    }
}