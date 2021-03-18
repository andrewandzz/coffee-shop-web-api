using System.Threading.Tasks;

namespace CoffeeShop.Data.Interfaces
{
    public interface IUnitOfWork
    {
        ICoffeeRepository Coffees { get; }
        IOrderRepository Orders { get; }
        IOrderItemRepository OrderItems { get; }
        Task SaveChangesAsync();
    }
}
