using Meta.Books.WebApi.Dto;
using Meta.Books.WebSite.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Meta.Books.WebSite.Pages.Book;

public class ListModel : PageModel
{
    private readonly IBaseService<BookDto> _bookService;
    private readonly IBaseService<AuthorDto> _authorService;
    private readonly IBaseService<CategoryDto> _categoryService;
    private readonly IBaseService<PublisherDto> _publisherService;

    public List<BookDto> Books { get; set; }
    public Dictionary<int, string> Authors { get; set; }
    public Dictionary<int, string> Categories { get; set; }
    public Dictionary<int, string> Publishers { get; set; }

    public ListModel(IBaseService<BookDto> bookService, IBaseService<AuthorDto> authorService,
        IBaseService<CategoryDto> categoryService, IBaseService<PublisherDto> publisherService)
    {
        _bookService = bookService;
        _authorService = authorService;
        _categoryService = categoryService;
        _publisherService = publisherService;
        Books = new List<BookDto>();
        Authors = new Dictionary<int, string>();
        Categories = new Dictionary<int, string>();
        Publishers = new Dictionary<int, string>();
    }
    
    public async Task<IActionResult> OnGetAsync()
    {
        var books = await _bookService.GetAllAsync();

        Books = books.data;

        foreach (var book in Books)
        {
            if (!Authors.ContainsKey(book.author_id))
            {
                var response = await _authorService.GetById(book.author_id);
                if (response != null)
                {
                    Authors[book.author_id] = response.data.name;
                }
            }

            if (!Categories.ContainsKey(book.category_id))
            {
                var response = await _categoryService.GetById(book.category_id);
                if (response != null)
                {
                    Categories[book.category_id] = response.data.name;
                }
            }

            if (!Publishers.ContainsKey(book.publisher_id))
            {
                var response = await _publisherService.GetById(book.publisher_id);
                if (response != null)
                {
                    Publishers[book.publisher_id] = response.data.name;
                }
            }
        }

        return Page();
    }
}