using Meta.Books.Core.Entities;

namespace Meta.Books.WebApi.Dto;

public class LoginDto
{
    public string email { get; set; }
    public string password { get; set; }
    
    public LoginDto(){}

    public LoginDto(LoginDto loginDto)
    {
        email = loginDto.email;
        password = loginDto.password;
    }
}