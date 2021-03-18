using CoffeeShop.Data.Entities;
using CoffeeShop.Data.Filters;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CoffeeShop.Data.Interfaces
{
    public interface IOrderItemRepository :
        ICanAddRepository<OrderItem>,
        ICanUpdateRepository<OrderItem>,
        ICanRemoveRepository<OrderItem>
    {
        Task<OrderItem> FindByIdAsync(int id, params Expression<Func<OrderItem, object>>[] includes);
        Task<List<OrderItem>> FindByOrderIdAsync(int orderId, params Expression<Func<OrderItem, object>>[] includes);
        Task<List<OrderItem>> FindAllMatchingAsync(OrderItemFilter filter, params Expression<Func<OrderItem, object>>[] includes);
    }
}
