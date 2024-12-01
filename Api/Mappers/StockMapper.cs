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
}