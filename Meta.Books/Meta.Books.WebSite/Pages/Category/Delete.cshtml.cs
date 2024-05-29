using Meta.Books.WebApi.Dto;
using Meta.Books.WebSite.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Meta.Books.WebSite.Pages.Category;

public class Delete : PageModel
{
    [BindProperty]
    public CategoryDto CategoryDto { get; set; }

    public List<string> Errors { get; set; } = new List<string>();

    private readonly IBaseService<CategoryDto> _service;

    public Delete(IBaseService<CategoryDto> service)
    {
        _service = service;
    }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        CategoryDto = new CategoryDto();

        var response = await _service.GetById(id);
        CategoryDto = response.data;

        if (CategoryDto == null)
        {
            return RedirectToPage("/Error");
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var response = await _service.DeleteAsync(CategoryDto.id);
        return RedirectToPage("./List");
    }
}