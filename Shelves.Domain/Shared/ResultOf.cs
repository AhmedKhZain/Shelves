namespace Shelves.Domain.Shared;

public class ResultOf<T> : IResultOf<T>
{
    private bool _isError = false;
    private List<Error> _errors = new();
    private T? _value = default(T);

    public bool IsSuccess => !_isError;

    public bool IsError => _isError;

    public List<Error> Errors
    {
        get
        {
            if (_isError)
            {
                return _errors;
            }
            throw new InvalidOperationException("Cannot access Errors when the result is successful.");
        }
    }
    public T? Value
    {
        get
        {
            if (!_isError)
            {
                return _value;
            }
            throw new InvalidOperationException("Cannot access Value when the result is an error.");
        }
    }

    private ResultOf(IEnumerable<Error> errors)
    {
        _isError = true;
        _errors.AddRange(errors);
    }
    private ResultOf(Error error)
    {
        _isError = true;
        _errors.Add(error);
    }

    private ResultOf(T value)
    {
        _isError = false;
        _value = value;
    }

    public static implicit operator ResultOf<T>(Error error)
    {
        return new ResultOf<T>(error);
    }
    public static implicit operator ResultOf<T>(List<Error> errors)
    {
        return new ResultOf<T>(errors);
    }

    public static implicit operator ResultOf<T>(T value)
    {
        return new ResultOf<T>(value);
    }




}