namespace Shared.Kernel.Utils;

/// <summary>
/// Represents the result of an API operation that does not return a value.
/// </summary>
public class ApiResult
{
    public bool IsSuccess { get; set; }
    public bool IsFailure => !IsSuccess;
    public ResponseContent Value { get; set; }
    public ErrorContent Error { get; set; }

    public static ApiResult Success(string code = "200", string message = "Operation successful.")
    {
        return new ApiResult
        {
            IsSuccess = true,
            Value = new ResponseContent
            {
                Code = code,
                Message = message
            },
            Error = null
        };
    }

    public static ApiResult Failure(string code = "400", string message = "Operation failed.")
    {
        return new ApiResult
        {
            IsSuccess = false,
            Value = null,
            Error = new ErrorContent
            {
                Code = code,
                Message = message
            }
        };
    }
}

/// <summary>
/// Represents the result of an API operation that returns a value.
/// </summary>
/// <typeparam name="T">The type of the value returned by the operation.</typeparam>
public class ApiResult<T>
{
    public bool IsSuccess { get; set; }
    public bool IsFailure => !IsSuccess;
    public ResponseDataContent<T> Value { get; set; }
    public ErrorContent Error { get; set; }

    public static ApiResult<T> Success(T data, string code = "200", string message = "Operation successful.")
    {
        return new ApiResult<T>
        {
            IsSuccess = true,
            Value = new ResponseDataContent<T>
            {
                Code = code,
                Message = message,
                Data = data
            },
            Error = null
        };
    }

    public static ApiResult<T> Failure(string code = "400", string message = "Operation failed.", T value = default)
    {
        return new ApiResult<T>
        {
            IsSuccess = false,
            Value = value == null
                ? null
                : new ResponseDataContent<T>
                {
                    Code = code,
                    Message = message,
                    Data = value
                },
            Error = new ErrorContent
            {
                Code = code,
                Message = message
            }
        };
    }
}

/// <summary>
/// Represents the content of a successful response without data.
/// </summary>
public class ResponseContent
{
    public string Code { get; set; }
    public string Message { get; set; }
}

/// <summary>
/// Represents the content of a successful response with data.
/// </summary>
/// <typeparam name="T">The type of the data.</typeparam>
public class ResponseDataContent<T>
{
    public string Code { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
}

/// <summary>
/// Represents the content of an error response.
/// </summary>
public class ErrorContent
{
    public string Code { get; set; }
    public string Message { get; set; }
}