using Meta.Books.Core.Http;
using Meta.Books.WebApi.Dto;

namespace Meta.Books.WebSite.Services.Interfaces;

public interface IBaseService<T> 
{
    Task<Response<List<T>>> GetAllAsync();

    Task<Response<T>> GetById(int id);
    
    Task<Response<T>> SaveAsync(T entityDto);

    Task<Response<T>> UpdateAsync(T entityDto);

    Task<Response<bool>> DeleteAsync(int id);   
}