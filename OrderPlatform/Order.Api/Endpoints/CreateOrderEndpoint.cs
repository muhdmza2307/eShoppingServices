using FastEndpoints;
using Order.Common.Enums;
using Order.Mapping;
using Order.Models;
using Order.Repositories.Repositories.Interfaces;

namespace Order.Api.Endpoints;

public class CreateOrderEndpoint : Endpoint<CreateOrderRequest, CreateOrderResponse>
{
    private readonly IOrderRepository _orderRepository;

    public CreateOrderEndpoint(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    
    public override void Configure()
    {
        Post("/api/orders");
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Create an order";
            s.Description = "Create an order details with product purchases";
            s.Response<CreateOrderResponse>(200, "Order successfully created");
            s.Response(400, "Bad request - invalid input");
            s.Response(500, "Internal server error");
        });
    }

    public override async Task HandleAsync(CreateOrderRequest req, CancellationToken ct)
    {
        var order = req.ToOrder();
        order.StatusId = OrderState.Pending;
        
        var result = await _orderRepository.UpsertAsync(order);
        
        await SendOkAsync(new CreateOrderResponse 
            {
                Id = result.Id
            }
        , cancellation: ct);
    }
}