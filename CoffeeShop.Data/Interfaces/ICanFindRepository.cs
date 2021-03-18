using System.Threading.Tasks;

namespace CoffeeShop.Data.Interfaces
{
    public interface ICanFindRepository<TEntity> where TEntity : class
    {
        ValueTask<TEntity> FindByIdAsync(int id);
    }
}
