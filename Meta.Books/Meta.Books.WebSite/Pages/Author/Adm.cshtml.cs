using Meta.Books.Core.Http;
using Meta.Books.WebApi.Dto;
using Meta.Books.WebSite.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Meta.Books.WebSite.Pages.Author;

public class Adm : PageModel
{
    [BindProperty] public AuthorDto AuthorDto { get; set; }

    private readonly IBaseService<AuthorDto> _service;

    public Adm(IBaseService<AuthorDto> service)
    {
        _service = service;
    }
    
    public async Task<IActionResult> OnGetAsync(int? id)
    {
        AuthorDto = new AuthorDto();
        if (id.HasValue && id.Value > 0)
        {
            var response = await _service.GetById(id.Value);
            AuthorDto = response.data;
        }

        if (AuthorDto == null)
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

        Response<AuthorDto> response;

        if (AuthorDto.id > 0)
        {
            //Update
            response = await _service.UpdateAsync(AuthorDto);
        }
        else
        {
            //Insert
            response = await _service.SaveAsync(AuthorDto);
        }

        AuthorDto = response.data;
        return RedirectToPage("./List");
    }
}