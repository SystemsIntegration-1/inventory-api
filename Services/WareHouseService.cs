public class WareHouseService : IInventoryService
{
    private readonly IProductRepository _repository;

    public WareHouseService(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<ProductDto>> GetProductsAsync() =>
        (await _repository.GetProductsAsync()).ConvertAll(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Category = p.Category,
            AvailableQuantity = p.AvailableQuantity,
            WarehouseLocation = p.WarehouseLocation,
            EntryDate = p.EntryDate,
            ExpirationDate = p.ExpirationDate
        });

    public async Task<ProductDto> GetProductByIdAsync(Guid id)
    {
        var product = await _repository.GetProductByIdAsync(id);
        return product == null
            ? null
            : new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Category = product.Category,
                AvailableQuantity = product.AvailableQuantity,
                WarehouseLocation = product.WarehouseLocation,
                EntryDate = product.EntryDate,
                ExpirationDate = product.ExpirationDate
            };
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
        await _repository.AddInventoryMovementAsync(movement);
    }

    public async Task<List<InventoryMovementDto>> GetMovementsByProductIdAsync(Guid productId) =>
        (await _repository.GetMovementsByProductIdAsync(productId)).ConvertAll(
            m => new InventoryMovementDto
            {
                ProductId = m.ProductId,
                MovementType = m.MovementType,
                Quantity = m.Quantity,
                MovementDate = m.MovementDate,
                Origin = m.Origin,
                Destination = m.Destination
            }
        );

    public async Task UpdateProductAsync(ProductDto productDto)
    {
        var product = new Product
        {
            Id = productDto.Id,
            Name = productDto.Name,
            Description = productDto.Description,
            Category = productDto.Category,
            AvailableQuantity = productDto.AvailableQuantity,
            WarehouseLocation = productDto.WarehouseLocation,
            EntryDate = productDto.EntryDate,
            ExpirationDate = productDto.ExpirationDate
        };
        await _repository.UpdateProductAsync(product);
    }

    public async Task<ProductDto> AddProductAsync(ProductDto productDto)
    {
        var product = new Product
        {
            Id = productDto.Id,
            Name = productDto.Name,
            Description = productDto.Description,
            Category = productDto.Category,
            AvailableQuantity = productDto.AvailableQuantity,
            WarehouseLocation = productDto.WarehouseLocation,
            EntryDate = productDto.EntryDate,
            ExpirationDate = productDto.ExpirationDate
        };
        await _repository.AddProductAsync(product);
        return productDto;
    }

    public async Task<List<ProductDto>> SearchProductsAsync(string name) =>
        (await _repository.SearchProductsAsync(name)).ConvertAll(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Category = p.Category,
            AvailableQuantity = p.AvailableQuantity,
            WarehouseLocation = p.WarehouseLocation,
            EntryDate = p.EntryDate,
            ExpirationDate = p.ExpirationDate
        });
}
