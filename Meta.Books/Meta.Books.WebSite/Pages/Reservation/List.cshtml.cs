using Meta.Books.WebApi.Dto;
using Meta.Books.WebSite.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Meta.Books.WebSite.Pages.Reservation;

public class ListModel : PageModel
{
    private readonly IBaseService<ReservationDto> _reservationService;
    private readonly IBaseService<BookDto> _bookService;
    private readonly IUserService _userService;

    public List<ReservationDto> Reservations { get; set; }
    public Dictionary<int, string> Books { get; set; }
    public Dictionary<int, string> Users { get; set; }

    public ListModel(IBaseService<ReservationDto> reservationService, IBaseService<BookDto> bookService, IUserService userService)
    {
        _reservationService = reservationService;
        _bookService = bookService;
        _userService = userService;
        Reservations = new List<ReservationDto>();
        Books = new Dictionary<int, string>();
        Users = new Dictionary<int, string>();
    }

    public async Task<IActionResult> OnGetAsync()
    {
        var reservations = await _reservationService.GetAllAsync();

        Reservations = reservations.data;

        foreach (var reservation in Reservations)
        {
            if (!Books.ContainsKey(reservation.book_id))
            {
                var response = await _bookService.GetById(reservation.book_id);
                if (response != null)
                {
                    Books[reservation.book_id] = response.data.title;
                }
            }

            if (!Users.ContainsKey(reservation.user_id))
            {
                var response = await _userService.GetById(reservation.user_id);
                if (response != null)
                {
                    Users[reservation.user_id] = response.data.name;
                }
            }
        }

        return Page();
    }
}