namespace CoffeeShop.Data.Interfaces
{
    public interface ICanRemoveRepository<TEntity> where TEntity : class
    {
        void Remove(TEntity entity);
    }
}
