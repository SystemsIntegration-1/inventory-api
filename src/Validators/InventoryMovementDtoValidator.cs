using FluentValidation;
using InventoryApi.Dto;

namespace inventory_api.src.Validators;

public class InventoryMovementDtoValidator : AbstractValidator<InventoryMovementDto>
{
    public InventoryMovementDtoValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEqual(Guid.Empty).WithMessage("ProductId is required");

        RuleFor(x => x.MovementType)
            .NotEmpty().WithMessage("Movement type is required")
            .Must(x => x.ToLower() == "outgoing").WithMessage("Only outgoing movements are supported");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than 0");

        RuleFor(x => x.Origin)
            .NotEmpty().WithMessage("Origin is required");

        RuleFor(x => x.Destination)
            .NotEmpty().WithMessage("Destination is required");
    }
}
