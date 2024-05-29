using Meta.Books.Core.Entities;

namespace Meta.Books.WebApi.Dto;

public class CommentDto : BaseDto
{
    public int book_id { get; set; }
    public int user_id { get; set; }
    public string comment { get; set; }
    
    public CommentDto(){}

    public CommentDto(Comment comment)
    {
        id = comment.id;
        book_id = comment.book_id;
        user_id = comment.user_id;
        this.comment = comment.comment;
    }
}