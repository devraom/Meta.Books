using Meta.Books.Core.Entities;
using Meta.Books.WebApi.Dto;
using Meta.Books.WebApi.Repositories.Interfaces;
using Meta.Books.WebApi.Services.Interfaces;

namespace Meta.Books.WebApi.Services;

public class BookService : IBaseService<BookDto>
{
    private readonly IBaseRepository<Book> _bookRepository;
    
    public BookService(IBaseRepository<Book> bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<BookDto> SaveAsync(BookDto bookDto)
    {
        var book = new Book
        {
            title = bookDto.title,
            author_id = bookDto.author_id,
            category_id = bookDto.category_id,
            publisher_id = bookDto.publisher_id,
            created_by = 0,
            created_date = DateTime.Now,
            updated_by = 0,
            updated_date = DateTime.Now
        };
        
        book = await _bookRepository.SaveAsync(book);
        bookDto.id = book.id;

        return bookDto;
    }

    public async Task<BookDto> UpdateAsync(BookDto bookDto)
    {
        var book = await _bookRepository.GetById(bookDto.id);

        if (book == null)
            throw new Exception("Book not found");
            
        book.title = bookDto.title;
        book.author_id = bookDto.author_id;
        book.category_id = bookDto.category_id;
        book.publisher_id = bookDto.publisher_id;
        book.updated_by = 0;
        book.updated_date = DateTime.Now;
        
        await _bookRepository.UpdateAsync(book);

        return bookDto;
    }

    public async Task<List<BookDto>> GetAllAsync()
    {
        var books = await _bookRepository.GetAllAsync();
        var booksDto = books.Select(c => new BookDto(c)).ToList();
        return booksDto;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var book = await _bookRepository.GetById(id);
        if (book == null)
        {
            throw new Exception("Book not found");
        }
        
        return await _bookRepository.DeleteAsync(id);
    }

    public async Task<BookDto> GetById(int id)
    {
        var book = await _bookRepository.GetById(id);
        if (book == null)
        {
            throw new Exception("Book not found");
        }

        var bookDto = new BookDto(book);
        return bookDto;
    }
}