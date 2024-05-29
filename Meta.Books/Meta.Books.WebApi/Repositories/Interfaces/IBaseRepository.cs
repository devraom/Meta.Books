using Meta.Books.Core.Entities;

namespace Meta.Books.WebApi.Repositories.Interfaces;

public interface IBaseRepository<T> where T : BaseEntity
{
    Task<T> SaveAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<List<T>> GetAllAsync();
    Task<bool> DeleteAsync(int id);
    Task<T> GetById(int id);
}