namespace CoffeeShop.Data.Interfaces
{
    public interface ICanAddRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
    }
}
