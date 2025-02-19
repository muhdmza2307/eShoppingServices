using Order.Repositories.Repositories.Interfaces;
using Order.Services.Interfaces;

namespace Order.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public Task<Data.Entities.Order> CreateAsync(Data.Entities.Order order) =>
        _orderRepository.UpsertAsync(order);
}