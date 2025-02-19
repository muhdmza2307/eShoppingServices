using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Order.Common.Options;
using Order.Data.Extensions;

namespace Order.Data;

public class DataContext : DbContext, IDataContext
{
    private SqlServerOption? _sqlServerOption;
    
    public DbSet<Entities.Order> Orders { get; set; } = null!;
    
    private bool IsUnitOfWorkActive { get; set; }
    
    public void ActivateUnitOfWork() => IsUnitOfWorkActive = true;
    
    public void DeactivateUnitOfWork() => IsUnitOfWorkActive = false;

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        IsUnitOfWorkActive 
            ? Task.FromResult(0) 
            : base.SaveChangesAsync(cancellationToken);
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        _sqlServerOption ??= new SqlServerOption
        {
            ServerName = ".",
            DatabaseName = "api-order",
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