using Meta.Books.Core.Entities;
using Meta.Books.WebApi.Dto;
using Meta.Books.WebApi.Repositories.Interfaces;
using Meta.Books.WebApi.Services.Interfaces;

namespace Meta.Books.WebApi.Services;

public class CommentService : IBaseService<CommentDto>
{
    private readonly IBaseRepository<Comment> _commentRepository;
    
    public CommentService(IBaseRepository<Comment> commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<CommentDto> SaveAsync(CommentDto commentDto)
    {
        var comment = new Comment
        {
            book_id = commentDto.book_id,
            user_id = commentDto.user_id,
            comment = commentDto.comment,
            date = DateTime.Now,
            created_by = 0,
            created_date = DateTime.Now,
            updated_by = 0,
            updated_date = DateTime.Now
        };
        
        comment = await _commentRepository.SaveAsync(comment);
        commentDto.id = comment.id;

        return commentDto;
    }

    public async Task<CommentDto> UpdateAsync(CommentDto commentDto)
    {
        var comment = await _commentRepository.GetById(commentDto.id);

        if (comment == null)
            throw new Exception("Comment not found");
            
        comment.book_id = commentDto.book_id;
        comment.user_id = commentDto.user_id;
        comment.comment = commentDto.comment;
        comment.updated_by = 0;
        comment.updated_date = DateTime.Now;
        
        await _commentRepository.UpdateAsync(comment);

        return commentDto;
    }

    public async Task<List<CommentDto>> GetAllAsync()
    {
        var comments = await _commentRepository.GetAllAsync();
        var commentsDto = comments.Select(c => new CommentDto(c)).ToList();
        return commentsDto;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var comment = await _commentRepository.GetById(id);
        if (comment == null)
        {
            throw new Exception("Comment not found");
        }
        
        return await _commentRepository.DeleteAsync(id);
    }

    public async Task<CommentDto> GetById(int id)
    {
        var comment = await _commentRepository.GetById(id);
        if (comment == null)
        {
            throw new Exception("Comment not found");
        }

        var commentDto = new CommentDto(comment);
        return commentDto;
    }
}