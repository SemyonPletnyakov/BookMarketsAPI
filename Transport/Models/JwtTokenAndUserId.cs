namespace Transport.Models;

/// <summary>
/// JWT-токен с идентификатором пользователя.
/// </summary>
public sealed class JwtTokenAndUserId
{
    /// <summary>
    /// JWT-токен.
    /// </summary>
    public required string JwtToken { get; set; }

    /// <summary>
    /// Идентификатор пользователя.
    /// </summary>
    public int UserId { get; set; }
}
