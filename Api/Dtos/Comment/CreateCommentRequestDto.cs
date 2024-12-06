using System.ComponentModel.DataAnnotations;

namespace Api.Dtos.Comment;

public class CreateCommentRequestDto
{
    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;
    [Required]
    [MaxLength(250)]
    public string Content { get; set; } = string.Empty;
}