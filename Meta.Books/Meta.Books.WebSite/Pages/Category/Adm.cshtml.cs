using Meta.Books.Core.Http;
using Meta.Books.WebApi.Dto;
using Meta.Books.WebSite.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Meta.Books.WebSite.Pages.Category;

public class Adm : PageModel
{
    [BindProperty] public CategoryDto CategoryDto { get; set; }

    public List<string> Errors { get; set; } = new List<string>();

    private readonly IBaseService<CategoryDto> _service;

    public Adm(IBaseService<CategoryDto> service)
    {
        _service = service;
    }
        
    public async Task<IActionResult> OnGetAsync(int? id)
    {
        CategoryDto = new CategoryDto();
        if (id.HasValue && id.Value > 0)
        {
            var response = await _service.GetById(id.Value);
            CategoryDto = response.data;
        }

        if (CategoryDto == null)
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

        Response<CategoryDto> response;

        if (CategoryDto.id > 0)
        {
            // Update
            response = await _service.UpdateAsync(CategoryDto);
        }
        else
        {
            // Insert
            response = await _service.SaveAsync(CategoryDto);
        }

        if (Errors != null && Errors.Any())
        {
            foreach (var error in Errors)
            {
                Errors.Add(error); // Add the error message to the list
            }
            return Page();
        }

        CategoryDto = response.data;
        return RedirectToPage("./List");
    }
}