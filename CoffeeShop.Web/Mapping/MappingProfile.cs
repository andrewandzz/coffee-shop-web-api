using AutoMapper;
using CoffeeShop.Logics.Dtos;
using CoffeeShop.Logics.Filters;
using CoffeeShop.Web.Infrastructure;
using CoffeeShop.Web.Models;
using CoffeeShop.Web.Resources;

namespace CoffeeShop.Web.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GetCoffeeQueryParams, CoffeeFilter>();
            CreateMap<GetOrderQueryParams, OrderFilter>();
            CreateMap<GetOrderItemQueryParams, OrderItemFilter>();

            CreateMap<CreateOrderModel, CreateOrderDto>();
            CreateMap<CreateOrderItemModel, CreateOrderItemDto>();
            CreateMap<PatchOrderItemModel, PatchOrderItemDto>();

            CreateMap<CheckoutOrderModel, CheckoutOrderDto>();

            CreateMap<CoffeeDto, CoffeeResource>();
            CreateMap<OrderItemDto, OrderItemResource>();
            CreateMap<OrderDto, OrderResource>();
        }
    }
}
