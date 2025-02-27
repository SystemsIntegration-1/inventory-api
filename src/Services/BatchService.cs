using AutoMapper;
using inventory_api.src.DTOs;
using InventoryApi.Dto;
using InventoryApi.Entities;
using InventoryApi.Repositories.Interfaces;
using InventoryApi.Services.Interfaces;

namespace InventoryApi.Services;

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

        var expiredBatchDtos = expiredBatchesWithProduct.Select(bp => new ExpiredBatchDto
        {
            BatchId = bp.Item1.Id,
            ProductId = bp.Item1.ProductId,
            ProductName = bp.Item2.Name,
            ProductCategory = bp.Item2.Category,
            Stock = bp.Item1.Stock,
            EntryDate = bp.Item1.EntryDate,
            ExpirationDate = bp.Item1.ExpirationDate
        }).ToList();

        return expiredBatchDtos;
    }

    public async Task ClearExpiredBatchesAsync()
    {
        await _batchRepository.ClearExpiredBatchesAsync();
    }
}
