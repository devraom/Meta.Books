using Meta.Books.Core.Http;
using Meta.Books.WebApi.Dto;
using Meta.Books.WebSite.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Meta.Books.WebSite.Pages.Reservation;

public class Adm : PageModel
{
    [BindProperty]
        public ReservationDto ReservationDto { get; set; }

        public List<string> Errors { get; set; } = new List<string>();

        private readonly IBaseService<ReservationDto> _reservationService;
        private readonly IBaseService<BookDto> _bookService;
        private readonly IUserService _userService;

        public List<BookDto> Books { get; set; } = new List<BookDto>();
        public List<UserDto> Users { get; set; } = new List<UserDto>();

        public Adm(IBaseService<ReservationDto> reservationService, IBaseService<BookDto> bookService, IUserService userService)
        {
            _reservationService = reservationService;
            _bookService = bookService;
            _userService = userService;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            Books = (await _bookService.GetAllAsync()).data;
            Users = (await _userService.GetAllAsync()).data;

            ReservationDto = new ReservationDto();
            if (id.HasValue && id.Value > 0)
            {
                var response = await _reservationService.GetById(id.Value);
                ReservationDto = response.data;
            }

            if (ReservationDto == null)
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

            Response<ReservationDto> response;

            if (ReservationDto.id > 0)
            {
                // Update
                response = await _reservationService.UpdateAsync(ReservationDto);
            }
            else
            {
                // Insert
                response = await _reservationService.SaveAsync(ReservationDto);
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

            ReservationDto = response.data;
            return RedirectToPage("./List");
        }
}