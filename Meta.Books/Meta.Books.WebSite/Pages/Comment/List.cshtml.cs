using Meta.Books.WebApi.Dto;
using Meta.Books.WebSite.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Meta.Books.WebSite.Pages.Comment;

public class ListModel : PageModel
{
    private readonly IBaseService<CommentDto> _commentService;
    private readonly IBaseService<BookDto> _bookService;
    private readonly IUserService _userService;

    public List<CommentDto> Comments { get; set; }
    public Dictionary<int, string> Books { get; set; }
    public Dictionary<int, string> Users { get; set; }

    public ListModel(IBaseService<CommentDto> commentService, IBaseService<BookDto> bookService, IUserService userService)
    {
        _commentService = commentService;
        _bookService = bookService;
        _userService = userService;
        Comments = new List<CommentDto>();
        Books = new Dictionary<int, string>();
        Users = new Dictionary<int, string>();
    }

    public async Task<IActionResult> OnGetAsync()
    {
        var comments = await _commentService.GetAllAsync();

        Comments = comments.data;

        foreach (var comment in Comments)
        {
            if (!Books.ContainsKey(comment.book_id))
            {
                var response = await _bookService.GetById(comment.book_id);
                if (response != null)
                {
                    Books[comment.book_id] = response.data.title;
                }
            }

            if (!Users.ContainsKey(comment.user_id))
            {
                var response = await _userService.GetById(comment.user_id);
                if (response != null)
                {
                    Users[comment.user_id] = response.data.name;
                }
            }
        }

        return Page();
    }
}