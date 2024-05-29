using Meta.Books.WebApi.Dto;
using Meta.Books.WebSite.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Meta.Books.WebSite.Pages.Book;

public class Delete : PageModel
{
    [BindProperty]
    public BookDto BookDto { get; set; }

    public string AuthorName { get; set; }
    public string CategoryName { get; set; }
    public string PublisherName { get; set; }

    public List<string> Errors { get; set; } = new List<string>();

    private readonly IBaseService<BookDto> _service;
    private readonly IBaseService<AuthorDto> _authorService;
    private readonly IBaseService<CategoryDto> _categoryService;
    private readonly IBaseService<PublisherDto> _publisherService;

    public Delete(IBaseService<BookDto> service, IBaseService<AuthorDto> authorService, IBaseService<CategoryDto> categoryService, IBaseService<PublisherDto> publisherService)
    {
        _service = service;
        _authorService = authorService;
        _categoryService = categoryService;
        _publisherService = publisherService;
    }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var bookResponse = await _service.GetById(id);
        BookDto = bookResponse.data;

        if (BookDto == null)
        {
            return RedirectToPage("/Error");
        }

        var authorResponse = await _authorService.GetById(BookDto.author_id);
        AuthorName = authorResponse.data?.name;

        var categoryResponse = await _categoryService.GetById(BookDto.category_id);
        CategoryName = categoryResponse.data?.name;

        var publisherResponse = await _publisherService.GetById(BookDto.publisher_id);
        PublisherName = publisherResponse.data?.name;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var response = await _service.DeleteAsync(BookDto.id);
        return RedirectToPage("./List");
    }
}