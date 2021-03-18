using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeShop.UnitTests.Services
{
    public class OrderItemServiceTests
    {
        private static IMapper mapper;

        static OrderItemServiceTests()
        {
            mapper = new MapperConfiguration(config =>
                config.AddProfile(typeof(Logics.Mapping.MappingProfile))
            ).CreateMapper();
        }

        public class GetAllMatchingMethod
        {

        }

        public class GetByIdMethod
        {

        }

        public class AddMethod
        {

        }

        public class PatchMethod
        {

        }

        public class RemoveMethod
        {

        }
    }
}
