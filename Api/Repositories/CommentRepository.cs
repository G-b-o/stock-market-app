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

    public Task<Comment?> UpdateAsync(int id, Comment entity)
    {
        throw new NotImplementedException();
    }

    public Task<Comment?> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}