using FastEndpoints;
using Order.Common.Enums;
using Order.Mapping;
using Order.Models;
using Order.Services.Interfaces;

namespace Order.Api.Endpoints;

public class CreateOrderEndpoint : Endpoint<CreateOrderRequest, CreateOrderResponse>
{
    private readonly IOrderService _orderService;

    public CreateOrderEndpoint(IOrderService orderService)
    {
        _orderService = orderService;
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
        
        var result = await _orderService.CreateAsync(order);
        
        await SendOkAsync(new CreateOrderResponse 
            {
                Id = result.Id
            }
        , cancellation: ct);
    }
}