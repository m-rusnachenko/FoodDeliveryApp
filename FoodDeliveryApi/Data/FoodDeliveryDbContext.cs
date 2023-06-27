using FoodDeliveryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryApi.Data;

public class FoodDeliveryDbContext : DbContext
{
    public FoodDeliveryDbContext(DbContextOptions<FoodDeliveryDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Shop> Shops => Set<Shop>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderProduct> OrderProducts => Set<OrderProduct>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>()
            .HasOne(o => o.User)
            .WithMany(u => u.Orders)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.Shop)
            .WithMany(s => s.Orders)
            .OnDelete(DeleteBehavior.Cascade);

    }
}