using System.Net.NetworkInformation;

namespace Shelves.Domain.Shared;

public class ResultOf<T> : IResultOf<T>
{
    private bool _isError = false;
    private DoneStatus _statusCode = DoneStatus.Done;
    private List<Error> _errors = new();


    private T? _value = default(T);

    public bool IsSuccess => !_isError;
    public DoneStatus StatusCode => _statusCode;
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

    private ResultOf(T value, DoneStatus statusCode=DoneStatus.Done)
    {
        _isError = false;
        _value = value;
        _statusCode = statusCode;
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

    public TNextValue Match<TNextValue>(Func<T, DoneStatus, TNextValue> onValue, Func<List<Error>, TNextValue> onError)
    {
        if (IsError)
        {
            return onError(_errors);
        }

        return onValue(_value, _statusCode);
    }
    public TNextValue Match<TNextValue>(Func<T, TNextValue> onValue, Func<List<Error>, TNextValue> onError)
    {
        if (IsError)
        {
            return onError(_errors);
        }

        return onValue(_value);
    }

    public static ResultOf<T> FromValue(T value, DoneStatus status = DoneStatus.Done)
    {
        return new ResultOf<T>(value, status);
    }


    
}

[Flags]
public enum DoneStatus
{
    None = 0,
    Done = 1,
    NoContent = 2,
    Created = 4,
    Accepted = 8,
    Partial = 16,
    ZeroCount = 32,
    PartialZeroCount = 48,
    Cached = 64,
    CachedPartial = 80,
}