namespace Shared.Kernel.Utils;

/// <summary>
/// Helper class for creating exceptions with specific HTTP status codes.
/// </summary>
public static class ErrorHelper
{
    /// <summary>
    /// Creates an exception with a specified status code and message.
    /// </summary>
    /// <param name="statusCode">The HTTP status code.</param>
    /// <param name="message">The error message.</param>
    /// <returns>An exception with the status code stored in its Data dictionary.</returns>
    public static Exception WithStatus(int statusCode, string message)
    {
        var ex = new Exception(message);
        ex.Data["StatusCode"] = statusCode;
        return ex;
    }

    /// <summary>
    /// Creates an Unauthorized (401) exception.
    /// Call when the user is not logged in or the token is invalid.
    /// </summary>
    public static Exception Unauthorized(string message = "Không được phép.")
    {
        return WithStatus(401, message); // Unauthorized
    }

    /// <summary>
    /// Creates a NotFound (404) exception.
    /// Call when a resource (user, product, order, etc.) does not exist.
    /// </summary>
    public static Exception NotFound(string message = "Không tìm thấy tài nguyên.")
    {
        return WithStatus(404, message); // Not Found
    }

    /// <summary>
    /// Creates a BadRequest (400) exception.
    /// Call when the client sends incorrect data (validation failed, wrong format, missing fields).
    /// </summary>
    public static Exception BadRequest(string message = "Dữ liệu không hợp lệ.")
    {
        return WithStatus(400, message); // Bad Request
    }

    /// <summary>
    /// Creates a Forbidden (403) exception.
    /// Call when the user is logged in but does not have permission to perform the action.
    /// </summary>
    public static Exception Forbidden(string message = "Truy cập bị từ chối.")
    {
        return WithStatus(403, message); // Forbidden
    }

    /// <summary>
    /// Creates a Conflict (409) exception.
    /// Call when there is a data conflict (e.g., email already exists, registering the same product twice).
    /// </summary>
    public static Exception Conflict(string message = "Xung đột dữ liệu.")
    {
        return WithStatus(409, message); // Conflict
    }

    /// <summary>
    /// Creates an InternalServerError (500) exception.
    /// Call for unknown system errors (null reference, database error, etc.).
    /// </summary>
    public static Exception Internal(string message = "Lỗi hệ thống.")
    {
        return WithStatus(500, message); // Internal Server Error
    }
}