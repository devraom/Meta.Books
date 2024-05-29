using Meta.Books.Core.Http;
using Meta.Books.WebApi.Dto;
using Meta.Books.WebSite.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Meta.Books.WebSite.Pages.User;

public class Adm : PageModel
{
    [BindProperty] public UserDto UserDto { get; set; }

    public List<string> Errors { get; set; } = new List<string>();

    private readonly IBaseService<UserDto> _service;

    public Adm(IBaseService<UserDto> service)
    {
        _service = service;
    }
        
    public async Task<IActionResult> OnGetAsync(int? id)
    {
        UserDto = new UserDto();
        if (id.HasValue && id.Value > 0)
        {
            var response = await _service.GetById(id.Value);
            UserDto = response.data;
        }

        if (UserDto == null)
        {
            return RedirectToPage("/Error");
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        Response<UserDto> response;

        if (UserDto.id > 0)
        {
            // Update
            response = await _service.UpdateAsync(UserDto);
        }
        else
        {
            // Insert
            response = await _service.SaveAsync(UserDto);
        }

        if (Errors != null && Errors.Any())
        {
            foreach (var error in Errors)
            {
                Errors.Add(error); // Add the error message to the list
            }
            return Page();
        }

        UserDto = response.data;
        return RedirectToPage("./List");
    }
}