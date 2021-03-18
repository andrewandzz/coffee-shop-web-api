using AutoMapper;
using CoffeeShop.Data.Entities;
using CoffeeShop.Logics.Dtos;

namespace CoffeeShop.Logics.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Coffee, CoffeeDto>().ForMember(
                coffeeDto => coffeeDto.Image,
                options => options.MapFrom(coffee => coffee.ImageFileName)
            );
            CreateMap<Order, OrderDto>();
            CreateMap<OrderItem, OrderItemDto>();

            CreateMap<Logics.Filters.OrderFilter, Data.Filters.OrderFilter>();
            CreateMap<Logics.Filters.CoffeeFilter, Data.Filters.CoffeeFilter>();
            CreateMap<Logics.Filters.OrderItemFilter, Data.Filters.OrderItemFilter>();
        }
    }
}
