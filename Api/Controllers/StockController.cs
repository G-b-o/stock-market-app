using Api.Data;
using Api.Dtos.Stock;
using Api.Mappers;
using Microsoft.AspNetCore.Mvc;

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
    public IActionResult GetAll()
    {
        var stocks = _context.Stocks.Select(s => s.ToStockDto());
        
        return Ok(stocks);
    }

    [HttpGet("{id}")]
    public IActionResult GetById([FromRoute] int id)
    {
        var stock = _context.Stocks.Find(id);

        return stock == null ? NotFound() : Ok(stock.ToStockDto());
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateStockRequestDto stockRequestDto)
    {
        var stock = stockRequestDto.ToStock();
        
        _context.Stocks.Add(stock);
        _context.SaveChanges();
        
        var dto = stock.ToStockDto();
        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    [HttpPut("{id}")]
    public IActionResult Update([FromRoute] int id, [FromBody] UpdateStockRequestDto stockRequestDto)
    {
        var stock = _context.Stocks.Find(id);

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
        
        _context.Stocks.Update(stock);
        _context.SaveChanges();
        
        return Ok(stock.ToStockDto());
    }

    [HttpDelete("{id}")]
    public IActionResult Delete([FromRoute] int id)
    {
        var stock = _context.Stocks.Find(id);

        if (stock == null)
        {
            return NotFound();
        }
        
        _context.Stocks.Remove(stock);
        _context.SaveChanges();

        return NoContent();
    }
}