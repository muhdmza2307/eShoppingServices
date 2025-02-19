namespace Order.Models;

public class CreateOrderRequest
{
    public string CustomerName { get; set; } = null!;

    public string ProductRefNo { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
}