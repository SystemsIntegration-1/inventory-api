using AutoMapper;
using inventory_api.API.Models;
using InventoryApi.Aplication.Dto;
using InventoryApi.Aplication.Services;
using InventoryApi.Aplication.Services.Interfaces;
using InventoryApi.Domain.Entities;
using InventoryApi.Infrastructure.Repositories.Interfaces;
using Moq;
using Xunit;

namespace InventoryApi.Tests;

public class InventoryServiceTests
{
    private readonly Mock<IInventoryRepository> _mockInventoryRepository;
    private readonly Mock<IBatchRepository> _mockBatchRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly IInventoryService _inventoryService;

    public InventoryServiceTests()
    {
        _mockInventoryRepository = new Mock<IInventoryRepository>();
        _mockBatchRepository = new Mock<IBatchRepository>();
        _mockMapper = new Mock<IMapper>();
        _inventoryService = new InventoryService(
            _mockInventoryRepository.Object,
            _mockBatchRepository.Object,
            _mockMapper.Object);
    }

    [Fact]
    public async Task RegisterInventoryMovementAsync_ShouldReturnSuccessResponse_WhenStockIsSufficient()
    {
        var movementDto = new InventoryMovementDto
        {
            ProductId = Guid.NewGuid(),
            MovementType = "outgoing",
            Quantity = 5,
            Origin = "Warehouse A",
            Destination = "Customer B"
        };

        var batches = new List<Batch>
        {
            new Batch { Id = Guid.NewGuid(), Stock = 3, ExpirationDate = DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeMilliseconds() },
            new Batch { Id = Guid.NewGuid(), Stock = 4, ExpirationDate = DateTimeOffset.UtcNow.AddHours(2).ToUnixTimeMilliseconds() }
        };

        var allocations = new List<BatchAllocation>
        {
            new BatchAllocation { Batch = batches[0], QuantityToTake = 3 },
            new BatchAllocation { Batch = batches[1], QuantityToTake = 2 }
        };

        _mockBatchRepository.Setup(repo => repo.GetByProductIdAsync(movementDto.ProductId)).ReturnsAsync(batches);

        var result = await _inventoryService.RegisterInventoryMovementAsync(movementDto);

        Assert.True(result.Success);
        Assert.Equal("Movement processed successfully", result.Message);
        _mockBatchRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Batch>()), Times.Exactly(2));
    }

    [Fact]
    public async Task RegisterInventoryMovementAsync_ShouldReturnFailureResponse_WhenStockIsInsufficient()
    {
        var movementDto = new InventoryMovementDto
        {
            ProductId = Guid.NewGuid(),
            MovementType = "outgoing",
            Quantity = 10,
            Origin = "Warehouse A",
            Destination = "Customer B"
        };

        var batches = new List<Batch>
        {
            new Batch { Id = Guid.NewGuid(), Stock = 3, ExpirationDate = DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeMilliseconds() },
            new Batch { Id = Guid.NewGuid(), Stock = 4, ExpirationDate = DateTimeOffset.UtcNow.AddHours(2).ToUnixTimeMilliseconds() }
        };

        _mockBatchRepository.Setup(repo => repo.GetByProductIdAsync(movementDto.ProductId)).ReturnsAsync(batches);

        var result = await _inventoryService.RegisterInventoryMovementAsync(movementDto);

        Assert.False(result.Success);
        Assert.Equal("Insufficient stock available", result.Message);
        _mockInventoryRepository.Verify(repo => repo.AddAsync(It.IsAny<InventoryMovement>()), Times.Once);
    }

    [Fact]
    public async Task GetMovementsByProductIdAsync_ShouldReturnMappedMovements()
    {
        var productId = Guid.NewGuid();
        var movements = new List<InventoryMovement>
        {
            new InventoryMovement
            {
                Id = Guid.NewGuid(),
                ProductId = productId,
                MovementType = "outgoing",
                Quantity = 5,
                MovementDate = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                Origin = "Warehouse A",
                Destination = "Customer B"
            },
            new InventoryMovement
            {
                Id = Guid.NewGuid(),
                ProductId = productId,
                MovementType = "incoming",
                Quantity = 3,
                MovementDate = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                Origin = "Supplier C",
                Destination = "Warehouse A"
            }
        };

        var movementDtos = movements.Select(m => new InventoryMovementDto
        {
            ProductId = m.ProductId,
            MovementType = m.MovementType,
            Quantity = m.Quantity,
            MovementDate = m.MovementDate,
            Origin = m.Origin,
            Destination = m.Destination
        }).ToList();

        _mockInventoryRepository.Setup(repo => repo.GetMovementsByProductIdAsync(productId)).ReturnsAsync(movements);
        _mockMapper.Setup(mapper => mapper.Map<IEnumerable<InventoryMovementDto>>(movements)).Returns(movementDtos);

        var result = await _inventoryService.GetMovementsByProductIdAsync(productId);

        Assert.NotNull(result);
        Assert.Equal(movementDtos.Count, result.Count());
        Assert.Equal(movementDtos.First().MovementType, result.First().MovementType);
        Assert.Equal(movementDtos.Last().Destination, result.Last().Destination);
    }

    [Fact]
    public async Task GetAllMovementsAsync_ShouldReturnMappedMovements()
    {
        var movements = new List<InventoryMovement>
        {
            new InventoryMovement
            {
                Id = Guid.NewGuid(),
                ProductId = Guid.NewGuid(),
                MovementType = "outgoing",
                Quantity = 5,
                MovementDate = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                Origin = "Warehouse A",
                Destination = "Customer B"
            },
            new InventoryMovement
            {
                Id = Guid.NewGuid(),
                ProductId = Guid.NewGuid(),
                MovementType = "incoming",
                Quantity = 3,
                MovementDate = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                Origin = "Supplier C",
                Destination = "Warehouse A"
            }
        };

        var movementDtos = movements.Select(m => new InventoryMovementDto
        {
            ProductId = m.ProductId,
            MovementType = m.MovementType,
            Quantity = m.Quantity,
            MovementDate = m.MovementDate,
            Origin = m.Origin,
            Destination = m.Destination
        }).ToList();

        _mockInventoryRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(movements);
        _mockMapper.Setup(mapper => mapper.Map<IEnumerable<InventoryMovementDto>>(movements)).Returns(movementDtos);

        var result = await _inventoryService.GetAllMovementsAsync();

        Assert.NotNull(result);
        Assert.Equal(movementDtos.Count, result.Count());
        Assert.Equal(movementDtos.First().MovementType, result.First().MovementType);
        Assert.Equal(movementDtos.Last().Destination, result.Last().Destination);
    }
}
