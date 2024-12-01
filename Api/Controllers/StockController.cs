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
    public IActionResult Create([FromBody] StockRequestDto stockRequestDto)
    {
        var stock = stockRequestDto.ToStock();
        
        _context.Stocks.Add(stock);
        _context.SaveChanges();
        
        var dto = stock.ToStockDto();
        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }
}