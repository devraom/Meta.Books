using Meta.Books.Core.Entities;

namespace Meta.Books.WebApi.Dto;

public class LoanDto : BaseDto
{
    public int user_id { get; set; }
    public int book_id { get; set; }
    public DateTime loan_date { get; set; }
    public DateTime return_date { get; set; }
    
    public LoanDto(){}

    public LoanDto(Loan loan)
    {
        id = loan.id;
        user_id = loan.user_id;
        book_id = loan.book_id;
        loan_date = loan.loan_date;
        return_date = loan.return_date;
    }
}