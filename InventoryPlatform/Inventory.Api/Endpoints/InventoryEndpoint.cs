using Carter;
using Inventory.Handlers.Inventories.Commands;
using Inventory.Handlers.Inventories.Queries;
using Inventory.Handlers.Inventories.Requests;
using MediatR;

namespace Inventory.Api.Endpoints;

public class InventoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/inventories", async (CreateInventoryRequest request, ISender sender) =>
        {
            var command = new CreateInventoryCommand(request.ProductRefNo, request.StockCount);
            var result = await sender.Send(command);
            return Results.Created($"/inventories/{result.Id}", result); 
        });
        
        app.MapGet("/inventories/{id:guid}", async (Guid id, ISender sender) =>
        {
            var query = new GetInventoryByIdQuery(id);
            
            var result = await sender.Send(query);
            
            return result != null ? Results.Ok(result) : Results.NotFound();
        });
    }
}