using CoffeeShop.Data;
using CoffeeShop.Data.Entities;
using CoffeeShop.Data.Filters;
using CoffeeShop.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace CoffeeShop.UnitTests.Repositories
{
    public class OrderItemRepositoryTests
    {
        public class GetAllMethod : IClassFixture<TestsFixture>
        {
            private readonly TestsFixture fixture;

            public GetAllMethod(TestsFixture fixture)
            {
                this.fixture = fixture;
            }

            [Fact]
            public void WhenNoIncludesSpecified_ReturnsEntireCollectionWithNoIncludes()
            {
                // Arrange
                fixture.DetachAllEntriesFromContext();

                // Act
                var orderItems = fixture.Repository.FindAllAsync();

                // Assert
                Assert.Equal(2, orderItems.Count());
                Assert.All(orderItems, oi => Assert.Null(oi.Coffee));
                Assert.All(orderItems, oi => Assert.Null(oi.Order));
            }

            [Fact]
            public void WhenIncludesSpecified_ReturnsEntireCollectionWithIncludes()
            {
                // Arrange
                fixture.DetachAllEntriesFromContext();

                // Act
                var orderItems = fixture.Repository.FindAllAsync(oi => oi.Coffee, oi => oi.Order);

                // Assert
                Assert.Equal(2, orderItems.Count());
                Assert.All(orderItems, oi => Assert.NotNull(oi.Coffee));
                Assert.All(orderItems, oi => Assert.IsType<Coffee>(oi.Coffee));
                Assert.All(orderItems, oi => Assert.NotNull(oi.Order));
                Assert.All(orderItems, oi => Assert.IsType<Order>(oi.Order));
            }
        }

        public class GetByIdMethod : IClassFixture<TestsFixture>
        {
            private readonly TestsFixture fixture;

            public GetByIdMethod(TestsFixture fixture)
            {
                this.fixture = fixture;
            }

            [Fact]
            public void WhenValidIdSpecifiedAndNoIncludes_ReturnsEntryWithNoIncludes()
            {
                // Arrange
                const int Id = 1;
                fixture.DetachAllEntriesFromContext();

                // Act
                var orderItem = fixture.Repository.FindByIdAsync(Id);

                // Assert
                Assert.NotNull(orderItem);
                Assert.Equal(Id, orderItem.Id);
                Assert.Null(orderItem.Coffee);
                Assert.Null(orderItem.Order);
            }

            [Fact]
            public void WhenValidIdSpecifiedWithIncludes_ReturnsEntryWithIncludes()
            {
                // Arrange
                const int Id = 1;
                fixture.DetachAllEntriesFromContext();

                // Act
                var orderItem = fixture.Repository.FindByIdAsync(Id, oi => oi.Coffee, oi => oi.Order);

                // Assert
                Assert.NotNull(orderItem);
                Assert.Equal(Id, orderItem.Id);
                Assert.NotNull(orderItem.Coffee);
                Assert.IsType<Coffee>(orderItem.Coffee);
                Assert.NotNull(orderItem.Order);
                Assert.IsType<Order>(orderItem.Order);
            }

            [Fact]
            public void WhenNotExistingIdSpecifies_ReturnsNull()
            {
                // Arrange
                const int Id = -1;
                fixture.DetachAllEntriesFromContext();

                // Act
                var orderItem = fixture.Repository.FindByIdAsync(Id);

                // Assert
                Assert.Null(orderItem);
            }
        }

        public class FindMethod : IClassFixture<TestsFixture>
        {
            private readonly TestsFixture fixture;

            public FindMethod(TestsFixture fixture)
            {
                this.fixture = fixture;
            }

            [Fact]
            public void WhenValidPredicateSpecifiedWithNoIncludes_ReturnsEntriesWithNoIncludes()
            {
                // Arrange
                const int OrderId = 1;

                // Act
                var orderItems = fixture.Repository.FindByOrderId(oi => oi.OrderId == OrderId);

                // Assert
                Assert.Equal(2, orderItems.Count());
                Assert.All(orderItems, oi => Assert.Equal(OrderId, oi.OrderId));
                Assert.All(orderItems, oi => Assert.Null(oi.Coffee));
                Assert.All(orderItems, oi => Assert.Null(oi.Order));
            }

            [Fact]
            public void WhenValidPredicateSpecifiedWithIncludes_ReturnsEntriesWithIncludes()
            {
                // Arrange
                const int OrderId = 1;

                // Act
                var orderItems = fixture.Repository.FindByOrderId(oi => oi.OrderId == OrderId, oi => oi.Coffee, oi => oi.Order);

                // Assert
                Assert.Equal(2, orderItems.Count());
                Assert.All(orderItems, oi => Assert.Equal(OrderId, oi.OrderId));
                Assert.All(orderItems, oi => Assert.NotNull(oi.Coffee));
                Assert.All(orderItems, oi => Assert.IsType<Coffee>(oi.Coffee));
                Assert.All(orderItems, oi => Assert.NotNull(oi.Order));
                Assert.All(orderItems, oi => Assert.IsType<Order>(oi.Order));
            }

            [Fact]
            public void WhenInvalidPredicateSpecified_ReturnsEmptyCollection()
            {
                // Arrange
                const int CoffeeId = -1;

                // Act
                var orderItems = fixture.Repository.FindByOrderId(oi => oi.CoffeeId == CoffeeId);

                // Assert
                Assert.Empty(orderItems);
            }
        }

        public class FindAllMatchingMethod : IClassFixture<TestsFixture>
        {
            private readonly TestsFixture fixture;

            public FindAllMatchingMethod(TestsFixture fixture)
            {
                this.fixture = fixture;
            }

            [Fact]
            public void WhenValidOrderIdFilterSpecifiedWithNoIncludes_ReturnsEntriesWithNoIncludes()
            {
                // Arrange
                const int OrderId = 1;

                var filter = new OrderItemFilter()
                {
                    OrderId = OrderId
                };

                // Act
                var orderItems = fixture.Repository.FindAllMatchingAsync(filter);

                // Assert
                Assert.Equal(2, orderItems.Count());
                Assert.All(orderItems, oi => Assert.Equal(OrderId, oi.OrderId));
                Assert.All(orderItems, oi => Assert.Null(oi.Coffee));
                Assert.All(orderItems, oi => Assert.Null(oi.Order));
            }

            [Fact]
            public void WhenNotExistingOrderIdFilterSpecified_ReturnsEmptyCollection()
            {
                // Arrange
                const int OrderId = -1;

                var filter = new OrderItemFilter()
                {
                    OrderId = OrderId
                };

                // Act
                var orderItems = fixture.Repository.FindAllMatchingAsync(filter);

                // Assert
                Assert.Empty(orderItems);
            }

            [Fact]
            public void WhenValidOrderIdFilterSpecifiedWithIncludes_ReturnsEntriesWithIncludes()
            {
                // Arrange
                const int OrderId = 1;

                var filter = new OrderItemFilter()
                {
                    OrderId = OrderId
                };

                // Act
                var orderItems = fixture.Repository.FindAllMatchingAsync(filter, oi => oi.Coffee);

                // Assert
                Assert.Equal(2, orderItems.Count());
                Assert.All(orderItems, oi => Assert.Equal(OrderId, oi.OrderId));
                Assert.All(orderItems, oi => Assert.NotNull(oi.Coffee));
                Assert.All(orderItems, oi => Assert.IsType<Coffee>(oi.Coffee));
            }

            [Fact]
            public void WhenValidFiltersSpecifiedThatRequiresPropertyToBeIncluded_ReturnsEntriesWithIncludes()
            {
                // Arrange
                const string CustomerGuid = "guid1";
                const bool CheckedOut = false;

                var filter = new OrderItemFilter()
                {
                    CustomerGuid = CustomerGuid,
                    CheckedOut = CheckedOut
                };

                // Act
                var orderItems = fixture.Repository.FindAllMatchingAsync(filter);

                // Assert
                Assert.Equal(2, orderItems.Count());
                Assert.All(orderItems, oi => Assert.Equal(CustomerGuid, oi.Order.CustomerGuid));
                Assert.All(orderItems, oi => Assert.Equal(CheckedOut, oi.Order.CheckedOut));
                Assert.All(orderItems, oi => Assert.NotNull(oi.Order));
                Assert.All(orderItems, oi => Assert.IsType<Order>(oi.Order));
            }
        }

        public class TestsFixture : IDisposable
        {
            public OrderItemRepository Repository { get; }

            private readonly ShopDbContext context;

            public TestsFixture()
            {
                var options = new DbContextOptionsBuilder<ShopDbContext>()
                    .UseInMemoryDatabase("OrderItems_" + Guid.NewGuid().ToString()).Options;

                context = new ShopDbContext(options);
                context.Coffees.Add(new Coffee() { Name = "Espresso" });
                context.Coffees.Add(new Coffee() { Name = "Americano" });
                context.Orders.Add(new Order() { CustomerGuid = "guid1", CheckedOut = false });
                context.OrderItems.Add(new OrderItem() { CoffeeId = 1, OrderId = 1 });
                context.OrderItems.Add(new OrderItem() { CoffeeId = 2, OrderId = 1 });
                context.SaveChanges();

                Repository = new OrderItemRepository(context);

                DetachAllEntriesFromContext();
            }

            public void DetachAllEntriesFromContext()
            {
                foreach (var entry in context.ChangeTracker.Entries())
                {
                    entry.State = EntityState.Detached;
                }
            }

            public void Dispose()
            {
                context.Database.EnsureDeleted();
                context.Dispose();
            }
        }
    }
}
