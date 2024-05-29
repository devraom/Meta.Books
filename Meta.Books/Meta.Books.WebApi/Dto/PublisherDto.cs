using Meta.Books.Core.Entities;

namespace Meta.Books.WebApi.Dto;

public class PublisherDto : BaseDto
{
    public string name { get; set; }
    
    public PublisherDto(){}

    public PublisherDto(Publisher publisher)
    {
        id = publisher.id;
        name = publisher.name;
    }
}