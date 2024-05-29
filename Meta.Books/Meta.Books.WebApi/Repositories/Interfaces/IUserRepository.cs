using Meta.Books.Core.Entities;
using Meta.Books.WebApi.Dto;

namespace Meta.Books.WebApi.Repositories.Interfaces;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User> Login(LoginDto loginDto);
}