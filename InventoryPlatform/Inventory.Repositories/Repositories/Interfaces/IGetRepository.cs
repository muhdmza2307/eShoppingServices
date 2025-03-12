using Inventory.Data.Entities;

namespace Inventory.Repositories.Repositories.Interfaces;

public interface IGetRepository<TEntity> where TEntity : EntityBase
{
    public Task<TEntity?> GetAsync(Guid id);
}