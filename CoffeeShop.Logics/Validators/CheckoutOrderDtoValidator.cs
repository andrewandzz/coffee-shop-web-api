using CoffeeShop.Logics.Dtos;
using FluentValidation;

namespace CoffeeShop.Logics.Validators
{
    internal class CheckoutOrderDtoValidator : AbstractValidator<CheckoutOrderDto>
    {
        public CheckoutOrderDtoValidator()
        {
            RuleFor(dto => dto.Id)
                .NotNull()
                    .WithMessage(dto => $"You did not specify required property '{nameof(dto.Id)}'.");

            RuleFor(dto => dto.CustomerName)
                .NotEmpty()
                    .WithMessage(dto => $"You did not specify required property '{nameof(dto.CustomerName)}'.")
                .MaximumLength(30)
                    .WithMessage(dto => $"Property '{nameof(dto.CustomerName)}' should have from 1 to 30 characters in it.");

            RuleFor(dto => dto.CustomerPhone)
                .Matches(@"^\d{7,12}$")
                .When(p => p.CustomerPhone != null)
                    .WithMessage(dto => $"Property '{nameof(dto.CustomerPhone)}' should have from 7 to 12 digits in it.");
        }
    }
}
