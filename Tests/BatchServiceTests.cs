using AutoMapper;
using InventoryApi.API.Dto;
using InventoryApi.Aplication.Dto;
using InventoryApi.Aplication.Services;
using InventoryApi.Aplication.Services.Interfaces;
using InventoryApi.Domain.Entities;
using InventoryApi.Infrastructure.Repositories.Interfaces;
using Moq;
using Xunit;

namespace InventoryApi.Tests;

public class BatchServiceTests
{
    private readonly Mock<IBatchRepository> _mockBatchRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly IBatchService _batchService;

    public BatchServiceTests()
    {
        _mockBatchRepository = new Mock<IBatchRepository>();
        _mockMapper = new Mock<IMapper>();
        _batchService = new BatchService(_mockBatchRepository.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task GetBatchesByProductIdAsync_ShouldReturnMappedBatches()
    {
        var productId = Guid.NewGuid();
        var batches = new List<Batch>
        {
            new Batch { Id = Guid.NewGuid(), ProductId = productId, Stock = 10, EntryDate = 123456789, ExpirationDate = 987654321 },
            new Batch { Id = Guid.NewGuid(), ProductId = productId, Stock = 20, EntryDate = 234567890, ExpirationDate = 876543210 }
        };

        var batchDtos = batches.Select(b => new BatchDto
        {
            Stock = b.Stock,
            EntryDate = b.EntryDate,
            ExpirationDate = b.ExpirationDate
        }).ToList();

        _mockBatchRepository.Setup(repo => repo.GetByProductIdAsync(productId)).ReturnsAsync(batches);
        _mockMapper.Setup(mapper => mapper.Map<IEnumerable<BatchDto>>(batches)).Returns(batchDtos);

        var result = await _batchService.GetBatchesByProductIdAsync(productId);

        Assert.NotNull(result);
        Assert.Equal(batchDtos.Count, result.Count());
        Assert.Equal(batchDtos.First().Stock, result.First().Stock);
    }

    [Fact]
    public async Task AddBatchAsync_ShouldAddBatch_WhenValidDataIsProvided()
    {
        var createBatchDto = new CreateBatchDto
        {
            ProductId = Guid.NewGuid(),
            Stock = 10,
            EntryDate = 123456789,
            ExpirationDate = 987654321
        };

        var batch = new Batch
        {
            Id = Guid.NewGuid(),
            ProductId = createBatchDto.ProductId,
            Stock = createBatchDto.Stock,
            EntryDate = createBatchDto.EntryDate,
            ExpirationDate = createBatchDto.ExpirationDate
        };

        _mockMapper.Setup(mapper => mapper.Map<Batch>(createBatchDto)).Returns(batch);

        await _batchService.AddBatchAsync(createBatchDto);

        _mockBatchRepository.Verify(repo => repo.AddAsync(batch), Times.Once);
    }

    [Fact]
    public async Task ClearExpiredBatchesAsync_ShouldCallClearExpiredBatchesAsyncOnRepository()
    {
        await _batchService.ClearExpiredBatchesAsync();

        _mockBatchRepository.Verify(repo => repo.ClearExpiredBatchesAsync(), Times.Once);
    }
}
