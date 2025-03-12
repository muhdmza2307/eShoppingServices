using Inventory.Data.Entities;

namespace Inventory.Repositories.Repositories.Interfaces;

public interface IEntityStateManager
{
    void Detach<TEntity>(TEntity entity) where TEntity : EntityBase;
    void GenerateIdAndSetAsModified<TEntity>(TEntity entity) where TEntity : EntityBase;
    IEnumerable<TEntity> ChangeTrackerEntries<TEntity>() where TEntity : EntityBase;
}