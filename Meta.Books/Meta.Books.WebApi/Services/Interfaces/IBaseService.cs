using Meta.Books.WebApi.Dto;

namespace Meta.Books.WebApi.Services.Interfaces;

public interface IBaseService<T> where T : BaseDto
{
    Task<T> SaveAsync(T entity);
    
    Task<T> UpdateAsync(T entity);
    
    Task<List<T>> GetAllAsync();
    
    Task<bool> DeleteAsync(int id);
    
    Task<T> GetById(int id);
}