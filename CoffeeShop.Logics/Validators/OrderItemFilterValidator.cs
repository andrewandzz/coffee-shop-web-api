using CoffeeShop.Logics.Filters;
using FluentValidation;
using System;

namespace CoffeeShop.Logics.Validators
{
    public class OrderItemFilterValidator : AbstractValidator<OrderItemFilter>
    {
        public OrderItemFilterValidator()
        {
            RuleFor(dto => dto.CustomerGuid)
                .Must(guid => Guid.TryParse(guid, out _))
                .When(dto => dto.CustomerGuid != null)
                    .WithMessage(dto => $"Value '{dto.CustomerGuid}' of property '{nameof(dto.CustomerGuid)}' has invalid format.");
        }
    }
}
