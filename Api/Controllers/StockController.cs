using Api.Data;
using Api.Dtos.Stock;
using Api.Helpers.Stock;
using Api.Interfaces;
using Api.Mappers;
using Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("api/stocks")]
public class StockController : ControllerBase
{
    private readonly IStockRepository _stockRepo;
    
    public StockController(IStockRepository stockRepo)
    {
        _stockRepo = stockRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] QueryObject queryObject)
    {
        var stocks = await _stockRepo.GetAllAsync(queryObject);
        var stockDtos = stocks.Select(s => s.ToStockDto());
        
        return Ok(stockDtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var stock = await _stockRepo.GetByIdAsync(id);

        return stock == null ? NotFound() : Ok(stock.ToStockDto());
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockRequestDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var createStockDto = stockRequestDto.ToStock();
        
        var createdStock = await _stockRepo.CreateAsync(createStockDto);
        
        var dto = createdStock.ToStockDto();
        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto stockRequestDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var stockToUpdate = stockRequestDto.ToStock();
        
        var updatedStock = await _stockRepo.UpdateAsync(id, stockToUpdate);
        
        return updatedStock == null ? NotFound() : Ok(updatedStock.ToStockDto());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var stock = await _stockRepo.DeleteAsync(id);
        
        if (stock == null)
        {
            return NotFound();
        }

        return NoContent();
    }
}