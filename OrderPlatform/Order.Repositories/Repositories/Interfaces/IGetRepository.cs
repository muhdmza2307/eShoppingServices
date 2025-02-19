using Order.Data.Entities;

namespace Order.Repositories.Repositories.Interfaces;

public interface IGetRepository<TEntity> where TEntity : EntityBase
{
    public Task<TEntity?> GetAsync(Guid id);
}