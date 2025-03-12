using Inventory.Handlers.Inventories.Responses;
using Inventory.Repositories.Repositories.Interfaces;
using MediatR;

namespace Inventory.Handlers.Inventories.Commands;

public class CreateInventoryHandler : IRequestHandler<CreateInventoryCommand, InventoryResponse>
{
    private readonly IInventoryRepository _inventoryRepository;
    
    public CreateInventoryHandler(IInventoryRepository inventoryRepository)
    {
        _inventoryRepository = inventoryRepository;
    }

    public async Task<InventoryResponse> Handle(CreateInventoryCommand request, CancellationToken cancellationToken)
    {
        var inventoryEntity = new Data.Entities.Inventory
        {
            ProductRefNo = request.ProductRefNo,
            StockCount = request.StockCount
        };
        
        var inventory = await _inventoryRepository.UpsertAsync(inventoryEntity);
        
        return new InventoryResponse(inventory.Id, inventory.ProductRefNo, inventory.StockCount);
    }
}