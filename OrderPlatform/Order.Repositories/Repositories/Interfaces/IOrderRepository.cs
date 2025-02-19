namespace Order.Repositories.Repositories.Interfaces;

public interface IOrderRepository :
    IGetRepository<Data.Entities.Order>,
    IUpsertRepository<Data.Entities.Order>
{
    
}