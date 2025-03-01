using FluentValidation;
using InventoryApi.Aplication.Dto;

namespace inventory_api.src.Validators;

public class InventoryMovementDtoValidator : AbstractValidator<InventoryMovementDto>
{
    public InventoryMovementDtoValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEqual(Guid.Empty).WithMessage("Product ID is required");

        RuleFor(x => x.MovementType)
            .NotEmpty().WithMessage("Movement type is required")
            .Must(BeValidMovementType).WithMessage("Movement type must be 'outgoing'");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than 0");

        RuleFor(x => x.Origin)
            .NotEmpty().WithMessage("Origin is required")
            .MaximumLength(100).WithMessage("Origin must not exceed 100 characters");

        RuleFor(x => x.Destination)
            .NotEmpty().WithMessage("Destination is required")
            .MaximumLength(100).WithMessage("Destination must not exceed 100 characters");

        RuleFor(x => x.MovementDate)
            .GreaterThanOrEqualTo(0).WithMessage("Movement date cannot be negative");
    }

    private bool BeValidMovementType(string movementType)
    {
        return movementType.ToLower() == "outgoing";
    }
}
