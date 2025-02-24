using AutoMapper;
using InventoryApi.Dto;
using InventoryApi.Entities;
using InventoryApi.Repositories.Interfaces;
using InventoryApi.Services.Interfaces;

namespace InventoryApi.Services;
public class InventoryService : IInventoryService
{
    private readonly IInventoryRepository _repository;
    private readonly IMapper _mapper;

    public InventoryService(IInventoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task RegisterInventoryMovementAsync(InventoryMovementDto movementDto)
    {
        var movement = new InventoryMovement
        {
            Id = Guid.NewGuid(),
            ProductId = movementDto.ProductId,
            MovementType = movementDto.MovementType,
            Quantity = movementDto.Quantity,
            MovementDate = movementDto.MovementDate,
            Origin = movementDto.Origin,
            Destination = movementDto.Destination
        };
        await _repository.AddAsync(movement);
    }

    public async Task<IEnumerable<InventoryMovementDto>> GetMovementsByProductIdAsync(Guid productId)
    {
        var movements = await _repository.GetMovementsByProductIdAsync(productId);
        return _mapper.Map<IEnumerable<InventoryMovementDto>>(movements);
    }
}
