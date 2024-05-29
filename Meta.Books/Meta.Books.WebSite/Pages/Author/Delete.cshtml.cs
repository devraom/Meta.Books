using Meta.Books.WebApi.Dto;
using Meta.Books.WebSite.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Meta.Books.WebSite.Pages.Author;

public class Delete : PageModel
{
    [BindProperty] public AuthorDto AuthorDto { get; set; }

    public List<string> Errors { get; set; } = new List<string>();

    private readonly IBaseService<AuthorDto> _service;

    public Delete(IBaseService<AuthorDto> service)
    {
        _service = service;
    }
    
    public async Task<IActionResult> OnGet(int id)
    {
        AuthorDto = new AuthorDto();

        var response = await _service.GetById(id);
        AuthorDto = response.data;

        if (AuthorDto == null)
        {
            return RedirectToPage("/Error");
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var response = await _service.DeleteAsync(AuthorDto.id);
        return RedirectToPage("./List");
    }
}