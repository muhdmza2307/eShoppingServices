namespace Inventory.Repositories.Repositories.Interfaces;

public interface IInventoryRepository :
    IGetRepository<Data.Entities.Inventory>,
    IUpsertRepository<Data.Entities.Inventory>
{
    
}