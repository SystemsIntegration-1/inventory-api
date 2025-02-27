using AutoMapper;
using InventoryApi.Dto;
using InventoryApi.Entities;

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
