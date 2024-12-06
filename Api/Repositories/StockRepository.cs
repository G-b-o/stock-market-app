using Api.Data;
using Api.Helpers.Stock;
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
        return await _context.Stocks.Include(s => s.Comments).ToListAsync();
    }
    
    public Task<List<Stock>> GetAllAsync(QueryObject query)
    {
        var stocks = _context.Stocks.Include(s => s.Comments).AsQueryable();

        if (!string.IsNullOrWhiteSpace(query.CompanyName))
        {
            stocks = stocks.Where(s => s.CompanyName.ToLower().Contains(query.CompanyName.ToLower()));
        }

        if (!string.IsNullOrWhiteSpace(query.Symbol))
        {
            stocks = stocks.Where(s => s.Symbol.ToLower().Contains(query.Symbol.ToLower()));
        }

        if (!string.IsNullOrWhiteSpace(query.OrderBy))
        {
            if (query.OrderBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
            {
                stocks = query.IsDescending ? stocks.OrderByDescending(s => s.Symbol) : stocks.OrderBy(s => s.Symbol);
            }
        }
        
        var skip = query.PageSize * (query.PageNumber - 1);
        stocks = stocks.Skip(skip).Take(query.PageSize);
        
        return stocks.ToListAsync();
    }

    public async Task<Stock?> GetByIdAsync(int id)
    {
        return await _context.Stocks.Include(s => s.Comments).FirstOrDefaultAsync(s => s.Id == id);
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

    public async Task<bool> IsExistAsync(int id)
    {
        return await _context.Stocks.AnyAsync(e => e.Id == id);
    }
}