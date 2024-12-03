using Api.Models;

namespace Api.Interfaces;

public interface IStockRepository : IBaseRepository<int, Stock>
{
    Task<bool> IsExistAsync(int id);
}