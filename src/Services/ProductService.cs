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

    public async Task<ProductDto> GetProductByIdAsync(Guid id)
    {
        var product = await _repository.GetByIdWithBatchesAsync(id);
        if (product == null)
            return null!;

        var productDto = _mapper.Map<ProductDto>(product);
        productDto.TotalStock = product.Batches.Sum(b => b.Stock);

        return productDto;
    }

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

    public async Task AddProductBatchAsync(BatchDto batchProduct)
    {
        var batch = _mapper.Map<Batch>(batchProduct);
        await _repository.AddBatchAsync(batch);
    }
}
