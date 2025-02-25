using AutoMapper;
using InventoryApi.Dto;
using InventoryApi.Entities;
using InventoryApi.Repositories.Interfaces;
using InventoryApi.Services.Interfaces;
namespace InventoryApi.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductDto>> GetProductsAsync() =>
        _mapper.Map<IEnumerable<ProductDto>>(await _repository.GetAllAsync());

    public async Task<ProductDto> GetProductByIdAsync(Guid id) =>
        _mapper.Map<ProductDto>(await _repository.GetByIdAsync(id));

    public async Task AddProductAsync(CreateProductDto productDto)
    {
        var product = _mapper.Map<Product>(productDto);
        await _repository.AddAsync(product);
    }

    public async Task UpdateProductAsync(Guid id, CreateProductDto productDto)
    {
        var existingProduct = await _repository.GetByIdAsync(id);
        if (existingProduct == null)
            throw new KeyNotFoundException("Product not found");

        _mapper.Map(productDto, existingProduct);

        await _repository.UpdateAsync(existingProduct);
    }

    public async Task<IEnumerable<ProductDto>> SearchProductsAsync(string name) =>
        _mapper.Map<IEnumerable<ProductDto>>(await _repository.SearchProductsAsync(name));
}
