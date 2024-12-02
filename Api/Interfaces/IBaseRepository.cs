namespace Api.Interfaces;

public interface IBaseRepository<TId, T>
{
    Task<List<T>> GetAll();
    Task<T?> GetById(int id);
    Task<T> Create(T entity);
    Task<T?> Update(TId id, T entity);
    Task<T?> Delete(TId id);
}