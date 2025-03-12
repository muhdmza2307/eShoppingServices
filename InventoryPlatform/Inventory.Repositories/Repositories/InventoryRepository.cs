using Inventory.Data;
using Inventory.Repositories.Repositories.Interfaces;

namespace Inventory.Repositories.Repositories;

public class InventoryRepository : RepositoryBase<Data.Entities.Inventory>, 
    IInventoryRepository
{
    public InventoryRepository(IEntityStateManager entityStateManager, 
        IDataContext dbContext) 
        : base(entityStateManager, dbContext)
    {
    }
}