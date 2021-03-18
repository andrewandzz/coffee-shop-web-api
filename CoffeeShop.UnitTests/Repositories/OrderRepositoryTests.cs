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
    public class OrderRepositoryTests
    {
        public class FindAllMatchingMethod : IDisposable
        {
            private ShopDbContext context;

            public FindAllMatchingMethod()
            {
                var options = new DbContextOptionsBuilder<ShopDbContext>()
                    .UseInMemoryDatabase("Orders").Options;
                context = new ShopDbContext(options);
            }

            [Fact]
            public void WhenValidCustomerGuidAndCheckedOutSpecified_ReturnsEntries()
            {
                // Arrange
                const string CustomerGuid = "guid1";
                const bool CheckedOut = true;

                context.Orders.Add(new Order() { CustomerGuid = CustomerGuid, CheckedOut = CheckedOut });
                context.Orders.Add(new Order() { CustomerGuid = CustomerGuid, CheckedOut = CheckedOut });
                context.Orders.Add(new Order() { CustomerGuid = CustomerGuid, CheckedOut = !CheckedOut });
                context.Orders.Add(new Order() { CustomerGuid = "guid2", CheckedOut = CheckedOut });
                context.SaveChanges();

                var repo = new OrderRepository(context);

                var filter = new OrderFilter()
                {
                    CustomerGuid = CustomerGuid,
                    CheckedOut = CheckedOut
                };

                // Act
                var orders = repo.FindAllMatchingAsync(filter);

                // Assert
                Assert.Equal(2, orders.Count());
                Assert.All(orders, o => Assert.Equal(CustomerGuid, o.CustomerGuid));
                Assert.All(orders, o => Assert.Equal(CheckedOut, o.CheckedOut));
            }

            [Fact]
            public void WhenOnlyValidCustomerGuidSpecified_ReturnsEntries()
            {
                // Arrange
                const string CustomerGuid = "guid1";

                context.Orders.Add(new Order() { CustomerGuid = CustomerGuid });
                context.Orders.Add(new Order() { CustomerGuid = "guid2" });
                context.Orders.Add(new Order() { CustomerGuid = CustomerGuid });
                context.SaveChanges();

                var repo = new OrderRepository(context);

                var filter = new OrderFilter()
                {
                    CustomerGuid = CustomerGuid
                };

                // Act
                var orders = repo.FindAllMatchingAsync(filter);

                // Assert
                Assert.Equal(2, orders.Count());
                Assert.All(orders, o => Assert.Equal(CustomerGuid, o.CustomerGuid));
            }

            [Fact]
            public void WhenOnlyCheckedOutSpecified_ReturnsEntries()
            {
                // Arrange
                const bool CheckedOut = true;

                context.Orders.Add(new Order() { CheckedOut = CheckedOut });
                context.Orders.Add(new Order() { CheckedOut = CheckedOut });
                context.Orders.Add(new Order() { CheckedOut = !CheckedOut });
                context.SaveChanges();

                var repo = new OrderRepository(context);

                var filter = new OrderFilter()
                {
                    CheckedOut = CheckedOut
                };

                // Act
                var orders = repo.FindAllMatchingAsync(filter);

                // Assert
                Assert.Equal(2, orders.Count());
                Assert.All(orders, o => Assert.Equal(CheckedOut, o.CheckedOut));
            }

            [Fact]
            public void WhenEmptyFilterSpecified_ReturnsEntireCollection()
            {
                // Arrange
                context.Orders.Add(new Order() { CustomerGuid = "guid1", CheckedOut = true });
                context.Orders.Add(new Order() { CustomerGuid = "guid2", CheckedOut = true });
                context.Orders.Add(new Order() { CustomerGuid = "guid1", CheckedOut = false });
                context.SaveChanges();

                var repo = new OrderRepository(context);

                var filter = new OrderFilter();

                // Act
                var orders = repo.FindAllMatchingAsync(filter);

                // Assert
                Assert.Equal(3, orders.Count());
            }

            [Fact]
            public void WhenOnlyInvalidCustomerGuidSpecified_ReturnsEmptyCollection()
            {
                // Arrange
                context.Orders.Add(new Order() { CustomerGuid = "guid1" });
                context.Orders.Add(new Order() { CustomerGuid = "guid2" });
                context.Orders.Add(new Order() { CustomerGuid = "guid2" });
                context.SaveChanges();

                var repo = new OrderRepository(context);

                var filter = new OrderFilter()
                {
                    CustomerGuid = "Invalid guid"
                };

                // Act
                var orders = repo.FindAllMatchingAsync(filter);

                // Assert
                Assert.Empty(orders);
            }

            [Fact]
            public void WhenInvalidCustomerGuidAndCheckedOutSpecified_ReturnsEmptyCollection()
            {
                // Arrange
                const bool CheckedOut = false;

                context.Orders.Add(new Order() { CustomerGuid = "guid1", CheckedOut = CheckedOut });
                context.Orders.Add(new Order() { CustomerGuid = "guid2", CheckedOut = CheckedOut });
                context.Orders.Add(new Order() { CustomerGuid = "guid2", CheckedOut = !CheckedOut });
                context.SaveChanges();

                var repo = new OrderRepository(context);

                var filter = new OrderFilter()
                {
                    CustomerGuid = "Invalid guid",
                    CheckedOut = CheckedOut
                };

                // Act
                var orders = repo.FindAllMatchingAsync(filter);

                // Assert
                Assert.Empty(orders);
            }

            public void Dispose()
            {
                context.Database.EnsureDeleted();
                context.Dispose();
            }
        }
    }
}
