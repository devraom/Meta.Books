using Meta.Books.WebApi.Dto;
using Meta.Books.WebSite.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Meta.Books.WebSite.Pages.Publisher;

public class Delete : PageModel
{
    [BindProperty]
    public PublisherDto PublisherDto { get; set; }

    public List<string> Errors { get; set; } = new List<string>();

    private readonly IBaseService<PublisherDto> _service;

    public Delete(IBaseService<PublisherDto> service)
    {
        _service = service;
    }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        PublisherDto = new PublisherDto();

        var response = await _service.GetById(id);
        PublisherDto = response.data;

        if (PublisherDto == null)
        {
            return RedirectToPage("/Error");
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var response = await _service.DeleteAsync(PublisherDto.id);
        return RedirectToPage("./List");
    }
}