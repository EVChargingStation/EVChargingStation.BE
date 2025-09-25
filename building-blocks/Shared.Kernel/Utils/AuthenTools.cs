using System.Security.Claims;

#pragma warning disable CS8603 // Possible null reference return =))

namespace Shared.Kernel.Utils;

/// <summary>
/// Provides utility methods for authentication.
/// </summary>
public static class AuthenTools
{
    /// <summary>
    /// Gets the current user's ID from the claims identity.
    /// </summary>
    /// <param name="identity">The claims identity.</param>
    /// <returns>The user ID if found; otherwise, null.</returns>
    public static string? GetCurrentUserId(ClaimsIdentity? identity)
    {
        if (identity == null)
            return null;

        var userId = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        // Log userId value
        Console.WriteLine($"Extracted UserId from claims: {userId}");
        return userId;
    }
}