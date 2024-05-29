using Meta.Books.WebApi.Dto;
using Meta.Books.WebSite.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Meta.Books.WebSite.Pages.Category;

public class ListModel : PageModel
{
    private readonly IBaseService<CategoryDto> _service;
    public List<CategoryDto> Categories { get; set; }

    public ListModel(IBaseService<CategoryDto> service)
    {
        _service = service;
        Categories = new List<CategoryDto>();
    }
    
    public async Task<IActionResult> OnGetAsync()
    {
        var response = await _service.GetAllAsync();
        Categories = response.data;

        return Page();
    }
}