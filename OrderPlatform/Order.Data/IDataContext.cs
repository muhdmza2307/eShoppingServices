using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Order.Data;

public interface IDataContext
{
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    EntityEntry Entry(object entity);
    EntityEntry Add(object entity);
    ChangeTracker ChangeTracker { get; }
    void ActivateUnitOfWork();
    void DeactivateUnitOfWork();
}