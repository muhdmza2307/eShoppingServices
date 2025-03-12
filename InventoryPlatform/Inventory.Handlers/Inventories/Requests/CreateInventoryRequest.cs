namespace Inventory.Handlers.Inventories.Requests;

public class CreateInventoryRequest
{
    public string ProductRefNo { get; set; } = null!;
    public int StockCount { get; set; }
}