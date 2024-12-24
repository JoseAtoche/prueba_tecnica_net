namespace PruebaTecnica.Domain.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        void CommitAsync();
        void Rollback();
    }
}
