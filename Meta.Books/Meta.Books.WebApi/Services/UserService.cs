using Meta.Books.Core.Entities;
using Meta.Books.WebApi.Dto;
using Meta.Books.WebApi.Repositories.Interfaces;
using Meta.Books.WebApi.Services.Interfaces;

namespace Meta.Books.WebApi.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto> SaveAsync(UserDto userDto)
    {
        var user = new User
        {
            name = userDto.name,
            email = userDto.email,
            password = userDto.password,
            created_by = 0,
            created_date = DateTime.Now,
            updated_by = 0,
            updated_date = DateTime.Now
        };
        
        user = await _userRepository.SaveAsync(user);
        userDto.id = user.id;

        return userDto;
    }

    public async Task<UserDto> UpdateAsync(UserDto userDto)
    {
        var user = await _userRepository.GetById(userDto.id);

        if (user == null)
            throw new Exception("User not found");
            
        user.name = userDto.name;
        user.email = userDto.email;
        user.password = userDto.password;
        user.updated_by = 0;
        user.updated_date = DateTime.Now;
        
        await _userRepository.UpdateAsync(user);

        return userDto;
    }

    public async Task<List<UserDto>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();
        var usersDto = users.Select(c => new UserDto(c)).ToList();
        return usersDto;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var user = await _userRepository.GetById(id);
        if (user == null)
        {
            throw new Exception("User not found");
        }
        
        return await _userRepository.DeleteAsync(id);
    }

    public async Task<UserDto> GetById(int id)
    {
        var user = await _userRepository.GetById(id);
        if (user == null)
        {
            throw new Exception("User not found");
        }

        var userDto = new UserDto(user);
        return userDto;
    }

    public async Task<bool> Login(LoginDto loginDto)
    {
        var user = await _userRepository.Login(loginDto);
        return user != null;
    }
}