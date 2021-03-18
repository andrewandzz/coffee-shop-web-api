using CoffeeShop.Data.Entities;
using CoffeeShop.Data.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoffeeShop.Data.Interfaces
{
    public interface ICoffeeRepository : ICanFindRepository<Coffee>
    {
        /// <summary>
        /// Asynchronously finds a list of <see cref="Coffee"/> 
        /// objects that match the specified filter.
        /// </summary>
        /// <param name="filter">A filter that the found objects should match.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains
        /// a list of <see cref="Coffee"/> objects that match
        /// the specified filter.
        /// </returns>
        Task<List<Coffee>> FindAllMatchingAsync(CoffeeFilter filter);
    }
}
