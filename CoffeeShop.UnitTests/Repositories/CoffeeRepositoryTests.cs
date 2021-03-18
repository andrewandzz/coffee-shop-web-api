using CoffeeShop.Data;
using CoffeeShop.Data.Entities;
using CoffeeShop.Data.Filters;
using CoffeeShop.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;

namespace CoffeeShop.UnitTests.Repositories
{
    public class CoffeeRepositoryTests 
    {
        public class FindAllMatchingMethod : IDisposable
        {
            private ShopDbContext context;

            public FindAllMatchingMethod()
            {
                var options = new DbContextOptionsBuilder<ShopDbContext>()
                    .UseInMemoryDatabase("Coffees").Options;
                context = new ShopDbContext(options);
            }

            [Fact]
            public void WhenValidNameSpecified_ReturnsEntries()
            {
                // Arrange
                const string Name = "Americano";

                context.Coffees.Add(new Coffee() { Name = "Espresso" });
                context.Coffees.Add(new Coffee() { Name = "Americano" });
                context.Coffees.Add(new Coffee() { Name = "Americano" });
                context.SaveChanges();

                var filter = new CoffeeFilter() { Name = Name };

                var repo = new CoffeeRepository(context);

                // Act
                var coffees = repo.FindAllMatchingAsync(filter);

                // Assert
                Assert.Equal(2, coffees.Count());
                Assert.All(coffees, c => Assert.Equal(Name, c.Name));
            }

            [Fact]
            public void WhenValidNameRandomCaseSpecified_ReturnsEntries()
            {
                // Arrange
                context.Coffees.Add(new Coffee() { Name = "Americano" });
                context.Coffees.Add(new Coffee() { Name = "Americano with milk" });
                context.Coffees.Add(new Coffee() { Name = "Americano with milk" });
                context.SaveChanges();

                var repo = new CoffeeRepository(context);

                var filter = new CoffeeFilter() { Name = " amERiCaNO WITH miLk  " };

                // Act
                var coffees = repo.FindAllMatchingAsync(filter);

                // Assert
                Assert.Equal(2, coffees.Count());
                Assert.All(coffees, c => Assert.Equal("Americano with milk", c.Name));
            }

            [Fact]
            public void WhenInvalidNameSpecified_ReturnsEmptyCollection()
            {
                // Arrange
                context.Coffees.Add(new Coffee() { Name = "Americano" });
                context.Coffees.Add(new Coffee() { Name = "Lattee" });
                context.Coffees.Add(new Coffee() { Name = "Americano with milk" });
                context.SaveChanges();

                var repo = new CoffeeRepository(context);

                var filter = new CoffeeFilter() { Name = "Invalid name" };

                // Act
                var coffees = repo.FindAllMatchingAsync(filter);

                // Assert
                Assert.Empty(coffees);
                Assert.NotNull(coffees);
            }

            [Fact]
            public void WhenEmptyFilterSpecified_ReturnsEntireCollection()
            {
                // Arrange
                context.Coffees.Add(new Coffee() { Name = "Americano" });
                context.Coffees.Add(new Coffee() { Name = "Lattee" });
                context.Coffees.Add(new Coffee() { Name = "Americano with milk" });
                context.SaveChanges();

                var repo = new CoffeeRepository(context);

                var filter = new CoffeeFilter();

                // Act
                var coffees = repo.FindAllMatchingAsync(filter);

                // Assert
                Assert.Equal(3, coffees.Count());
            }

            public void Dispose()
            {
                context.Database.EnsureDeleted();
                context.Dispose();
            }
        }
    }
}
