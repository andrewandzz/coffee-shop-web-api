using AutoMapper;
using CoffeeShop.Data.Entities;
using CoffeeShop.Data.Interfaces;
using CoffeeShop.Logics.Dtos;
using CoffeeShop.Logics.Filters;
using CoffeeShop.Logics.Infrastructure;
using CoffeeShop.Logics.Services;
using Moq;
using System;
using System.Linq;
using Xunit;

namespace CoffeeShop.UnitTests.Services
{
    public class CoffeeServiceTests
    {
        private static IMapper mapper;

        static CoffeeServiceTests()
        {
            mapper = new MapperConfiguration(config =>
                config.AddProfile(typeof(Logics.Mapping.MappingProfile))
            ).CreateMapper();
        }

        public class GetAllMatchingMethod
        {
            [Fact]
            public void WhenValidNameAndNoFieldsSpecified_ReturnsEntriesWithTheNameAndAllFields()
            {
                // Arrange
                const string Name = "Latte";

                var repo = new Mock<ICoffeeRepository>();
                // we are not testing the repository here, so we assume it returns correct result
                repo.Setup(repo => repo.FindAllMatchingAsync(It.IsAny<Data.Filters.CoffeeFilter>()))
                    .Returns(() => new[]
                    {
                        new Coffee() { Name = Name, Price = 10 },
                        new Coffee() { Name = Name, Price = 12 }
                    });

                var uow = new Mock<IUnitOfWork>();
                uow.SetupGet(uow => uow.Coffees).Returns(repo.Object);

                var service = new CoffeeService(uow.Object, mapper);

                var filter = new CoffeeFilter()
                {
                    Name = Name
                };

                // Act
                var coffeeDtos = service.GetAllMatchingAsync(filter);

                // Assert
                Assert.Equal(2, coffeeDtos.Count());
                Assert.All(coffeeDtos, dto => Assert.Equal(Name, dto.Name));
                Assert.All(coffeeDtos, dto => Assert.IsType<CoffeeDto>(dto));
                Assert.All(coffeeDtos, dto => Assert.NotNull(dto.Price));
            }

            [Fact]
            public void WhenValidNameAndSomeFieldsSpecified_ReturnsEntriesWithTheNameAndOnlySpecifiedFields()
            {
                // Arrange
                const string Name = "Latte";

                var repo = new Mock<ICoffeeRepository>();
                // we are not testing the repository here, so we assume it returns correct result
                repo.Setup(repo => repo.FindAllMatchingAsync(It.IsAny<Data.Filters.CoffeeFilter>()))
                    .Returns(() => new[]
                    {
                        new Coffee() { Name = Name, Price = 10, Volume = "vol1" },
                        new Coffee() { Name = Name, Price = 12, Volume = "vol2" }
                    });

                var uow = new Mock<IUnitOfWork>();
                uow.SetupGet(uow => uow.Coffees).Returns(repo.Object);

                var service = new CoffeeService(uow.Object, mapper);

                var filter = new CoffeeFilter()
                {
                    Name = Name,
                    Fields = "name,Price"
                };

                // Act
                var coffeeDtos = service.GetAllMatchingAsync(filter);

                // Assert
                Assert.Equal(2, coffeeDtos.Count());
                Assert.All(coffeeDtos, dto => Assert.NotNull(dto.Name));
                Assert.All(coffeeDtos, dto => Assert.NotNull(dto.Price));
                Assert.All(coffeeDtos, dto => Assert.Null(dto.Volume));
                Assert.All(coffeeDtos, dto => Assert.Equal(Name, dto.Name));
                Assert.All(coffeeDtos, dto => Assert.IsType<CoffeeDto>(dto));
            }

            [Fact]
            public void WhenInvalidNameSpecified_ReturnsEmptyCollection()
            {
                // Arrange
                const string Name = "Invalid name";

                var repo = new Mock<ICoffeeRepository>();
                // we are not testing the repository here, so we assume it returns correct result
                repo.Setup(repo => repo.FindAllMatchingAsync(It.IsAny<Data.Filters.CoffeeFilter>()))
                    .Returns(() => new Coffee[0]);

                var uow = new Mock<IUnitOfWork>();
                uow.SetupGet(uow => uow.Coffees).Returns(repo.Object);

                var service = new CoffeeService(uow.Object, mapper);

                var filter = new CoffeeFilter()
                {
                    Name = Name
                };

                // Act
                var coffeeDtos = service.GetAllMatchingAsync(filter);

                // Assert
                Assert.Empty(coffeeDtos);
            }
        }

        public class GetByIdMethod
        {
            [Fact]
            public void WhenValidIdSpecified_ReturnsEntry()
            {
                // Arrange
                const int Id = 1;

                // we assume that the repo returns a correct result
                var repo = new Mock<ICoffeeRepository>();
                repo.Setup(r => r.GetById(Id)).Returns(() => new Coffee() { Id = Id });

                var uow = new Mock<IUnitOfWork>();
                uow.Setup(uow => uow.Coffees).Returns(repo.Object);

                var service = new CoffeeService(uow.Object, mapper);

                // Act
                var coffeeDto = service.GetById(Id);

                // Assert
                Assert.NotNull(coffeeDto);
                Assert.IsType<CoffeeDto>(coffeeDto);
                Assert.Equal(Id, coffeeDto.Id);
            }

            [Fact]
            public void WhenNotExistingIdSpecified_ThrowsNotFoundException()
            {
                // Arrange
                const int Id = -1;

                // we assume that the repo returns a correct result
                var repo = new Mock<ICoffeeRepository>();
                repo.Setup(r => r.GetById(Id)).Returns(() => null);

                var uow = new Mock<IUnitOfWork>();
                uow.Setup(uow => uow.Coffees).Returns(repo.Object);

                var service = new CoffeeService(uow.Object, mapper);

                // Act and Assert
                Assert.Throws<NotFoundException>(() => service.GetById(Id));
            }
        }
    }
}
