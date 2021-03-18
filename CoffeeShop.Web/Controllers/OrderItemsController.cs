using AutoMapper;
using CoffeeShop.Logics.Dtos;
using CoffeeShop.Logics.Filters;
using CoffeeShop.Logics.Infrastructure;
using CoffeeShop.Logics.Interfaces;
using CoffeeShop.Web.Infrastructure;
using CoffeeShop.Web.Models;
using CoffeeShop.Web.Resources;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoffeeShop.Web.Controllers
{
    [Route("api/order-items")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly IOrderItemService orderItemService;
        private readonly IMapper mapper;

        public OrderItemsController(IOrderItemService orderItemService, IMapper mapper)
        {
            this.orderItemService = orderItemService;
            this.mapper = mapper;
        }

        // GET api/order-items
        // GET api/order-items?order-id={orderId}
        // GET api/order-items?customer-guid={customerGuid}
        // GET api/order-items?order-id={orderId}&customer-guid={customerGuid}
        [HttpGet("")]
        public async Task<ActionResult<List<OrderItemResource>>> GetAll([FromQuery] GetOrderItemQueryParams query)
        {
            try
            {
                OrderItemFilter filter = mapper.Map<OrderItemFilter>(query);
                List<OrderItemDto> orderItemDtos = await orderItemService.GetAllMatchingAsync(filter);
                List<OrderItemResource> orderItemResources = mapper.Map<List<OrderItemResource>>(orderItemDtos);
                return Ok(orderItemResources);
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // GET api/order-items/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<OrderItemResource>> GetById([FromRoute] int id)
        {
            try
            {
                OrderItemDto orderItemDto = await orderItemService.GetByIdAsync(id);
                OrderItemResource orderItemResource = mapper.Map<OrderItemResource>(orderItemDto);
                return Ok(orderItemResource);
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

        // POST api/order-items
        [HttpPost("")]
        public async Task<ActionResult<OrderItemResource>> Add([FromBody] CreateOrderItemModel model)
        {
            try
            {
                OrderItemDto orderItemDto = await orderItemService.AddAsync(mapper.Map<CreateOrderItemDto>(model));
                OrderItemResource orderItemResource = mapper.Map<OrderItemResource>(orderItemDto);
                return StatusCode(201, orderItemResource);
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

        // PATCH api/order-items/{id}
        [HttpPatch("{id:int}")]
        public async Task<ActionResult> Patch([FromRoute] int id, [FromBody] PatchOrderItemModel model)
        {
            try
            {
                PatchOrderItemDto patchOrderItemDto = mapper.Map<PatchOrderItemDto>(model);
                await orderItemService.PatchAsync(id, patchOrderItemDto);
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

        // DELETE api/order-items/{id}
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<OrderItemResource>> Remove([FromRoute] int id)
        {
            try
            {
                OrderItemDto orderItemDto = await orderItemService.RemoveAsync(id);
                OrderItemResource orderItemResource = mapper.Map<OrderItemResource>(orderItemDto);
                return orderItemResource;
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
