using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Shelves.Domain.Common;

namespace Shelves.Domain.Persistence;

public class ShelvesDbContext : DbContext, IUnitOfWork
{
    private IDbContextTransaction? _dbContextTransaction;

    public ShelvesDbContext(DbContextOptions<ShelvesDbContext> options) : base(options)
    {
    }

    public DbSet<Client> Clients { get; set; } = null!;
    public DbSet<Book> Books { get; set; } = null!;
    public DbSet<Reservation> Reservations { get; set; } = null!;
    public DbSet<Author> Authors { get; set; } = null!;


    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }


    // Derive transaction state directly from the active transaction instance to avoid state drift.
    public bool IsInTransaction => _dbContextTransaction is not null;

    public async Task StartTransactionAsync()
    {
        if (_dbContextTransaction is not null)
        {
            return;
        }

        _dbContextTransaction = await Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        if (_dbContextTransaction is null)
        {
            throw new InvalidOperationException("No active transaction to commit.");
        }

        try
        {
            await SaveChangesAsync(cancellationToken);
            await _dbContextTransaction.CommitAsync();
        }
        catch
        {
            await RollBackTransactionAsync();
            throw;
        }
        finally
        {
            await DisposeTransactionAsync();
        }
    }

    public async Task RollBackTransactionAsync()
    {
        if (_dbContextTransaction is null)
        {
            return;
        }

        try
        {
            await _dbContextTransaction.RollbackAsync();
        }
        finally
        {
            await DisposeTransactionAsync();
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShelvesDbContext).Assembly);
    }

    public override async ValueTask DisposeAsync()
    {
        if (IsInTransaction)
            await DisposeTransactionAsync();
        await base.DisposeAsync();
    }

    public override void Dispose()
    {
        if (IsInTransaction)
        {
            _dbContextTransaction?.Dispose();
            _dbContextTransaction = null;
        }

        base.Dispose();
    }

    private async Task DisposeTransactionAsync()
    {
        if (_dbContextTransaction is not null)
        {
            await _dbContextTransaction.DisposeAsync();
            _dbContextTransaction = null;
        }
    }
}