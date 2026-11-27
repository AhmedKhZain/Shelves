namespace Shelves.API.Requests;

public record ClientGetAllRequest(
    string? Name = null,
    string? Email = null,
    string? Phone = null,
    int PageNumber = 0,
    int PageSize = 12) : PaginatedRequest(PageNumber, PageSize);