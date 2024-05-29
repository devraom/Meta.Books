using Meta.Books.Core.Http;
using Meta.Books.WebApi.Dto;
using Meta.Books.WebSite.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Meta.Books.WebSite.Pages.Comment;

public class Adm : PageModel
{
    [BindProperty] public CommentDto CommentDto { get; set; }

    public List<string> Errors { get; set; } = new List<string>();

    private readonly IBaseService<CommentDto> _commentService;
    private readonly IBaseService<BookDto> _bookService;
    private readonly IUserService _userService;

    public List<BookDto> Books { get; set; } = new List<BookDto>();
    public List<UserDto> Users { get; set; } = new List<UserDto>();

    public Adm(IBaseService<CommentDto> commentService, IBaseService<BookDto> bookService, IUserService userService)
    {
        _commentService = commentService;
        _bookService = bookService;
        _userService = userService;
    }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        Books = (await _bookService.GetAllAsync()).data;
        Users = (await _userService.GetAllAsync()).data;

        CommentDto = new CommentDto(); 
        if (id.HasValue && id.Value > 0)
        {
            var response = await _commentService.GetById(id.Value);
            CommentDto = response.data;
        }

        if (CommentDto == null)
        {
            return RedirectToPage("/Error");
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            Books = (await _bookService.GetAllAsync()).data;
            Users = (await _userService.GetAllAsync()).data;
            return Page();
        }

        Response<CommentDto> response;

        if (CommentDto.id > 0)
        { 
            // Update
            response = await _commentService.UpdateAsync(CommentDto);
        }
        else
        {
            // Insert
            response = await _commentService.SaveAsync(CommentDto);
        }

        if (response.errors != null && response.errors.Any())
        {
            foreach (var error in response.errors)
            {
                Errors.Add(error); // Add the error message to the list
            }
            Books = (await _bookService.GetAllAsync()).data;
            Users = (await _userService.GetAllAsync()).data;
            return Page();
        }

        CommentDto = response.data;
        return RedirectToPage("./List");
    }
}