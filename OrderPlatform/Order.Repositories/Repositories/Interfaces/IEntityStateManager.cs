using Order.Data.Entities;

namespace Order.Repositories.Repositories.Interfaces;

public interface IEntityStateManager
{
    void Detach<TEntity>(TEntity entity) where TEntity : EntityBase;
    void GenerateIdAndSetAsModified<TEntity>(TEntity entity) where TEntity : EntityBase;
    IEnumerable<TEntity> ChangeTrackerEntries<TEntity>() where TEntity : EntityBase;
}