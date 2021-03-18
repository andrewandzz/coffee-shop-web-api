using AutoMapper;
using CoffeeShop.Logics.Dtos;
using CoffeeShop.Logics.Filters;
using CoffeeShop.Logics.Infrastructure;
using CoffeeShop.Logics.Interfaces;
using CoffeeShop.Web.Infrastructure;
using CoffeeShop.Web.Models;
using CoffeeShop.Web.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShop.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService orderService;
        private readonly IMapper mapper;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            this.orderService = orderService;
            this.mapper = mapper;
        }

        // HEAD api/orders?customer-guid={customerGuid}
        // HEAD api/orders?checked-out={checkedOut}
        // HEAD api/orders?customer-guid={customerGuid}&checked-out={checkedOut}
        [HttpHead("")]
        public async Task<ActionResult> Exists([FromQuery] GetOrderQueryParams query)
        {
            try
            {
                OrderFilter filter = mapper.Map<OrderFilter>(query);
                List<OrderDto> orderDtos = await orderService.GetAllMatchingAsync(filter);

                if (orderDtos.Any())
                {
                    List<OrderResource> orderResources = mapper.Map<List<OrderResource>>(orderDtos);
                    HttpContext.Response.ContentLength = JsonConvert.SerializeObject(orderResources).Length;
                }
                else
                {
                    HttpContext.Response.ContentLength = 0;
                }

                return StatusCode(204);
            }
            catch (ValidationException e)
            {
                return StatusCode(e.StatusCode, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // GET api/orders
        // GET api/orders?customer-guid={customerGuid}
        // GET api/orders?checked-out={checkedOut}
        // GET api/orders?customer-guid={customerGuid}&checked-out={checkedOut}
        [HttpGet("")]
        public async Task<ActionResult<List<OrderResource>>> GetAll([FromQuery] GetOrderQueryParams query)
        {
            try
            {
                OrderFilter filter = mapper.Map<OrderFilter>(query);
                List<OrderDto> orderDtos = await orderService.GetAllMatchingAsync(filter);
                List<OrderResource> orderResources = mapper.Map<List<OrderResource>>(orderDtos);
                return StatusCode(200, orderResources);
            }
            catch (ValidationException e)
            {
                return StatusCode(e.StatusCode, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // POST api/orders
        [HttpPost("")]
        public async Task<ActionResult<OrderResource>> Add([FromBody] CreateOrderModel model)
        {
            try
            {
                CreateOrderDto createOrderDto = mapper.Map<CreateOrderDto>(model);
                OrderDto orderDto = await orderService.AddAsync(createOrderDto);
                OrderResource orderResource = mapper.Map<OrderResource>(orderDto);
                return StatusCode(201, orderResource);
            }
            catch (ValidationException e)
            {
                return StatusCode(e.StatusCode, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // POST api/orders:checkout
        [HttpPost("/api/[controller]:checkout")]
        public async Task<ActionResult> Checkout([FromBody] CheckoutOrderModel model)
        {
            try
            {
                CheckoutOrderDto checkoutOrderDto = mapper.Map<CheckoutOrderDto>(model);
                await orderService.CheckoutAsync(checkoutOrderDto);
                return StatusCode(204);
            }
            catch (ValidationException e)
            {
                return StatusCode(e.StatusCode, e.Message);
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
