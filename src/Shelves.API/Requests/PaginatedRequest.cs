namespace Shelves.API.Requests;

public record PaginatedRequest(
    int PageNumber = 0,
    int PageSize = 12)
{
    public const int MaxPageSize = 100;

    // Normalize values to prevent negative numbers or oversized queries
    public int NormalizedPageNumber => PageNumber < 0 ? 0 : PageNumber;
    public int NormalizedPageSize => PageSize < 1 ? 12 : Math.Min(PageSize, MaxPageSize);
}