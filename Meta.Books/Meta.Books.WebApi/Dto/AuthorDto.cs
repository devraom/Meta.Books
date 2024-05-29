using Meta.Books.Core.Entities;

namespace Meta.Books.WebApi.Dto;

public class AuthorDto : BaseDto
{
    public string name { get; set; }
    
    public AuthorDto(){}

    public AuthorDto(Author author)
    {
        id = author.id;
        name = author.name;
    }
}