namespace Api.Helpers.Stock;

public class QueryObject
{
    public string? Symbol { get; set; }
    public string? CompanyName { get; set; }
    public string? OrderBy { get; set; }
    public bool IsDescending { get; set; } = false;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 1;
}