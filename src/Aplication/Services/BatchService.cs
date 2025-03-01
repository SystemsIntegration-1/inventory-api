using AutoMapper;
using inventory_api.Aplication.DTOs;
using InventoryApi.API.Dto;
using InventoryApi.Aplication.Dto;
using InventoryApi.Aplication.Services.Interfaces;
using InventoryApi.Domain.Entities;
using InventoryApi.Infrastructure.Repositories.Interfaces;

namespace InventoryApi.Aplication.Services;

public class BatchService(IBatchRepository batchRepository, IMapper mapper) : IBatchService
{
    private readonly IBatchRepository _batchRepository = batchRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<BatchDto>> GetAllBatchesAsync() =>
        _mapper.Map<IEnumerable<BatchDto>>(await _batchRepository.GetAllAsync());

    public async Task<BatchDto?> GetBatchByIdAsync(Guid id) =>
        _mapper.Map<BatchDto>(await _batchRepository.GetByIdAsync(id));

    public async Task<IEnumerable<BatchDto>> GetBatchesByProductIdAsync(Guid productId) =>
        _mapper.Map<IEnumerable<BatchDto>>(await _batchRepository.GetByProductIdAsync(productId));

    public async Task AddBatchAsync(CreateBatchDto batchDto)
    {
        if (batchDto.Stock <= 0)
            throw new ArgumentException("Batch stock cannot be 0");

        var batch = _mapper.Map<Batch>(batchDto);
        await _batchRepository.AddAsync(batch);
    }

    public async Task UpdateBatchAsync(Guid id, BatchDto batchDto)
    {
        var existingBatch = await _batchRepository.GetByIdAsync(id);
        if (existingBatch == null)
            throw new KeyNotFoundException("Batch not found");

        _mapper.Map(batchDto, existingBatch);
        await _batchRepository.UpdateAsync(existingBatch);
    }

    public async Task<IEnumerable<ExpiredBatchDto>> GetExpiredBatchesAsync()
    {
        var expiredBatchesWithProduct = await _batchRepository.GetExpiredBatchesAsync();

        var expiredBatchDtos = expiredBatchesWithProduct.Select(batchProductPair => new ExpiredBatchDto
        {
            BatchId = batchProductPair.Batch.Id,
            ProductId = batchProductPair.Batch.ProductId,
            ProductName = batchProductPair.Product.Name,
            ProductCategory = batchProductPair.Product.Category,
            Stock = batchProductPair.Batch.Stock,
            EntryDate = batchProductPair.Batch.EntryDate,
            ExpirationDate = batchProductPair.Batch.ExpirationDate
        }).ToList();

        return expiredBatchDtos;
    }

    public async Task ClearExpiredBatchesAsync()
    {
        await _batchRepository.ClearExpiredBatchesAsync();
    }
}
