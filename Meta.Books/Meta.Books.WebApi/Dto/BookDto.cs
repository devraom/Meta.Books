using Meta.Books.Core.Entities;

namespace Meta.Books.WebApi.Dto;

public class BookDto : BaseDto
{
    public string title { get; set; }
    public int author_id { get; set; }
    public int category_id { get; set; }
    public int publisher_id { get; set; }
    
    public BookDto(){}

    public BookDto(Book book)
    {
        id = book.id;
        title = book.title;
        author_id = book.author_id;
        category_id = book.category_id;
        publisher_id = book.publisher_id;
    }
}