using Meta.Books.WebApi.Dto;
using Meta.Books.WebSite.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Meta.Books.WebSite.Pages.User;

public class ListModel : PageModel
{
    private readonly IBaseService<UserDto> _userService;

    public List<UserDto> Users { get; set; }

    public ListModel(IBaseService<UserDto> userService)
    {
        _userService = userService;
        Users = new List<UserDto>();
    }

    public async Task<IActionResult> OnGetAsync()
    {
        var response = await _userService.GetAllAsync();
        Users = response.data;

        return Page();
    }
}