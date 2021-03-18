using CoffeeShop.Logics.Dtos;
using FluentValidation;
using System;

namespace CoffeeShop.Logics.Validators
{
    internal class CreateOrderItemDtoValidator : AbstractValidator<CreateOrderItemDto>
    {
        public CreateOrderItemDtoValidator()
        {
            RuleFor(dto => dto.CoffeeId)
                .NotNull()
                    .WithMessage(dto => $"You did not specify required property '{nameof(dto.CoffeeId)}'.");

            RuleFor(dto => dto.OrderId)
                .NotNull()
                .When(dto => dto.CustomerGuid == null, ApplyConditionTo.CurrentValidator)
                    .WithMessage(dto => $"You should specify either '{nameof(dto.OrderId)}' or '{nameof(dto.CustomerGuid)}'. You did neither.")
                .Null()
                .When(dto => dto.CustomerGuid != null, ApplyConditionTo.CurrentValidator)
                    .WithMessage(dto => $"You should specify either '{nameof(dto.OrderId)}' or '{nameof(dto.CustomerGuid)}'. You did either.");

            RuleFor(dto => dto.CustomerGuid)
                .NotNull()
                .When(dto => dto.OrderId == null, ApplyConditionTo.CurrentValidator)
                    .WithMessage(dto => $"You should specify either '{nameof(dto.OrderId)}' or '{nameof(dto.CustomerGuid)}'. You did neither.")
                .Null()
                .When(dto => dto.OrderId != null, ApplyConditionTo.CurrentValidator)
                    .WithMessage(dto => $"You should specify either '{nameof(dto.OrderId)}' or '{nameof(dto.CustomerGuid)}'. You did either.")
                .Must(guid => Guid.TryParse(guid, out _))
                .When(dto => dto.CustomerGuid != null && dto.OrderId == null, ApplyConditionTo.CurrentValidator)
                    .WithMessage(dto => $"Value '{dto.CustomerGuid}' of property '{nameof(dto.CustomerGuid)}' has invalid format.");

            RuleFor(dto => dto.Sugar)
                .NotNull()
                    .WithMessage(dto => $"You did not specify required property '{nameof(dto.Sugar)}'.")
                .GreaterThanOrEqualTo(0)
                    .WithMessage(dto => $"Property '{nameof(dto.Sugar)}' should have value from 0 to 3. You passed {dto.Sugar}.")
                .LessThanOrEqualTo(3)
                    .WithMessage(dto => $"Property '{nameof(dto.Sugar)}' should have value from 0 to 3. You passed {dto.Sugar}.");

            RuleFor(dto => dto.CupCap)
                .NotNull()
                    .WithMessage(dto => $"You did not specify required property '{nameof(dto.CupCap)}'.");
        }
    }
}
