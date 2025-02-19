using Order.Common.Enums;

namespace Order.Data.Entities;

public class OrderStatus
{
    public OrderState Id { get; set; }
    public string Description { get; set; } = null!;
}