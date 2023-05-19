namespace Howest.MagicCards.Shared.Filters;

public class PaginationFilter
{
    private int _maxPageSize = 150;
    private int _pageSize = 10;
    private int _pageNumber = 1;

    public int PageNumber
    {
        get { return _pageNumber; }
        set { _pageNumber = (value < 1) ? 1 : value; }
    }

    public int PageSize
    {
        get { return _pageSize > _maxPageSize ? _maxPageSize : _pageSize; }
        set { _pageSize = (value > _maxPageSize || value < 1) ? _maxPageSize : value; }
    }
}
