using AutoMapper;
using CoffeeShop.Data.Entities;
using CoffeeShop.Data.Interfaces;
using CoffeeShop.Logics.Dtos;
using CoffeeShop.Logics.Filters;
using CoffeeShop.Logics.Infrastructure;
using CoffeeShop.Logics.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CoffeeShop.Logics.Services
{
    public class CoffeeService : ICoffeeService
    {
        private readonly IUnitOfWork db;
        private readonly IMapper mapper;

        public CoffeeService(IUnitOfWork uow, IMapper mapper)
        {
            this.db = uow;
            this.mapper = mapper;
        }

        public async Task<List<CoffeeDto>> GetAllMatchingAsync(CoffeeFilter filter)
        {
            List<Coffee> coffees = await db.Coffees.FindAllMatchingAsync(mapper.Map<Data.Filters.CoffeeFilter>(filter));
            List<CoffeeDto> coffeeDtos = mapper.Map<List<CoffeeDto>>(coffees);

            if (filter.Fields != null)
            {
                coffeeDtos = coffeeDtos.Select(dto => ExcludeAllFieldsExceptFor(dto, filter.Fields)).ToList();
            }

            return coffeeDtos;
        }

        public async Task<CoffeeDto> GetById(int id)
        {
            Coffee coffee = await db.Coffees.FindByIdAsync(id);

            if (coffee == null)
            {
                throw new NotFoundException($"Coffee with id {id} not found.");
            }

            return mapper.Map<CoffeeDto>(coffee);
        }

        /// <summary>
        /// Makes all fields of <paramref name="coffeeDto"/> to be null,
        /// except for ones specified in <paramref name="fieldsQueryParam"/> parameter.
        /// </summary>
        /// <param name="coffeeDto">A <see cref="CoffeeDto"/> where to exclude fields.</param>
        /// <param name="fieldsQueryParam">A string representing a set of fields to include
        /// in <paramref name="coffeeDto"/>, separated by comma.</param>
        /// <returns>
        /// Reference to the modified <paramref name="coffeeDto"/>.
        /// </returns>
        private CoffeeDto ExcludeAllFieldsExceptFor(CoffeeDto coffeeDto, string fieldsQueryParam)
        {
            IEnumerable<string> fields = fieldsQueryParam.Split(",").Select(field => field.Trim().ToLower());

            PropertyInfo[] propertyInfos = coffeeDto.GetType().GetProperties();

            foreach (var propertyInfo in propertyInfos)
            {
                // if there is no this property name in the required set of fields
                // then make it null
                if (!fields.Contains(propertyInfo.Name.ToLower()))
                {
                    propertyInfo.SetValue(coffeeDto, null);
                }
            }

            return coffeeDto;
        }
    }
}
