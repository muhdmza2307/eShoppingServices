using Inventory.Common.Configurations;
using Inventory.Data;
using Inventory.Data.Entities;
using Inventory.Repositories.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Polly;

namespace Inventory.Repositories.Repositories;

public class RepositoryBase<TEntity> :
    IGetRepository<TEntity>,
    IUpsertRepository<TEntity> 
    where TEntity : EntityBase
{
    private readonly IEntityStateManager _entityStateManager;
    private readonly IDataContext _dbContext;
    private DbSet<TEntity> DbContextField => _dbContext.Set<TEntity>();

    protected RepositoryBase(IEntityStateManager entityStateManager, 
        IDataContext dbContext)
    {
        _entityStateManager = entityStateManager;
        _dbContext = dbContext;
    }

    public async Task<TEntity?> GetAsync(Guid id) =>
        await Policy<TEntity?>
            .Handle<Exception>()
            .WaitAndRetryAsync(PollyConfigurations.ForOneSecondIncreasesUpToThree())
            .ExecuteAsync(async () => (await DbContextField.FindAsync(id).ConfigureAwait(false))!);

    public async Task<TEntity> UpsertAsync(TEntity entity)
    {
        await AddEntityState(entity);
        await SaveChangesAsync();

        return entity;
    }
    
    private async Task AddEntityState(TEntity entity)
    {
        entity.Id = entity.Id != Guid.Empty ? entity.Id : Guid.NewGuid();
        entity.CreatedOnUtc = DateTime.Now;
        await DbContextField.AddAsync(entity).ConfigureAwait(false);
    }
    
    private async Task SaveChangesAsync()
    {
        await Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(PollyConfigurations.ForOneSecondIncreasesUpToThree())
            .ExecuteAsync(async () => await _dbContext.SaveChangesAsync().ConfigureAwait(false));
    }
}