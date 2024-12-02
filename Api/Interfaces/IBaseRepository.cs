namespace Api.Interfaces;

public interface IBaseRepository<TId, T>
{
    Task<List<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task<T> CreateAsync(T entity);
    Task<T?> UpdateAsync(TId id, T entity);
    Task<T?> DeleteAsync(TId id);
}