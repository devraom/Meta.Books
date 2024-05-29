using Meta.Books.WebApi.Dto;
using Meta.Books.WebSite.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Meta.Books.WebSite.Pages.Author;

public class ListModel : PageModel
{
    private readonly IBaseService<AuthorDto> _service;
    public List<AuthorDto> Authors { get; set; }

    public ListModel(IBaseService<AuthorDto> service)
    {
        _service = service;
        Authors = new List<AuthorDto>();
    }
    
    public async Task<IActionResult> OnGetAsync()
    {
        var response = await _service.GetAllAsync();
        Authors = response.data;

        return Page();
    }
}