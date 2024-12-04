using Api.Data;
using Api.Interfaces;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly AppDbContext _context;
    
    public CommentRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<List<Comment>> GetAllAsync()
    {
        return await _context.Comments.ToListAsync();
    }

    public async Task<Comment?> GetByIdAsync(int id)
    {
        return await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Comment> CreateAsync(Comment entity)
    {
        await _context.Comments.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Comment?> UpdateAsync(int id, Comment entity)
    {
        var existingComment = await GetByIdAsync(id);

        if (existingComment == null)
        {
            return null;
        }
        
        existingComment.Title = entity.Title;
        existingComment.Content = entity.Content;
        await _context.SaveChangesAsync();
        
        return existingComment;
    }

    public async Task<Comment?> DeleteAsync(int id)
    {
        var comment = await GetByIdAsync(id);

        if (comment == null)
        {
            return null;
        }

        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();

        return comment;
    }
}