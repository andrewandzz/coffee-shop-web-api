using CoffeeShop.Data.Entities;
using CoffeeShop.Data.Filters;
using CoffeeShop.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CoffeeShop.Data.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly ShopDbContext db;

        public OrderItemRepository(ShopDbContext db)
        {
            this.db = db;
        }

        public Task<OrderItem> FindByIdAsync(int id, params Expression<Func<OrderItem, object>>[] includes)
        {
            return IncludeProperties(includes).FirstOrDefaultAsync(oi => oi.Id == id);
        }

        public Task<List<OrderItem>> FindByOrderIdAsync(int orderId, params Expression<Func<OrderItem, object>>[] includes)
        {
            return IncludeProperties(includes).Where(oi => oi.OrderId == orderId).ToListAsync();
        }

        public Task<List<OrderItem>> FindAllMatchingAsync(OrderItemFilter filter, params Expression<Func<OrderItem, object>>[] includes)
        {
            var query = IncludeProperties(includes);

            if (filter.OrderId != null)
            {
                query = query.Where(oi => oi.OrderId == filter.OrderId.Value);
            }

            if (filter.CustomerGuid != null)
            {
                query = query.Where(oi => oi.Order.CustomerGuid == filter.CustomerGuid).Include(oi => oi.Order);
            }

            if (filter.CheckedOut != null)
            {
                query = query.Where(oi => oi.Order.CheckedOut == filter.CheckedOut).Include(oi => oi.Order);
            }

            return query.ToListAsync();
        }

        public void Add(OrderItem orderItem)
        {
            db.OrderItems.Add(orderItem);
        }

        public ValueTask<OrderItem> FindByIdAsync(int id)
        {
            return db.OrderItems.FindAsync(id);
        }

        public void Update(OrderItem orderItem)
        {
            db.OrderItems.Update(orderItem);
        }

        public void Remove(OrderItem orderItem)
        {
            db.OrderItems.Remove(orderItem);
        }

        private IQueryable<OrderItem> IncludeProperties(params Expression<Func<OrderItem, object>>[] properties)
        {
            return properties.Aggregate(
                db.OrderItems.AsQueryable(),
                (query, include) => query.Include(include)
            );
        }
    }
}
