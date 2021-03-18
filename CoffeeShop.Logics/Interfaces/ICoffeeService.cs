using CoffeeShop.Logics.Dtos;
using CoffeeShop.Logics.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoffeeShop.Logics.Interfaces
{
    public interface ICoffeeService
    {
        /// <summary>
        /// Asynchronously finds a list of <see cref="CoffeeDto"/>
        /// objects that match the specified filter.
        /// </summary>
        /// <param name="filter">A filter that the found objects should match.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains
        /// a list of <see cref="CoffeeDto"/> objects that match
        /// the specified filter.
        /// </returns>
        Task<List<CoffeeDto>> GetAllMatchingAsync(CoffeeFilter filter);

        /// <summary>
        /// Asynchronously finds a <see cref="CoffeeDto"/> by the specified <paramref name="id"/>.
        /// </summary>
        /// <param name="id">Coffee id.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains
        /// a <see cref="CoffeeDto"/> object with the specified id or null.
        /// </returns>
        Task<CoffeeDto> GetById(int id);
    }
}
