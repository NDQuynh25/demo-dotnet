

public class PageReq
{
    public int Page { get; set; } = 1;
    public int Limit { get; set; } = 5;

    public string? Search { get; set; } = null;
}