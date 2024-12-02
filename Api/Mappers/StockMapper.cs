using Api.Dtos.Stock;
using Api.Models;

namespace Api.Mappers;

public static class StockMapper
{
    public static StockDto ToStockDto(this Stock stock)
    {
        return new StockDto
        {
            Id = stock.Id,
            Industry = stock.Industry,
            Purchase = stock.Purchase,
            Symbol = stock.Symbol,
            CompanyName = stock.CompanyName,
            LastDiv = stock.LastDiv,
            MarketCap = stock.MarketCap,
            Comments = stock.Comments.Select(c => c.ToCommentDto()).ToList()
        };
    }

    public static Stock ToStock(this CreateStockRequestDto createStockRequestDto)
    {
        return new Stock
        {
            CompanyName = createStockRequestDto.CompanyName,
            LastDiv = createStockRequestDto.LastDiv,
            MarketCap = createStockRequestDto.MarketCap,
            Symbol = createStockRequestDto.Symbol,
            Industry = createStockRequestDto.Industry,
            Purchase = createStockRequestDto.Purchase
        };
    }
    
    public static Stock ToStock(this UpdateStockRequestDto updateStockRequestDto)
    {
        return new Stock
        {
            CompanyName = updateStockRequestDto.CompanyName,
            LastDiv = updateStockRequestDto.LastDiv,
            MarketCap = updateStockRequestDto.MarketCap,
            Symbol = updateStockRequestDto.Symbol,
            Industry = updateStockRequestDto.Industry,
            Purchase = updateStockRequestDto.Purchase
        };
    }
}