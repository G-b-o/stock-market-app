using System.ComponentModel.DataAnnotations;

namespace Api.Dtos.Stock;

public class UpdateStockRequestDto
{
    [Required]
    [MaxLength(15)]
    public string Symbol { get; set; } = string.Empty;
    [Required]
    [MaxLength(100)]
    public string CompanyName { get; set; } = string.Empty;
    [Required]
    [MaxLength(100)]
    public string Industry { get; set; } = string.Empty;
    [Required]
    [Range(1, 1_000_000_000)]
    public decimal Purchase { get; set; }
    [Required]
    [Range(0.01, 100)]
    public decimal LastDiv { get; set; }
    [Required]
    [Range(1, 9_000_000_000)]
    public long MarketCap { get; set; }
}