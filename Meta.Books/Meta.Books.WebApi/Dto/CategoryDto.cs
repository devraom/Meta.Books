using Meta.Books.Core.Entities;

namespace Meta.Books.WebApi.Dto;

public class CategoryDto : BaseDto
{
    public string name { get; set; }
    
    public CategoryDto(){}

    public CategoryDto(Category category)
    {
        id = category.id;
        name = category.name;
    }
}