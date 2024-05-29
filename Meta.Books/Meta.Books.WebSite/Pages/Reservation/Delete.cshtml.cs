using Meta.Books.WebApi.Dto;
using Meta.Books.WebSite.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Meta.Books.WebSite.Pages.Reservation;

public class Delete : PageModel
{
    [BindProperty]
    public ReservationDto ReservationDto { get; set; }

    public string UserName { get; set; }
    public string BookTitle { get; set; }

    public List<string> Errors { get; set; } = new List<string>();

    private readonly IBaseService<ReservationDto> _service;
    private readonly IBaseService<UserDto> _userService;
    private readonly IBaseService<BookDto> _bookService;

    public Delete(IBaseService<ReservationDto> service, IBaseService<UserDto> userService, IBaseService<BookDto> bookService)
    {
        _service = service;
        _userService = userService;
        _bookService = bookService;
    }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var reservationResponse = await _service.GetById(id);
        ReservationDto = reservationResponse.data;

        if (ReservationDto == null)
        {
            return RedirectToPage("/Error");
        }

        var userResponse = await _userService.GetById(ReservationDto.user_id);
        UserName = userResponse.data?.name;

        var bookResponse = await _bookService.GetById(ReservationDto.book_id);
        BookTitle = bookResponse.data?.title;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var response = await _service.DeleteAsync(ReservationDto.id);
        return RedirectToPage("./List");
    }
}