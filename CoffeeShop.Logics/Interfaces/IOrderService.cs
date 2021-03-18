using CoffeeShop.Logics.Dtos;
using CoffeeShop.Logics.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoffeeShop.Logics.Interfaces
{
    public interface IOrderService
    {
        Task<List<OrderDto>> GetAllMatchingAsync(OrderFilter filter);
        Task<OrderDto> AddAsync(CreateOrderDto createOrderDto);
        Task<OrderDto> CheckoutAsync(CheckoutOrderDto patchOrderDto);
    }
}
