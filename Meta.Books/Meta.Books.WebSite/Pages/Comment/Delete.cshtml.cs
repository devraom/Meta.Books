using Meta.Books.WebApi.Dto;
using Meta.Books.WebSite.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Meta.Books.WebSite.Pages.Comment;

public class Delete : PageModel
{
    [BindProperty]
    public CommentDto CommentDto { get; set; }

    public string UserName { get; set; }
    public string BookTitle { get; set; }

    public List<string> Errors { get; set; } = new List<string>();

    private readonly IBaseService<CommentDto> _service;
    private readonly IBaseService<UserDto> _userService;
    private readonly IBaseService<BookDto> _bookService;

    public Delete(IBaseService<CommentDto> service, IBaseService<UserDto> userService, IBaseService<BookDto> bookService)
    {
        _service = service;
        _userService = userService;
        _bookService = bookService;
    }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var commentResponse = await _service.GetById(id);
        CommentDto = commentResponse.data;

        if (CommentDto == null)
        {
            return RedirectToPage("/Error");
        }

        var userResponse = await _userService.GetById(CommentDto.user_id);
        UserName = userResponse.data?.name;

        var bookResponse = await _bookService.GetById(CommentDto.book_id);
        BookTitle = bookResponse.data?.title;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var response = await _service.DeleteAsync(CommentDto.id);
        return RedirectToPage("./List");
    }
}