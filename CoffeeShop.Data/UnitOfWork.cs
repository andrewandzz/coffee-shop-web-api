using CoffeeShop.Data.Interfaces;
using System.Threading.Tasks;

namespace CoffeeShop.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICoffeeRepository Coffees { get; }
        public IOrderRepository Orders { get; }
        public IOrderItemRepository OrderItems { get; }

        private readonly ShopDbContext context;

        public UnitOfWork(
            ShopDbContext context,
            ICoffeeRepository coffeeRepository,
            IOrderRepository orderRepository,
            IOrderItemRepository orderItemRepository
        )
        {
            this.context = context;
            Coffees = coffeeRepository;
            Orders = orderRepository;
            OrderItems = orderItemRepository;
        }

        public Task SaveChangesAsync()
        {
            return context.SaveChangesAsync();
        }
    }
}
