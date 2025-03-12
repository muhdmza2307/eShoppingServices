using Inventory.Data;
using Inventory.Data.Entities;
using Inventory.Repositories.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Repositories.Repositories;

public class EntityStateManager : IEntityStateManager
{
    private readonly IDataContext _dbContext;

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