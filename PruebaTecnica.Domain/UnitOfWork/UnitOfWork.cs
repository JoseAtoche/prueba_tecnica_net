using System.Data;
using Microsoft.Data.SqlClient;
using PruebaTecnica.Domain.Repositories;

namespace PruebaTecnica.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbConnection _connection;
        private IDbTransaction _transaction;
        private bool _disposed;
        public UnitOfWork(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public async void CommitAsync()
        {
            try
            {
                _transaction?.Commit();
                await Task.CompletedTask;
            }
            catch
            {
                Rollback();
                throw;
            }
        }

        public void Rollback()
        {
            _transaction?.Rollback();
        }

        public void Dispose()
        {
              if (_disposed)
                return;

            _transaction?.Dispose();
            _connection?.Close();
            _connection?.Dispose();

            _disposed = true;
        }
    }
}
