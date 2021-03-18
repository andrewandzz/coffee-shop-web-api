using AutoMapper;
using CoffeeShop.Data.Entities;
using CoffeeShop.Data.Interfaces;
using CoffeeShop.Logics.Dtos;
using CoffeeShop.Logics.Filters;
using CoffeeShop.Logics.Infrastructure;
using CoffeeShop.Logics.Interfaces;
using CoffeeShop.Logics.Validators;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShop.Logics.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IUnitOfWork db;
        private readonly IMapper mapper;

        public OrderItemService(IUnitOfWork uow, IMapper mapper)
        {
            this.db = uow;
            this.mapper = mapper;
        }

        public async Task<List<OrderItemDto>> GetAllMatchingAsync(OrderItemFilter filter)
        {
            var validator = new OrderItemFilterValidator();
            ValidationResult validationResult = validator.Validate(filter);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.ToString("\n"));
            }

            List<OrderItem> orderItems = await db.OrderItems.FindAllMatchingAsync(
                mapper.Map<Data.Filters.OrderItemFilter>(filter),
                oi => oi.Coffee, oi => oi.Order
            );

            List<OrderItemDto> orderItemDtos = mapper.Map<List<OrderItemDto>>(orderItems);

            return orderItemDtos;
        }

        public async Task<OrderItemDto> GetByIdAsync(int id)
        {
            OrderItem orderItem = await db.OrderItems.FindByIdAsync(id, oi => oi.Coffee, oi => oi.Order);

            if (orderItem == null)
            {
                throw new NotFoundException($"Order item with id {id} not found.");
            }

            OrderItemDto orderItemDto = mapper.Map<OrderItemDto>(orderItem);

            return orderItemDto;
        }

        public async Task<OrderItemDto> AddAsync(CreateOrderItemDto dto)
        {
            var validator = new CreateOrderItemDtoValidator();
            ValidationResult validationResult = validator.Validate(dto);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.ToString("\n"));
            }

            if (dto.OrderId != null)
            {
                if (await db.Orders.FindByIdAsync(dto.OrderId.Value) == null)
                {
                    throw new NotFoundException($"Order with id {dto.OrderId} not found.");
                }
            }

            if (dto.CustomerGuid != null)
            {
                List<Order> activeOrders = await db.Orders.FindActiveAsync(dto.CustomerGuid);

                if (!activeOrders.Any())
                {
                    throw new NotFoundException($"Active order with customer guid '{dto.CustomerGuid}' not found.");
                }
            }

            if (await db.Coffees.FindByIdAsync(dto.CoffeeId.Value) == null)
            {
                throw new NotFoundException($"Coffee with id {dto.CoffeeId} not found.");
            }

            var orderItem = new OrderItem()
            {
                OrderId = dto.OrderId ?? await GetOrderIdAsync(dto.CustomerGuid),
                CoffeeId = dto.CoffeeId.Value,
                Sugar = dto.Sugar.Value,
                CupCap = dto.CupCap.Value
            };

            db.OrderItems.Add(orderItem);

            await db.SaveChangesAsync();

            OrderItemDto orderItemDto = mapper.Map<OrderItemDto>(await GetByIdAsync(orderItem.Id));

            return orderItemDto;
        }

        public async Task PatchAsync(int id, PatchOrderItemDto dto)
        {
            var validator = new PatchOrderItemDtoValidator();
            ValidationResult validationResult = validator.Validate(dto);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.ToString("\n"));
            }

            OrderItem orderItem = await db.OrderItems.FindByIdAsync(id);

            if (orderItem == null)
            {
                throw new NotFoundException($"Order item with id {id} not found.");
            }

            // try update CoffeeId if new CoffeeId is specified in the dto
            if (dto.CoffeeId != null)
            {
                if (await db.Coffees.FindByIdAsync(dto.CoffeeId.Value) == null)
                {
                    throw new NotFoundException($"Coffee with id {dto.CoffeeId} not found.");
                }

                orderItem.CoffeeId = dto.CoffeeId.Value;
            }

            // update Sugar if specified new value
            if (dto.Sugar != null)
            {
                orderItem.Sugar = dto.Sugar.Value;
            }

            // update CupCap if specified new value
            if (dto.CupCap != null)
            {
                orderItem.CupCap = dto.CupCap.Value;
            }

            // if at least one of the properties is provided, the update orderItem
            if (dto.CoffeeId != null ||
                dto.Sugar != null ||
                dto.CupCap != null
            )
            {
                db.OrderItems.Update(orderItem);

                await db.SaveChangesAsync();
            }
        }

        public async Task<OrderItemDto> RemoveAsync(int id)
        {
            OrderItem orderItem = await db.OrderItems.FindByIdAsync(id);

            if (orderItem == null)
            {
                throw new NotFoundException($"Order item with id {id} not found.");
            }

            db.OrderItems.Remove(orderItem);

            await db.SaveChangesAsync();

            OrderItemDto orderItemDto = mapper.Map<OrderItemDto>(orderItem);

            return orderItemDto;
        }

        private async Task<int> GetOrderIdAsync(string customerGuid)
        {
            List<Order> activeOrders = await db.Orders.FindActiveAsync(customerGuid);
            return activeOrders.First().Id;
        }
    }
}
