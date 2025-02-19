using Order.Data;
using Order.Repositories.Repositories.Interfaces;

namespace Order.Repositories.Repositories;

public class OrderRepository : RepositoryBase<Data.Entities.Order>, 
    IOrderRepository
{
    public OrderRepository(IEntityStateManager entityStateManager, 
        IDataContext dbContext) 
        : base(entityStateManager, dbContext)
    {
    }
}