using AutoMapper;
using CoffeeShop.Data.Entities;
using CoffeeShop.Data.Interfaces;
using CoffeeShop.Logics.Dtos;
using CoffeeShop.Logics.Filters;
using CoffeeShop.Logics.Infrastructure;
using CoffeeShop.Logics.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace CoffeeShop.UnitTests.Services
{
    public class OrderServiceTests
    {
        private static IMapper mapper;

        static OrderServiceTests()
        {
            mapper = new MapperConfiguration(config =>
                config.AddProfile(typeof(Logics.Mapping.MappingProfile))
            ).CreateMapper();
        }

        public class GetAllMatchingMethod
        {
            [Fact]
            public void WhenValidCutomerGuidAndCheckedOutSpecified_ReturnsEntries()
            {
                // Arrange
                const string CustomerGuid = "2e287b6f-4af9-7694-606f-aa8ae7bfdaa8";
                const bool CheckedOut = true;

                var repo = new Mock<IOrderRepository>();
                // we are not testing the repository here, so we assume that it returns correct result
                repo.Setup(repo => repo.FindAllMatchingAsync(It.IsAny<Data.Filters.OrderFilter>()))
                    .Returns(() => new[]
                    {
                        new Order() { CustomerGuid = CustomerGuid, CheckedOut = CheckedOut },
                        new Order() { CustomerGuid = CustomerGuid, CheckedOut = CheckedOut }
                    });

                var uow = new Mock<IUnitOfWork>();
                uow.SetupGet(uow => uow.Orders).Returns(repo.Object);

                var service = new OrderService(uow.Object, mapper);

                var filter = new OrderFilter()
                {
                    CustomerGuid = CustomerGuid,
                    CheckedOut = CheckedOut
                };

                // Act
                var ordersDtos = service.GetAllMatching(filter);

                // Assert
                Assert.Equal(2, ordersDtos.Count());
                Assert.All(ordersDtos, dto => Assert.Equal(CustomerGuid, dto.CustomerGuid));
                Assert.All(ordersDtos, dto => Assert.Equal(CheckedOut, dto.CheckedOut));
                Assert.All(ordersDtos, dto => Assert.IsType<OrderDto>(dto));
            }

            [Fact]
            public void WhenINvalidCutomerGuidSpecified_ThrowsValidationException()
            {
                // Arrange
                var repo = new Mock<IOrderRepository>();
                // we are not testing the repository here, so we assume that it returns correct result
                repo.Setup(repo => repo.FindAllMatchingAsync(It.IsAny<Data.Filters.OrderFilter>()))
                    .Returns(() => null);

                var uow = new Mock<IUnitOfWork>();
                uow.SetupGet(uow => uow.Orders).Returns(repo.Object);

                var service = new OrderService(uow.Object, mapper);

                var filter = new OrderFilter()
                {
                    CustomerGuid = "Invalid guid"
                };

                // Act and Assert
                Assert.Throws<ValidationException>(() => service.GetAllMatching(filter));
            }
        }

        public class AddMethod
        {
            [Fact]
            public void WhenValidCustomerGuidSpecified_AddsEntryAndReturnsItWithCheckedOutFalse()
            {
                // Arrange
                var repo = new Mock<IOrderRepository>();
                repo.Setup(repo => repo.FindActive(It.IsAny<string>())).Returns(() => new Order[0]);
                repo.Setup(repo => repo.Add(It.IsAny<Order>()));

                var uow = new Mock<IUnitOfWork>();
                uow.SetupGet(uow => uow.Orders).Returns(repo.Object);

                var service = new OrderService(uow.Object, mapper);

                var createOrderDto = new CreateOrderDto()
                {
                    CustomerGuid = "2e287b6f-4af9-7694-606f-aa8ae7bfdaa8"
                };

                // Act
                var createdOrderDto = service.Add(createOrderDto);

                // Assert
                Assert.False(createdOrderDto.CheckedOut);
                Assert.IsType<OrderDto>(createdOrderDto);
            }

            [Fact]
            public void WhenInvalidCustomerGuidSpecified_ThrowsValidationException()
            {
                // Arrange
                var repo = new Mock<IOrderRepository>();

                var uow = new Mock<IUnitOfWork>();
                uow.SetupGet(uow => uow.Orders).Returns(repo.Object);

                var service = new OrderService(uow.Object, mapper);

                var createOrderDto = new CreateOrderDto()
                {
                    CustomerGuid = "Invalid guid"
                };

                // Act and Assert
                Assert.Throws<ValidationException>(() => service.Add(createOrderDto));
            }

            [Fact]
            public void WhenNoCustomerGuidSpecified_ThrowsValidationException()
            {
                // Arrange
                var repo = new Mock<IOrderRepository>();

                var uow = new Mock<IUnitOfWork>();
                uow.SetupGet(uow => uow.Orders).Returns(repo.Object);

                var service = new OrderService(uow.Object, mapper);

                var createOrderDto = new CreateOrderDto();

                // Act and Assert
                Assert.Throws<ValidationException>(() => service.Add(createOrderDto));
            }

            [Fact]
            public void WhenTheCustomerGuidSpecifiedAlreadyHasCheckedOutFalse_ThrowsValidationException()
            {
                // Arrange
                var repo = new Mock<IOrderRepository>();
                repo.Setup(repo => repo.FindActive(It.IsAny<string>())).Returns(() => new[] { new Order() });
                repo.Setup(repo => repo.Add(It.IsAny<Order>()));

                var uow = new Mock<IUnitOfWork>();
                uow.SetupGet(uow => uow.Orders).Returns(repo.Object);

                var service = new OrderService(uow.Object, mapper);

                var createOrderDto = new CreateOrderDto()
                {
                    CustomerGuid = "2e287b6f-4af9-7694-606f-aa8ae7bfdaa8"
                };

                // Act and Assert
                Assert.Throws<ValidationException>(() => service.Add(createOrderDto));
            }
        }

        public class CheckoutMethod
        {
            [Fact]
            public void WhenValidIdAndCustomerNameAndCustomerPhoneSpecified_UpdatesOrderAndSetsAllPropertiesAndSetsCheckedOutTrue()
            {
                // Arrange
                var orderRepository = new Mock<IOrderRepository>();
                orderRepository.Setup(repo => repo.GetById(It.IsAny<int>())).Returns(new Order() { CheckedOut = false });
                orderRepository.Setup(repo => repo.Update(It.IsAny<Order>()));

                var orderItem = new OrderItem()
                {
                    Coffee = new Coffee() { Price = 10 }
                };

                var orderItemRepository = new Mock<IOrderItemRepository>();
                orderItemRepository.Setup(repo => repo.FindByOrderId(It.IsAny<Func<OrderItem, bool>>()))
                    .Returns(new[] { orderItem });

                var uow = new Mock<IUnitOfWork>();
                uow.SetupGet(uow => uow.Orders).Returns(orderRepository.Object);
                uow.SetupGet(uow => uow.OrderItems).Returns(orderItemRepository.Object);

                var service = new OrderService(uow.Object, mapper);

                var checkoutOrderDto = new CheckoutOrderDto()
                {
                    Id = 1,
                    CustomerName = "Name",
                    CustomerPhone = "1234567"
                };

                // Act
                var checkedOutOrderDto = service.CheckoutAsync(checkoutOrderDto);

                // Assert
                Assert.True(checkedOutOrderDto.CheckedOut);
                Assert.NotNull(checkedOutOrderDto.CustomerName);
                Assert.NotNull(checkedOutOrderDto.TotalPrice);
                Assert.NotNull(checkedOutOrderDto.CustomerPhone);
                Assert.IsType<OrderDto>(checkedOutOrderDto);
            }

            [Fact]
            public void WhenValidIdAndCustomerNameAndNoCustomerPhoneSpecified_UpdatesOrderAndSetsAllPropertiesAndSetsCheckedOutTrue()
            {
                // Arrange
                var orderRepository = new Mock<IOrderRepository>();
                orderRepository.Setup(repo => repo.GetById(It.IsAny<int>())).Returns(new Order() { CheckedOut = false });
                orderRepository.Setup(repo => repo.Update(It.IsAny<Order>()));

                var orderItem = new OrderItem()
                {
                    Coffee = new Coffee() { Price = 10 }
                };

                var orderItemRepository = new Mock<IOrderItemRepository>();
                orderItemRepository.Setup(repo => repo.FindByOrderId(It.IsAny<Func<OrderItem, bool>>()))
                    .Returns(new[] { orderItem });

                var uow = new Mock<IUnitOfWork>();
                uow.SetupGet(uow => uow.Orders).Returns(orderRepository.Object);
                uow.SetupGet(uow => uow.OrderItems).Returns(orderItemRepository.Object);

                var service = new OrderService(uow.Object, mapper);

                var checkoutOrderDto = new CheckoutOrderDto()
                {
                    Id = 1,
                    CustomerName = "Name"
                };

                // Act
                var checkedOutOrderDto = service.CheckoutAsync(checkoutOrderDto);

                // Assert
                Assert.True(checkedOutOrderDto.CheckedOut);
                Assert.NotNull(checkedOutOrderDto.CustomerName);
                Assert.NotNull(checkedOutOrderDto.TotalPrice);
                Assert.Null(checkedOutOrderDto.CustomerPhone);
                Assert.IsType<OrderDto>(checkedOutOrderDto);
            }

            [Fact]
            public void WhenValidIdAndNoCustomerNameSpecified_ThrowsValidationException()
            {
                // Arrange
                var repo = new Mock<IOrderRepository>();

                var uow = new Mock<IUnitOfWork>();
                uow.SetupGet(uow => uow.Orders).Returns(repo.Object);

                var service = new OrderService(uow.Object, mapper);

                var checkoutOrderDto = new CheckoutOrderDto()
                {
                    Id = 1
                };

                // Act and Assert
                Assert.Throws<ValidationException>(() => service.CheckoutAsync(checkoutOrderDto));
            }

            [Fact]
            public void WhenValidIdAndInvalidCustomerNameSpecified_ThrowsValidationException()
            {
                // Arrange
                var repo = new Mock<IOrderRepository>();

                var uow = new Mock<IUnitOfWork>();
                uow.SetupGet(uow => uow.Orders).Returns(repo.Object);

                var service = new OrderService(uow.Object, mapper);

                var checkoutOrderDto = new CheckoutOrderDto()
                {
                    Id = 1,
                    CustomerName = "NameNameNameNameNameNameNameNameName"
                };

                // Act and Assert
                Assert.Throws<ValidationException>(() => service.CheckoutAsync(checkoutOrderDto));
            }

            [Fact]
            public void WhenValidIdAndCustomerNameAndInvalidCustomerPhoneSpecified_ThrowsValidationException()
            {
                // Arrange
                var repo = new Mock<IOrderRepository>();

                var uow = new Mock<IUnitOfWork>();
                uow.SetupGet(uow => uow.Orders).Returns(repo.Object);

                var service = new OrderService(uow.Object, mapper);

                var checkoutOrderDto = new CheckoutOrderDto()
                {
                    Id = 1,
                    CustomerName = "Name",
                    CustomerPhone = "123"
                };

                // Act and Assert
                Assert.Throws<ValidationException>(() => service.CheckoutAsync(checkoutOrderDto));
            }

            [Fact]
            public void WhenNoIdSpecified_ThrowsValidationException()
            {
                // Arrange
                var repo = new Mock<IOrderRepository>();

                var uow = new Mock<IUnitOfWork>();
                uow.SetupGet(uow => uow.Orders).Returns(repo.Object);

                var service = new OrderService(uow.Object, mapper);

                var checkoutOrderDto = new CheckoutOrderDto();

                // Act and Assert
                Assert.Throws<ValidationException>(() => service.CheckoutAsync(checkoutOrderDto));
            }

            [Fact]
            public void WhenNotExistingIdSpecified_ThrowsNotFoundException()
            {
                // Arrange
                const int Id = -1;

                var repo = new Mock<IOrderRepository>();
                repo.Setup(repo => repo.GetById(Id)).Returns(() => null);

                var uow = new Mock<IUnitOfWork>();
                uow.SetupGet(uow => uow.Orders).Returns(repo.Object);

                var service = new OrderService(uow.Object, mapper);

                var checkoutOrderDto = new CheckoutOrderDto()
                {
                    Id = Id,
                    CustomerName = "Name"
                };

                // Act and Assert
                Assert.Throws<NotFoundException>(() => service.CheckoutAsync(checkoutOrderDto));
            }

            [Fact]
            public void WhenTheOrderIsAlreadyCheckedOut_ThrowsValidationException()
            {
                // Arrange
                var repo = new Mock<IOrderRepository>();
                repo.Setup(repo => repo.GetById(It.IsAny<int>())).Returns(() => new Order() { CheckedOut = true });

                var uow = new Mock<IUnitOfWork>();
                uow.SetupGet(uow => uow.Orders).Returns(repo.Object);

                var service = new OrderService(uow.Object, mapper);

                var checkoutOrderDto = new CheckoutOrderDto()
                {
                    Id = 1,
                    CustomerName = "Name"
                };

                // Act and Assert
                Assert.Throws<ValidationException>(() => service.CheckoutAsync(checkoutOrderDto));
            }
        }
    }
}
