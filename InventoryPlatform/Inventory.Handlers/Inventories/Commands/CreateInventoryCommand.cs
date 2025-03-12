using Inventory.Handlers.Inventories.Responses;
using MediatR;

namespace Inventory.Handlers.Inventories.Commands;

public record CreateInventoryCommand(string ProductRefNo, int StockCount) : IRequest<InventoryResponse>;