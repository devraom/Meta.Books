using Meta.Books.Core.Entities;

namespace Meta.Books.WebApi.Dto;

public class UserDto : BaseDto
{
    public string name { get; set; }
    public string email { get; set; }
    public string password { get; set; }
    
    public UserDto(){}

    public UserDto(User user)
    {
        id = user.id;
        name = user.name;
        email = user.email;
        password = user.password;
    }
}