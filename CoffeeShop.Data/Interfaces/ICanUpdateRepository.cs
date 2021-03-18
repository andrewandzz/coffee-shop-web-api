namespace CoffeeShop.Data.Interfaces
{
    public interface ICanUpdateRepository<TEntity> where TEntity: class
    {
        void Update(TEntity entity);
    }
}
