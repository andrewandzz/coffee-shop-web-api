using CoffeeShop.Data.Entities;
using CoffeeShop.Data.Filters;
using CoffeeShop.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShop.Data.Repositories
{
    public class CoffeeRepository : ICoffeeRepository
    {
        private readonly ShopDbContext db;

        public CoffeeRepository(ShopDbContext db)
        {
            this.db = db;
        }

        public Task<List<Coffee>> FindAllMatchingAsync(CoffeeFilter filter)
        {
            IQueryable<Coffee> query = db.Coffees.AsNoTracking().AsQueryable();

            if (filter.Name != null)
            {
                query = query.Where(coffee => coffee.Name.Trim().ToLower() == filter.Name.Trim().ToLower());
            }

            return query.ToListAsync();
        }

        public ValueTask<Coffee> FindByIdAsync(int id)
        {
            return db.Coffees.FindAsync(id);
        }
    }
}
