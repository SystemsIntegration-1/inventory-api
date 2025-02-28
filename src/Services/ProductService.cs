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

    public async Task<IEnumerable<ProductDto>> GetProductsAsync()
    {
        var products = await _repository.GetAllAsync();
        var productDtos = new List<ProductDto>();

        foreach (var product in products)
        {
            var productWithBatches = await _repository.GetByIdWithBatchesAsync(product.Id);
            var productDto = _mapper.Map<ProductDto>(productWithBatches);
            productDto.TotalStock = productWithBatches!.Batches.Sum(b => b.Stock);
            productDtos.Add(productDto);
        }

        return productDtos;
    }

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
        product.SharedId = Guid.NewGuid();
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

    public async Task<IEnumerable<ProductDto>> SearchProductsAsync(string name)
    {
        var products = await _repository.SearchProductsAsync(name);
        var productDtos = new List<ProductDto>();

        foreach (var product in products)
        {
            var productWithBatches = await _repository.GetByIdWithBatchesAsync(product.Id);
            var productDto = _mapper.Map<ProductDto>(productWithBatches);
            productDto.TotalStock = productWithBatches!.Batches.Sum(b => b.Stock);
            productDtos.Add(productDto);
        }

        return productDtos;
    }
}
