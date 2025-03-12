namespace Inventory.Handlers.Inventories.Responses;

public record InventoryResponse(Guid Id, string ProductRefNo, int StockCount);
