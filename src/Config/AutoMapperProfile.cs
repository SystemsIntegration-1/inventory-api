using AutoMapper;
using InventoryApi.API.Dto;
using InventoryApi.Aplication.Dto;
using InventoryApi.Domain.Entities;

namespace InventoryApi.Config;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<CreateProductDto, Product>().ReverseMap();
        CreateMap<CreateBatchDto, Batch>().ReverseMap();
        CreateMap<Batch, BatchDto>().ReverseMap();
        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<InventoryMovement, InventoryMovementDto>().ReverseMap();
    }
}
