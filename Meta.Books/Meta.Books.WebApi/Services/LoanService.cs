using Meta.Books.Core.Entities;
using Meta.Books.WebApi.Dto;
using Meta.Books.WebApi.Repositories.Interfaces;
using Meta.Books.WebApi.Services.Interfaces;

namespace Meta.Books.WebApi.Services;

public class LoanService : IBaseService<LoanDto>
{
    private readonly IBaseRepository<Loan> _loanRepository;
    
    public LoanService(IBaseRepository<Loan> loanRepository)
    {
        _loanRepository = loanRepository;
    }

    public async Task<LoanDto> SaveAsync(LoanDto loanDto)
    {
        var loan = new Loan
        {
            user_id = loanDto.user_id,
            book_id = loanDto.book_id,
            loan_date = DateTime.Now,
            return_date = loanDto.return_date,
            created_by = 0,
            created_date = DateTime.Now,
            updated_by = 0,
            updated_date = DateTime.Now
        };
        
        loan = await _loanRepository.SaveAsync(loan);
        loanDto.id = loan.id;

        return loanDto;
    }

    public async Task<LoanDto> UpdateAsync(LoanDto loanDto)
    {
        var loan = await _loanRepository.GetById(loanDto.id);

        if (loan == null)
            throw new Exception("Loan not found");
            
        loan.book_id = loanDto.book_id;
        loan.user_id = loanDto.user_id;
        loan.loan_date = loanDto.loan_date;
        loan.return_date = loanDto.return_date;
        loan.updated_by = 0;
        loan.updated_date = DateTime.Now;
        
        await _loanRepository.UpdateAsync(loan);

        return loanDto;
    }

    public async Task<List<LoanDto>> GetAllAsync()
    {
        var loans = await _loanRepository.GetAllAsync();
        var loansDto = loans.Select(c => new LoanDto(c)).ToList();
        return loansDto;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var loan = await _loanRepository.GetById(id);
        if (loan == null)
        {
            throw new Exception("Loan not found");
        }
        
        return await _loanRepository.DeleteAsync(id);
    }

    public async Task<LoanDto> GetById(int id)
    {
        var loan = await _loanRepository.GetById(id);
        if (loan == null)
        {
            throw new Exception("Loan not found");
        }

        var loanDto = new LoanDto(loan);
        return loanDto;
    }
}