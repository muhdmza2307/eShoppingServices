using Inventory.Common.Options;
using Inventory.Data.Extensions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Data;

public class DataContext : DbContext, IDataContext
{
    private SqlServerOption? _sqlServerOption;

    public DataContext(SqlServerOption? sqlServerOption = null)
    {
        _sqlServerOption = sqlServerOption;
    }
    
    public DbSet<Entities.Inventory> Inventories { get; set; } = null!;

    private bool IsUnitOfWorkActive { get; set; }
    
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        IsUnitOfWorkActive 
            ? Task.FromResult(0) 
            : base.SaveChangesAsync(cancellationToken);
    
    public void ActivateUnitOfWork() => IsUnitOfWorkActive = true;

    public void DeactivateUnitOfWork() => IsUnitOfWorkActive = false;
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        _sqlServerOption ??= new SqlServerOption
        {
            ServerName = ".",
            DatabaseName = "api-inventory",
            UseIntegratedSecurity = true
        };

        var sqlConnectionStringBuilder = new SqlConnectionStringBuilder
        {
            DataSource = _sqlServerOption.ServerName,
            InitialCatalog = _sqlServerOption.DatabaseName,
            TrustServerCertificate = true
        };

        if (_sqlServerOption.UseIntegratedSecurity)
        {
            sqlConnectionStringBuilder.IntegratedSecurity = _sqlServerOption.UseIntegratedSecurity;
        }
        else
        {
            sqlConnectionStringBuilder.UserID = _sqlServerOption.Username;
            sqlConnectionStringBuilder.Password = _sqlServerOption.Password;
        }
        
        optionsBuilder.UseSqlServer(sqlConnectionStringBuilder.ConnectionString);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyAllConfigurations();
    }
}