namespace Inventory.Data.Entities;

public class Inventory : EntityBase
{
    public string ProductRefNo { get; set; } = null!;
    public int StockCount { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }
}