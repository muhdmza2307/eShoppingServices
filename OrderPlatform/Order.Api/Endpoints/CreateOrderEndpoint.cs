using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Order.Mapping;
using Order.Models;
using Order.Services.Interfaces;

namespace Order.Api.Endpoints;

[HttpPost("api/orders"), AllowAnonymous]
public class CreateOrderEndpoint : Endpoint<CreateOrderRequest, CreateOrderResponse>
{
    private readonly IOrderService _orderService;

    public CreateOrderEndpoint(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public override async Task HandleAsync(CreateOrderRequest req, CancellationToken ct)
    {
        var order = req.ToOrder();
        
        var result = await _orderService.CreateAsync(order);
        
        await SendOkAsync(new CreateOrderResponse 
            {
                Id = result.Id
            }
        , cancellation: ct);
    }
}