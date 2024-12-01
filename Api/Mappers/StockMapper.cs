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

    public static Stock ToStock(this StockRequestDto stockRequestDto)
    {
        return new Stock
        {
            CompanyName = stockRequestDto.CompanyName,
            LastDiv = stockRequestDto.LastDiv,
            MarketCap = stockRequestDto.MarketCap,
            Symbol = stockRequestDto.Symbol,
            Industry = stockRequestDto.Industry,
            Purchase = stockRequestDto.Purchase
        };
    }
}