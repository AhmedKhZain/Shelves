namespace Shelves.Domain.Shared;

public interface IResultOf
{
    bool IsError { get; }
    bool IsSuccess { get; }
    List<Error> Errors { get; }
}


public interface IResultOf<out T> : IResultOf
{
    T? Value { get; }
}
