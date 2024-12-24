using PruebaTecnica.Domain.Entities;

namespace PruebaTecnica.Domain.Repositories;

public interface IBankRepository
{
    public Task<BankEntity> FindByBicAsync(string BIC);
    public Task<IEnumerable<BankEntity>> FindAllAsync();
    public Task AddAsync(BankEntity entities);

}
