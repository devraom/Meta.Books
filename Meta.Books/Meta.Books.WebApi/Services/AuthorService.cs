using Meta.Books.Core.Entities;
using Meta.Books.WebApi.Dto;
using Meta.Books.WebApi.Repositories.Interfaces;
using Meta.Books.WebApi.Services.Interfaces;

namespace Meta.Books.WebApi.Services;

public class AuthorService : IBaseService<AuthorDto>
{
    private readonly IBaseRepository<Author> _authorRepository;
    
    public AuthorService(IBaseRepository<Author> authorRepository)
    {
        _authorRepository = authorRepository;
    }   

    public async Task<AuthorDto> SaveAsync(AuthorDto authorDto)
    {
        var author = new Author
        {
            name = authorDto.name,
            created_by = 0,
            created_date = DateTime.Now,
            updated_by = 0,
            updated_date = DateTime.Now
        };
        
        author = await _authorRepository.SaveAsync(author);
        authorDto.id = author.id;

        return authorDto;
    }

    public async Task<AuthorDto> UpdateAsync(AuthorDto authorDto)
    {
        var author = await _authorRepository.GetById(authorDto.id);

        if (author == null)
            throw new Exception("Author not found");
            
        author.name = authorDto.name;
        author.updated_by = 0;
        author.updated_date = DateTime.Now;
        
        await _authorRepository.UpdateAsync(author);

        return authorDto;
    }

    public async Task<List<AuthorDto>> GetAllAsync()
    {
        var authors = await _authorRepository.GetAllAsync();
        var authorsDto = authors.Select(c => new AuthorDto(c)).ToList();
        return authorsDto;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var author = await _authorRepository.GetById(id);
        if (author == null)
        {
            throw new Exception("Author not found");
        }
        
        return await _authorRepository.DeleteAsync(id);
    }

    public async Task<AuthorDto> GetById(int id)
    {
        var author = await _authorRepository.GetById(id);
        if (author == null)
        {
            throw new Exception("Author not found");
        }

        var authorDto = new AuthorDto(author);
        return authorDto;
    }
}