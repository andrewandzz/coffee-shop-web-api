using AutoMapper;
using CoffeeShop.Logics.Dtos;
using CoffeeShop.Logics.Filters;
using CoffeeShop.Logics.Infrastructure;
using CoffeeShop.Logics.Interfaces;
using CoffeeShop.Web.Infrastructure;
using CoffeeShop.Web.Resources;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoffeeShop.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoffeesController : ControllerBase
    {
        private readonly ICoffeeService coffeeService;
        private readonly IMapper mapper;

        public CoffeesController(ICoffeeService coffeeService, IMapper mapper)
        {
            this.coffeeService = coffeeService;
            this.mapper = mapper;
        }

        // GET api/coffees
        // GET api/coffees?name={name}
        // GET api/coffees?fields={fieldName1,fieldName2,...}
        // GET api/coffees?name={name}&fields={fieldName1,fieldName2,...}
        [HttpGet("")]
        public async Task<ActionResult<List<CoffeeResource>>> GetAll([FromQuery] GetCoffeeQueryParams query)
        {
            try
            {
                CoffeeFilter coffeeFilter = mapper.Map<CoffeeFilter>(query);
                List<CoffeeDto> coffeeDtos = await coffeeService.GetAllMatchingAsync(coffeeFilter);
                List<CoffeeResource> coffeeResources = mapper.Map<List<CoffeeResource>>(coffeeDtos);
                return Ok(coffeeResources);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // GET api/coffees/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CoffeeResource>> GetById([FromRoute] int id)
        {
            try
            {
                CoffeeDto coffeeDto = await coffeeService.GetById(id);
                CoffeeResource coffeeResource = mapper.Map<CoffeeResource>(coffeeDto);
                return Ok(coffeeResource);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}