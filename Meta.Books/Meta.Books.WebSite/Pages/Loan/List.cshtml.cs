using Meta.Books.WebApi.Dto;
using Meta.Books.WebSite.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Meta.Books.WebSite.Pages.Loan;

public class ListModel : PageModel
{
    private readonly IBaseService<LoanDto> _loanService;
    private readonly IBaseService<BookDto> _bookService;
    private readonly IUserService _userService;

    public List<LoanDto> Loans { get; set; }
    public Dictionary<int, string> Books { get; set; }
    public Dictionary<int, string> Users { get; set; }

    public ListModel(IBaseService<LoanDto> loanService, IBaseService<BookDto> bookService, IUserService userService)
    {
        _loanService = loanService;
        _bookService = bookService;
        _userService = userService;
        Loans = new List<LoanDto>();
        Books = new Dictionary<int, string>();
        Users = new Dictionary<int, string>();
    }

    public async Task<IActionResult> OnGetAsync()
    {
        var loans = await _loanService.GetAllAsync();

        Loans = loans.data;

        foreach (var loan in Loans)
        {
            if (!Books.ContainsKey(loan.book_id))
            {
                var response = await _bookService.GetById(loan.book_id);
                if (response != null)
                {
                    Books[loan.book_id] = response.data.title;
                }
            }

            if (!Users.ContainsKey(loan.user_id))
            {
                var response = await _userService.GetById(loan.user_id);
                if (response != null)
                {
                    Users[loan.user_id] = response.data.name;
                }
            }
        }

        return Page();
    }
}