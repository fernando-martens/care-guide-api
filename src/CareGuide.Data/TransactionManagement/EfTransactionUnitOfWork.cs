using Microsoft.EntityFrameworkCore.Storage;

namespace CareGuide.Data.TransactionManagement
{
    public class EfTransactionUnitOfWork : IEfTransactionUnitOfWork
    {
        private readonly DatabaseContext _dbContext;
        private IDbContextTransaction? _transaction;

        public EfTransactionUnitOfWork(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            _transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
                await (_transaction?.CommitAsync(cancellationToken) ?? Task.CompletedTask);
            }
            catch
            {
                await RollbackTransactionAsync(cancellationToken);
                throw;
            }
            finally
            {
                await (_transaction?.DisposeAsync() ?? ValueTask.CompletedTask);
                _transaction = null;
            }
        }

        public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync(cancellationToken);
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _dbContext.Dispose();
        }
    }
}
