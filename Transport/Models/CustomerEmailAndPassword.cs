namespace Transport.Models;

/// <summary>
/// Покупатель.
/// </summary>
public sealed class CustomerEmailAndPassword
{
    /// <summary>
    /// Электронная почта.
    /// </summary>
    public required string Email { get; set; }

    /// <summary>
    /// Пароль.
    /// </summary>
    public required string Password { get; set; }
}
