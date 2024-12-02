using Api.Data;
using Api.Interfaces;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories;

public class StockRepository : IStockRepository
{
    private readonly AppDbContext _context;
    
    public StockRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Stock>> GetAllAsync()
    {
        return await _context.Stocks.ToListAsync();
    }

    public async Task<Stock?> GetByIdAsync(int id)
    {
        return await _context.Stocks.FindAsync(id);
    }

    public async Task<Stock> CreateAsync(Stock entity)
    {
        await _context.Stocks.AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<Stock?> UpdateAsync(int id, Stock entity)
    {
        var existingStock = await GetByIdAsync(id);

        if (existingStock == null)
        {
            return null;
        }
        
        existingStock.CompanyName = entity.CompanyName;
        existingStock.MarketCap = entity.MarketCap;
        existingStock.Industry = entity.Industry;
        existingStock.Purchase  = entity.Purchase;
        existingStock.Symbol = entity.Symbol;
        existingStock.LastDiv = entity.LastDiv;
        
        _context.Stocks.Update(existingStock);
        await _context.SaveChangesAsync();
        
        return existingStock;
    }

    public async Task<Stock?> DeleteAsync(int id)
    {
        var stock = await GetByIdAsync(id);

        if (stock == null)
        {
            return null;
        }
        
        _context.Stocks.Remove(stock);
        await _context.SaveChangesAsync();

        return stock;
    }
}