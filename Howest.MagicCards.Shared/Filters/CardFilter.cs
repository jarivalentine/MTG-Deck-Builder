namespace Howest.MagicCards.Shared.Filters;

public class CardFilter : PaginationFilter
{
    public string Name { get; set; } = "";
    public string Type { get; set; } = "";
    public string Text { get; set; } = "";
    public string Artist { get; set; } = "";
    public string Set { get; set; } = "";
    public string? Rarity { get; set; }

}
