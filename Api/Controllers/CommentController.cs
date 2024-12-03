using Api.Dtos.Comment;
using Api.Interfaces;
using Api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/comments")]
public class CommentController : ControllerBase
{
    private readonly ICommentRepository _commentRepository;
    private readonly IStockRepository _stockRepository;

    public CommentController(ICommentRepository commentRepository, IStockRepository stockRepository)
    {
        _commentRepository = commentRepository;
        _stockRepository = stockRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var comments = await _commentRepository.GetAllAsync();
        var commentDtos = comments.Select(c => c.ToCommentDto());
        return Ok(commentDtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var comment = await _commentRepository.GetByIdAsync(id);
        
        return comment == null ? NotFound() : Ok(comment.ToCommentDto());
    }

    [HttpPost("{stockId}")]
    public async Task<IActionResult> Create([FromRoute] int stockId, [FromBody] CreateCommentRequestDto commentDto)
    {
        if (!await _stockRepository.IsExistAsync(stockId))
        {
            return BadRequest();
        }
        
        var commentToCreate = commentDto.ToComment(stockId);
        var createdComment = await _commentRepository.CreateAsync(commentToCreate);

        return CreatedAtAction(nameof(GetById), new { id = createdComment.Id }, createdComment.ToCommentDto());
    }
}