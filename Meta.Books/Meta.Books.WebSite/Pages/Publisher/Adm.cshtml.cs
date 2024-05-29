using Meta.Books.Core.Http;
using Meta.Books.WebApi.Dto;
using Meta.Books.WebSite.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Meta.Books.WebSite.Pages.Publisher;

public class Adm : PageModel
{
    [BindProperty] public PublisherDto PublisherDto { get; set; }

    public List<string> Errors { get; set; } = new List<string>();

    private readonly IBaseService<PublisherDto> _service;

    public Adm(IBaseService<PublisherDto> service)
    {
        _service = service;
    }
        
    public async Task<IActionResult> OnGetAsync(int? id)
    {
        PublisherDto = new PublisherDto();
        if (id.HasValue && id.Value > 0)
        {
            var response = await _service.GetById(id.Value);
            PublisherDto = response.data;
        }

        if (PublisherDto == null)
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

        Response<PublisherDto> response;

        if (PublisherDto.id > 0)
        {
            // Update
            response = await _service.UpdateAsync(PublisherDto);
        }
        else
        {
            // Insert
            response = await _service.SaveAsync(PublisherDto);
        }

        if (Errors != null && Errors.Any())
        {
            foreach (var error in Errors)
            {
                Errors.Add(error); // Add the error message to the list
            }
            return Page();
        }

        PublisherDto = response.data;
        return RedirectToPage("./List");
    }
}