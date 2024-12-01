using Api.Data;
using Api.Dtos.Stock;
using Api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("api/stocks")]
public class StockController : ControllerBase
{
    private readonly AppDbContext _context;
    
    public StockController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var stocks = await _context.Stocks.ToListAsync();
        var stockDtos = stocks.Select(s => s.ToStockDto());
        
        return Ok(stockDtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var stock = await _context.Stocks.FindAsync(id);

        return stock == null ? NotFound() : Ok(stock.ToStockDto());
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockRequestDto)
    {
        var stock = stockRequestDto.ToStock();
        
        await _context.Stocks.AddAsync(stock);
        await _context.SaveChangesAsync();
        
        var dto = stock.ToStockDto();
        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto stockRequestDto)
    {
        var stock = await _context.Stocks.FindAsync(id);

        if (stock == null)
        {
            return NotFound();
        }
        
        stock.CompanyName = stockRequestDto.CompanyName;
        stock.MarketCap = stockRequestDto.MarketCap;
        stock.Industry = stockRequestDto.Industry;
        stock.Purchase  = stockRequestDto.Purchase;
        stock.Symbol = stockRequestDto.Symbol;
        stock.LastDiv = stockRequestDto.LastDiv;
        
        var test = _context.Stocks.Update(stock);
        await _context.SaveChangesAsync();
        
        return Ok(stock.ToStockDto());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var stock = await _context.Stocks.FindAsync(id);

        if (stock == null)
        {
            return NotFound();
        }
        
        _context.Stocks.Remove(stock);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}