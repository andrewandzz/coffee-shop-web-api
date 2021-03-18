using CoffeeShop.Data.Entities;
using CoffeeShop.Data.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoffeeShop.Data.Interfaces
{
    public interface IOrderRepository : ICanFindRepository<Order>, ICanAddRepository<Order>, ICanUpdateRepository<Order>
    {
        Task<List<Order>> FindAllMatchingAsync(OrderFilter filter);
        Task<List<Order>> FindActiveAsync(string customerGuid);
    }
}
