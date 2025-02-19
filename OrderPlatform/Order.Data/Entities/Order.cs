using Order.Common.Enums;

namespace Order.Data.Entities;

public class Order : EntityBase
{
    public string CustomerName { get; set; } = null!;
    public string ProductRefNo { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public OrderState StatusId { get; set; }
    public DateTime? PublishedOnUtc { get; set; }
}