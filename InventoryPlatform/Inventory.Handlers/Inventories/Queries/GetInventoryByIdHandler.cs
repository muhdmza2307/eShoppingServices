using Inventory.Handlers.Inventories.Responses;
using Inventory.Repositories.Repositories.Interfaces;
using MediatR;

namespace Inventory.Handlers.Inventories.Queries;

public class GetInventoryByIdHandler : IRequestHandler<GetInventoryByIdQuery, InventoryResponse?>
{
    private readonly IInventoryRepository _inventoryRepository;
    
    public GetInventoryByIdHandler(IInventoryRepository inventoryRepository)
    {
        _inventoryRepository = inventoryRepository;
    }
    
    public async Task<InventoryResponse?> Handle(GetInventoryByIdQuery request, CancellationToken cancellationToken)
    {
        var inventory = await _inventoryRepository.GetAsync(request.Id);
        
        return inventory != null ? 
            new InventoryResponse(inventory.Id, inventory.ProductRefNo, inventory.StockCount)
            : null;
    }
}