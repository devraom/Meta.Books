using Meta.Books.Core.Http;
using Meta.Books.WebApi.Dto;
using Meta.Books.WebSite.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Meta.Books.WebSite.Pages.Loan;

public class Adm : PageModel
{
    [BindProperty]
    public LoanDto LoanDto { get; set; }

    public List<string> Errors { get; set; } = new List<string>();

    private readonly IBaseService<LoanDto> _loanService; 
    private readonly IBaseService<BookDto> _bookService;
    private readonly IUserService _userService;

    public List<BookDto> Books { get; set; } = new List<BookDto>();
    public List<UserDto> Users { get; set; } = new List<UserDto>();

    public Adm(IBaseService<LoanDto> loanService, IBaseService<BookDto> bookService, IUserService userService)
    {
        _loanService = loanService;
        _bookService = bookService;
        _userService = userService;
    }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        Books = (await _bookService.GetAllAsync()).data;
        Users = (await _userService.GetAllAsync()).data;

        LoanDto = new LoanDto();
        if (id.HasValue && id.Value > 0)
        {
            var response = await _loanService.GetById(id.Value);
            LoanDto = response.data;
        }

        if (LoanDto == null)
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

        Response<LoanDto> response;

        if (LoanDto.id > 0)
        {
            // Update
            response = await _loanService.UpdateAsync(LoanDto);
        }
        else
        {
            // Insert
            response = await _loanService.SaveAsync(LoanDto);
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

        LoanDto = response.data;
        return RedirectToPage("./List");
    }
}