using Meta.Books.Core.Http;
using Meta.Books.WebApi.Dto;

namespace Meta.Books.WebSite.Services.Interfaces;

public interface IUserService : IBaseService<UserDto>
{
    Task<Response<bool>> Login(LoginDto loginDto);
}