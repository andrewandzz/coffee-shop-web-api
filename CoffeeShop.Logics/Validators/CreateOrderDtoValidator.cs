using CoffeeShop.Logics.Dtos;
using FluentValidation;
using System;

namespace CoffeeShop.Logics.Validators
{
    public class CreateOrderDtoValidator : AbstractValidator<CreateOrderDto>
    {
        public CreateOrderDtoValidator()
        {
            RuleFor(dto => dto.CustomerGuid)
                .NotNull()
                    .WithMessage(dto => $"Youd did not specify required property '{nameof(dto.CustomerGuid)}'.")
                .Must(guid => Guid.TryParse(guid, out _))
                .When(dto => dto.CustomerGuid != null, ApplyConditionTo.CurrentValidator)
                    .WithMessage(dto => $"Value '{dto.CustomerGuid}' of property '{nameof(dto.CustomerGuid)}' has invalid format.");
        }
    }
}
