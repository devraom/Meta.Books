    using Meta.Books.Core.Http;
using Meta.Books.WebApi.Dto;
using Meta.Books.WebSite.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Meta.Books.WebSite.Pages.Book;

public class Adm : PageModel
{
    [BindProperty] public BookDto BookDto { get; set; }
        public List<string> Errors { get; set; } = new List<string>();

        private readonly IBaseService<BookDto> _bookService;
        private readonly IBaseService<AuthorDto> _authorService;
        private readonly IBaseService<CategoryDto> _categoryService;
        private readonly IBaseService<PublisherDto> _publisherService;

        public List<AuthorDto> Authors { get; set; }
        public List<CategoryDto> Categories { get; set; }
        public List<PublisherDto> Publishers { get; set; }

        public Adm(IBaseService<BookDto> bookService, IBaseService<AuthorDto> authorService, IBaseService<CategoryDto> categoryService, IBaseService<PublisherDto> publisherService)
        {
            _bookService = bookService;
            _authorService = authorService;
            _categoryService = categoryService;
            _publisherService = publisherService;
        }
        
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            Authors = (await _authorService.GetAllAsync()).data;
            Categories = (await _categoryService.GetAllAsync()).data;
            Publishers = (await _publisherService.GetAllAsync()).data;

            BookDto = new BookDto();
            if (id.HasValue && id.Value > 0)
            {
                var response = await _bookService.GetById(id.Value);
                BookDto = response.data;
            }

            if (BookDto == null)
            {
                return RedirectToPage("/Error");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Authors = (await _authorService.GetAllAsync()).data;
                Categories = (await _categoryService.GetAllAsync()).data;
                Publishers = (await _publisherService.GetAllAsync()).data;
                return Page();
            }

            Response<BookDto> response;

            if (BookDto.id > 0)
            {
                // Update
                response = await _bookService.UpdateAsync(BookDto);
            }
            else
            {
                // Insert
                response = await _bookService.SaveAsync(BookDto);
            }

            if (Errors != null && Errors.Any())
            {
                foreach (var error in Errors)
                {
                    Errors.Add(error); // Add the error message to the list
                }
                Authors = (await _authorService.GetAllAsync()).data;
                Categories = (await _categoryService.GetAllAsync()).data;
                Publishers = (await _publisherService.GetAllAsync()).data;
                return Page();
            }

            BookDto = response.data;
            return RedirectToPage("./List");
        }
}