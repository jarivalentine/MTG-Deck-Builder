namespace Howest.MagicCards.Shared.Filters;

public class CardFilter : PaginationFilter
{
    public string Name { get; set; } = "";
    public string Type { get; set; } = "";
    public string Text { get; set; } = "";
}
