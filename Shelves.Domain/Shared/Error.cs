namespace Shelves.Domain.Shared;

public record Error(
     string Code,
     string Message,
     int StatusCode)
{
    public static Error NotFound(string code = "General.NotFound", string message = "Not Found")
    {
        return new(code, message, 404);
    }

    public static Error BadRequest(string code = "General.BadRequest", string message = "Bad Request")
    {
        return new(code, message, 400);
    }

    public static Error Unauthorized(string code = "General.Unauthorized", string message = "Unauthorized")
    {
        return new(code, message, 401);
    }

    public static Error Forbidden(string code = "General.Forbidden", string message = "Forbidden")
    {
        return new(code, message, 403);
    }

    public static Error Failure(string code = "General.Failure", string message = "Internal Server Error")
    {
        return new(code, message, 500);
    }

    public static Error Validation(string code = "General.Validation", string message = "Validation Error")
    {
        return new(code, message, 400);
    }
}

