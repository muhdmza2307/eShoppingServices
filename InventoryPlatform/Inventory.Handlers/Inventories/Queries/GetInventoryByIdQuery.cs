using Inventory.Handlers.Inventories.Responses;
using MediatR;

namespace Inventory.Handlers.Inventories.Queries;

public record GetInventoryByIdQuery(Guid Id) : IRequest<InventoryResponse?>;