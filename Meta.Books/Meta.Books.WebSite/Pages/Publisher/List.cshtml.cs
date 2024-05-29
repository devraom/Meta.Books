using Meta.Books.WebApi.Dto;
using Meta.Books.WebSite.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Meta.Books.WebSite.Pages.Publisher;

public class ListModel : PageModel
{
    private readonly IBaseService<PublisherDto> _service;
    public List<PublisherDto> Publishers { get; set; }

    public ListModel(IBaseService<PublisherDto> service)
    {
        _service = service;
        Publishers = new List<PublisherDto>();
    }
    
    public async Task<IActionResult> OnGetAsync()
    {
        var response = await _service.GetAllAsync();
        Publishers = response.data;

        return Page();
    }
}