using CoffeeShop.Logics.Dtos;
using FluentValidation;

namespace CoffeeShop.Logics.Validators
{
    internal class PatchOrderItemDtoValidator : AbstractValidator<PatchOrderItemDto>
    {
        public PatchOrderItemDtoValidator()
        {
            RuleFor(dto => dto.Sugar)
                .GreaterThanOrEqualTo(0)
                    .WithMessage("Sugar should have value from 0 to 3.")
                .LessThanOrEqualTo(3)
                    .WithMessage("Sugar should have value from 0 to 3.")
                    .When(dto => dto.Sugar != null);
        }
    }
}
