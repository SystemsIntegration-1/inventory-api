using AutoMapper;
using inventory_api.src.DTOs;
using inventory_api.src.Models;
using InventoryApi.Dto;
using InventoryApi.Entities;
using InventoryApi.Repositories.Interfaces;
using InventoryApi.Services.Interfaces;

namespace InventoryApi.Services;

public class InventoryService : IInventoryService
{
    private readonly IInventoryRepository _repository;
    private readonly IBatchRepository _batchRepository;
    private readonly IMapper _mapper;

    public InventoryService(
        IInventoryRepository repository,
        IBatchRepository batchRepository,
        IMapper mapper)
    {
        _repository = repository;
        _batchRepository = batchRepository;
        _mapper = mapper;
    }

    public async Task<InventoryMovementResponseDto> RegisterInventoryMovementAsync(InventoryMovementDto movementDto)
    {
        NormalizeMovementData(movementDto);

        var validationResult = ValidateMovement(movementDto);
        if (!validationResult.IsValid)
        {
            return new InventoryMovementResponseDto
            {
                Success = false,
                Message = validationResult.ErrorMessage
            };
        }

        return await ProcessOutgoingMovementAsync(movementDto);
    }

    public async Task<IEnumerable<InventoryMovementDto>> GetMovementsByProductIdAsync(Guid productId)
    {
        var movements = await _repository.GetMovementsByProductIdAsync(productId);
        return _mapper.Map<IEnumerable<InventoryMovementDto>>(movements);
    }

    public async Task<IEnumerable<InventoryMovementDto>> GetAllMovementsAsync()
    {
        var movements = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<InventoryMovementDto>>(movements);
    }

    private void NormalizeMovementData(InventoryMovementDto movementDto)
    {
        if (movementDto.MovementDate <= 0)
        {
            movementDto.MovementDate = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        }

        movementDto.MovementType = movementDto.MovementType.ToLower();
    }

    private ValidationResult ValidateMovement(InventoryMovementDto movementDto)
    {
        var result = new ValidationResult { IsValid = true };

        if (movementDto.MovementType != "outgoing")
        {
            result.IsValid = false;
            result.ErrorMessage = "MovementType must be 'outgoing'";
            return result;
        }

        if (movementDto.Quantity <= 0)
        {
            result.IsValid = false;
            result.ErrorMessage = "Quantity must be greater than zero";
            return result;
        }

        if (movementDto.ProductId == Guid.Empty)
        {
            result.IsValid = false;
            result.ErrorMessage = "ProductId is required";
            return result;
        }

        return result;
    }

    private async Task<InventoryMovementResponseDto> ProcessOutgoingMovementAsync(InventoryMovementDto movementDto)
    {
        var allocationResult = await VerifyAndAllocateStockAsync(movementDto.ProductId, movementDto.Quantity);

        if (!allocationResult.Success)
        {
            var failedMovement = CreateMovementEntity(movementDto, true);
            await _repository.AddAsync(failedMovement);

            return new InventoryMovementResponseDto
            {
                Success = false,
                Message = "Insufficient stock available",
                MovementId = failedMovement.Id
            };
        }

        await UpdateBatchesStockAsync(allocationResult.Allocations);

        var movement = CreateMovementEntity(movementDto);
        await _repository.AddAsync(movement);

        return CreateSuccessResponse(movement.Id);
    }

    private InventoryMovement CreateMovementEntity(InventoryMovementDto movementDto, bool isFailed = false)
    {
        return new InventoryMovement
        {
            Id = Guid.NewGuid(),
            ProductId = movementDto.ProductId,
            MovementType = isFailed ? $"{movementDto.MovementType} (failed)" : movementDto.MovementType,
            Quantity = movementDto.Quantity,
            MovementDate = movementDto.MovementDate,
            Origin = movementDto.Origin,
            Destination = movementDto.Destination
        };
    }

    private InventoryMovementResponseDto CreateSuccessResponse(Guid movementId)
    {
        return new InventoryMovementResponseDto
        {
            Success = true,
            Message = "Movement processed successfully",
            MovementId = movementId
        };
    }

    private async Task<StockAllocationResult> VerifyAndAllocateStockAsync(Guid productId, int quantityNeeded)
    {
        var result = new StockAllocationResult();
        
        var batches = await _batchRepository.GetByProductIdAsync(productId);
        var currentTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        var validBatches = GetValidBatches(batches, currentTime);

        int totalAvailable = validBatches.Sum(b => b.Stock);
        if (totalAvailable < quantityNeeded)
        {
            result.Success = false;
            return result;
        }

        result.Success = true;
        result.Allocations = AllocateStockFromBatches(validBatches, quantityNeeded);

        return result;
    }

    private List<Batch> GetValidBatches(IEnumerable<Batch> batches, long currentTime)
    {
        return batches
            .Where(b => b.ExpirationDate > currentTime && b.Stock > 0)
            .OrderBy(b => b.ExpirationDate)
            .ToList();
    }

    private List<BatchAllocation> AllocateStockFromBatches(List<Batch> validBatches, int quantityNeeded)
    {
        var allocations = new List<BatchAllocation>();
        int remaining = quantityNeeded;

        foreach (var batch in validBatches)
        {
            if (remaining <= 0) break;

            int toTake = Math.Min(batch.Stock, remaining);
            allocations.Add(new BatchAllocation 
            { 
                Batch = batch, 
                QuantityToTake = toTake 
            });
            remaining -= toTake;
        }

        return allocations;
    }

    private async Task UpdateBatchesStockAsync(List<BatchAllocation> allocations)
    {
        foreach (var allocation in allocations)
        {
            allocation.Batch.Stock -= allocation.QuantityToTake;
            await _batchRepository.UpdateAsync(allocation.Batch);
        }
    }
}