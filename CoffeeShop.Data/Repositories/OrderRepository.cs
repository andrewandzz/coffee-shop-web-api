using CoffeeShop.Data.Entities;
using CoffeeShop.Data.Filters;
using CoffeeShop.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShop.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ShopDbContext db;

        public OrderRepository(ShopDbContext db)
        {
            this.db = db;
        }

        public void Add(Order order)
        {
            db.Orders.Add(order);
        }

        public Task<List<Order>> FindActiveAsync(string customerGuid)
        {
            return db.Orders.Where(o => o.CustomerGuid == customerGuid && o.CheckedOut == false).ToListAsync();
        }

        public Task<List<Order>> FindAllMatchingAsync(OrderFilter filter)
        {
            IQueryable<Order> query = db.Orders.AsQueryable();

            if (filter.CustomerGuid != null)
            {
                query = query.Where(o => o.CustomerGuid == filter.CustomerGuid);
            }

            if (filter.CheckedOut != null)
            {
                query = query.Where(o => o.CheckedOut == filter.CheckedOut.Value);
            }

            return query.ToListAsync();
        }

        public ValueTask<Order> FindByIdAsync(int id)
        {
            return db.Orders.FindAsync(id);
        }

        public void Update(Order order)
        {
            db.Orders.Update(order);
        }
    }
}
