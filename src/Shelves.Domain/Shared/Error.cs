namespace Shelves.Domain.Shared;

public record Error(
     string Code,
     string Message,
     ErrorType Type)
{
    public static Error NotFound(string code = "General.NotFound", string message = "Not Found")
    {
        return new(code, message, ErrorType.NotFound);
    }

    public static Error BadRequest(string code = "General.BadRequest", string message = "Bad Request")
    {
        return new(code, message, ErrorType.Validation);
    }

    public static Error Unauthorized(string code = "General.Unauthorized", string message = "Unauthorized")
    {
        return new(code, message, ErrorType.Unauthorized);
    }

    public static Error Forbidden(string code = "General.Forbidden", string message = "Forbidden")
    {
        return new(code, message, ErrorType.Forbidden);
    }

    public static Error Failure(string code = "General.Failure", string message = "Internal Server Error")
    {
        return new(code, message, ErrorType.Failure);
    }

    public static Error Validation(string code = "General.Validation", string message = "Validation Error")
    {
        return new(code, message, ErrorType.Validation);
    }


}
