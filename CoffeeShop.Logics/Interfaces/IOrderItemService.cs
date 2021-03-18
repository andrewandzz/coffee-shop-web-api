using CoffeeShop.Logics.Dtos;
using CoffeeShop.Logics.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoffeeShop.Logics.Interfaces
{
    public interface IOrderItemService
    {
        Task<List<OrderItemDto>> GetAllMatchingAsync(OrderItemFilter filter);
        Task<OrderItemDto> GetByIdAsync(int id);
        Task<OrderItemDto> AddAsync(CreateOrderItemDto createOrderItemDto);
        Task PatchAsync(int id, PatchOrderItemDto dto);
        Task<OrderItemDto> RemoveAsync(int id);
    }
}
