using CoffeeShop.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Data
{
    public class ShopDbContext : DbContext
    {
        public DbSet<Coffee> Coffees { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coffee>().HasMany(c => c.Orders).WithMany(o => o.Coffees)
                .UsingEntity<OrderItem>(
                    builder => builder.HasOne(oi => oi.Order).WithMany(o => o.OrderItems).HasForeignKey(oi => oi.OrderId),
                    builder => builder.HasOne(oi => oi.Coffee).WithMany(c => c.OrderItems).HasForeignKey(oi => oi.CoffeeId),
                    builder => builder.HasKey(oi => oi.Id)
                );

            modelBuilder.Entity<Coffee>().HasData(
                new Coffee() { Id = 1, Name = "Espresso", Volume = "133 ml", Price = 8, ImageFileName = "espresso.webp" },
                new Coffee() { Id = 2, Name = "Americano", Volume = "250 ml", Price = 12, ImageFileName = "americano.webp" },
                new Coffee() { Id = 3, Name = "Americano", Volume = "380 ml", Price = 15, ImageFileName = "americano.webp" },
                new Coffee() { Id = 4, Name = "Americano with milk", Volume = "250 ml", Price = 14, ImageFileName = "americano-with-milk.webp" },
                new Coffee() { Id = 5, Name = "Americano with milk", Volume = "380 ml", Price = 17, ImageFileName = "americano-with-milk.webp" },
                new Coffee() { Id = 6, Name = "Cappuccino", Volume = "250 ml", Price = 15, ImageFileName = "cappuccino.webp" },
                new Coffee() { Id = 7, Name = "Cappuccino", Volume = "380 ml", Price = 18, ImageFileName = "cappuccino.webp" },
                new Coffee() { Id = 8, Name = "Latte", Volume = "250 ml", Price = 15, ImageFileName = "latte.webp" },
                new Coffee() { Id = 9, Name = "Latte", Volume = "380 ml", Price = 18, ImageFileName = "latte.webp" }
            );
        }
    }
}
