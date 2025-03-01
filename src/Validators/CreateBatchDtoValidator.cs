using FluentValidation;
using InventoryApi.Dto;

namespace inventory_api.src.Validators;

public class CreateBatchDtoValidator : AbstractValidator<CreateBatchDto>
{
    public CreateBatchDtoValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEqual(Guid.Empty).WithMessage("ProductId is required");

        RuleFor(x => x.Stock)
            .GreaterThan(0).WithMessage("Stock must be greater than 0");

        RuleFor(x => x.EntryDate)
            .GreaterThan(0).WithMessage("Entry date is required");

        RuleFor(x => x.ExpirationDate)
            .GreaterThan(0).WithMessage("Expiration date is required")
            .GreaterThan(x => x.EntryDate).WithMessage("Expiration date must be after entry date");
    }
}
