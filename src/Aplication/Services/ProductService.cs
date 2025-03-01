using AutoMapper;
using InventoryApi.API.Dto;
using InventoryApi.Aplication.Dto;
using InventoryApi.Aplication.Services.Interfaces;
using InventoryApi.Domain.Entities;
using InventoryApi.Infrastructure.Repositories.Interfaces;

namespace InventoryApi.Aplication.Services;

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
        var currentTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        foreach (var product in products)
        {
            var productWithBatches = await _repository.GetByIdWithBatchesAsync(product.Id);
            var productDto = _mapper.Map<ProductDto>(productWithBatches);

            productDto.Batches = productWithBatches!.Batches
                .Where(b => b.ExpirationDate >= currentTime)
                .Select(_mapper.Map<BatchDto>)
                .ToList();

            productDto.TotalStock = productDto.Batches.Sum(b => b.Stock);
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
        var currentTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        productDto.Batches = product.Batches
            .Where(b => b.ExpirationDate >= currentTime)
            .Select(_mapper.Map<BatchDto>)
            .ToList();

        productDto.TotalStock = productDto.Batches.Sum(b => b.Stock);

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
        var currentTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        foreach (var product in products)
        {
            var productWithBatches = await _repository.GetByIdWithBatchesAsync(product.Id);
            var productDto = _mapper.Map<ProductDto>(productWithBatches);

            productDto.Batches = productWithBatches!.Batches
                .Where(b => b.ExpirationDate >= currentTime)
                .Select(_mapper.Map<BatchDto>)
                .ToList();

            productDto.TotalStock = productDto.Batches.Sum(b => b.Stock);
            productDtos.Add(productDto);
        }

        return productDtos;
    }
}
