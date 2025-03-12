namespace Inventory.Repositories.Repositories.Interfaces;

public interface IUpsertRepository<TEntity>
{
    public Task<TEntity> UpsertAsync(TEntity entity);
}