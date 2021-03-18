using AutoMapper;
using CoffeeShop.Data.Entities;
using CoffeeShop.Data.Interfaces;
using CoffeeShop.Logics.Dtos;
using CoffeeShop.Logics.Filters;
using CoffeeShop.Logics.Infrastructure;
using CoffeeShop.Logics.Interfaces;
using CoffeeShop.Logics.Validators;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShop.Logics.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork db;
        private readonly IMapper mapper;

        public OrderService(IUnitOfWork uow, IMapper mapper)
        {
            this.db = uow;
            this.mapper = mapper;
        }

        public async Task<List<OrderDto>> GetAllMatchingAsync(OrderFilter filter)
        {
            var validator = new OrderFilterValidator();
            ValidationResult validarionResult = validator.Validate(filter);

            if (!validarionResult.IsValid)
            {
                throw new ValidationException(validarionResult.ToString("\n"));
            }

            List<Order> orders = await db.Orders.FindAllMatchingAsync(mapper.Map<Data.Filters.OrderFilter>(filter));

            return mapper.Map<List<OrderDto>>(orders);
        }

        public async Task<OrderDto> AddAsync(CreateOrderDto dto)
        {
            var validator = new CreateOrderDtoValidator();
            ValidationResult validationResult = validator.Validate(dto);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.ToString("\n"));
            }

            List<Order> activeOrders = await db.Orders.FindActiveAsync(dto.CustomerGuid);

            if (activeOrders.Any())
            {
                throw new ValidationException(409, $"Active order with customer guid '{dto.CustomerGuid}' already exists.");
            }

            var newOrder = new Order()
            {
                CustomerGuid = dto.CustomerGuid,
                CustomerName = null,
                CustomerPhone = null,
                TotalPrice = null,
                CreationDate = DateTime.UtcNow,
                CheckedOut = false
            };

            db.Orders.Add(newOrder);

            await db.SaveChangesAsync();

            return mapper.Map<OrderDto>(newOrder);
        }

        public async Task<OrderDto> CheckoutAsync(CheckoutOrderDto dto)
        {
            var validator = new CheckoutOrderDtoValidator();
            ValidationResult validationResult = validator.Validate(dto);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.ToString("\n"));
            }

            Order order = await db.Orders.FindByIdAsync(dto.Id.Value);

            if (order == null)
            {
                throw new NotFoundException($"Order with id {dto.Id} not found.");
            }

            if (order.CheckedOut == true)
            {
                throw new ValidationException(409, $"Order with id {dto.Id} is already checked out.");
            }

            // checkout the order
            order.CustomerName = dto.CustomerName;
            order.CustomerPhone = dto.CustomerPhone;

            List<OrderItem> allOrderItemsForCurrentOrder = await db.OrderItems.FindByOrderIdAsync(order.Id, oi => oi.Coffee);
            order.TotalPrice = allOrderItemsForCurrentOrder.Select(oi => oi.Coffee.Price).Sum();

            order.CheckedOut = true;

            db.Orders.Update(order);

            await db.SaveChangesAsync();

            return mapper.Map<OrderDto>(order);
        }
    }
}
