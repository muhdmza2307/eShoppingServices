using Microsoft.EntityFrameworkCore;
using Order.Data;
using Order.Data.Entities;
using Order.Repositories.Repositories.Interfaces;

namespace Order.Repositories.Repositories;

public class EntityStateManager : IEntityStateManager
{
    IDataContext _dbContext;

    public EntityStateManager(IDataContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Detach<TEntity>(TEntity entity) where TEntity : EntityBase
    {
        var existingEntity = _dbContext.Entry(entity);
        existingEntity.State = EntityState.Detached;
    }

    public void GenerateIdAndSetAsModified<TEntity>(TEntity entity) where TEntity : EntityBase
    {
        var newEntity = _dbContext.Add(entity);
        newEntity.State = EntityState.Modified;
    }
    
    public IEnumerable<TEntity> ChangeTrackerEntries<TEntity>() where TEntity : EntityBase
    {
        return _dbContext.ChangeTracker.Entries<TEntity>()
            .Select(entry => entry.Entity);
    }
}