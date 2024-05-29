using Meta.Books.Core.Entities;
using Meta.Books.Core.Http;
using Meta.Books.WebApi.Dto;
using Meta.Books.WebApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Meta.Books.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : BaseController<User, UserDto>
{
    private readonly IUserService _userService;

    public UserController(IUserService userService) : base((IBaseService<UserDto>)userService)
    {
        _userService = userService;
    }

    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<Response<bool>>> Login([FromBody] LoginDto loginDto)
    {
        var response = new Response<bool>();

        try
        {
            bool login = await _userService.Login(loginDto);
            response.data = login;

            return Ok(response);
        }
        catch (Exception ex)
        {
            response.errors.Add(ex.Message);
            return Unauthorized(response);
        }
    }
}