using Api.Dtos.Comment;
using Api.Models;

namespace Api.Mappers;

public static class CommentMapper
{
    public static CommentDto ToCommentDto(this Comment comment)
    {
        return new CommentDto
        {
            Id = comment.Id,
            Content = comment.Content,
            Title = comment.Title,
            CreateAt = comment.CreateAt,
            StockId = comment.StockId
        };
    }
}