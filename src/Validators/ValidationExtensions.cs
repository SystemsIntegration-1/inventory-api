using FluentValidation;
using FluentValidation.AspNetCore;
using InventoryApi.API.Dto;
using InventoryApi.Aplication.Dto;

namespace inventory_api.src.Validators;

public static class ValidationExtensions
{
    public static IServiceCollection AddValidations(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();

        services.AddScoped<IValidator<CreateProductDto>, CreateProductDtoValidator>();
        services.AddScoped<IValidator<CreateBatchDto>, CreateBatchDtoValidator>();
        services.AddScoped<IValidator<InventoryMovementDto>, InventoryMovementDtoValidator>();

        return services;
    }
}