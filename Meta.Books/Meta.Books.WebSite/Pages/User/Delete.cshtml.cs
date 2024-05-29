using Meta.Books.WebApi.Dto;
using Meta.Books.WebSite.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Meta.Books.WebSite.Pages.User;

public class Delete : PageModel
{
    [BindProperty]
    public UserDto UserDto { get; set; }

    public List<string> Errors { get; set; } = new List<string>();

    private readonly IBaseService<UserDto> _service;

    public Delete(IBaseService<UserDto> service)
    {
        _service = service;
    }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        UserDto = new UserDto();

        var response = await _service.GetById(id);
        UserDto = response.data;

        if (UserDto == null)
        {
            return RedirectToPage("/Error");
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var response = await _service.DeleteAsync(UserDto.id);
        return RedirectToPage("./List");
    }
}