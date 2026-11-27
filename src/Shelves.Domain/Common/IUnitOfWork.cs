namespace Shelves.Domain.Common;

public interface IUnitOfWork
{
    bool IsInTransaction { get; }
    Task StartTransactionAsync();
    Task CommitTransactionAsync();
    Task RollBackTransactionAsync();
}
