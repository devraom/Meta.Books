using Meta.Books.Core.Entities;
using Meta.Books.WebApi.Dto;
using Meta.Books.WebApi.Repositories.Interfaces;

namespace Meta.Books.WebApi.Services.Interfaces;

public interface IUserService : IBaseService<UserDto>
{
    Task<bool> Login(LoginDto loginDto);
}