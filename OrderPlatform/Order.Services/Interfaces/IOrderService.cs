namespace Order.Services.Interfaces;

public interface IOrderService
{
    Task<Data.Entities.Order> CreateAsync(Data.Entities.Order order);
}