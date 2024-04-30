namespace Transport.Models;

/// <summary>
/// Работник.
/// </summary>
public sealed class EmployeeLoginAndPassword
{
    /// <summary>
    /// Логин.
    /// </summary>
    public required string Login { get; set; }

    /// <summary>
    /// Пароль.
    /// </summary>
    public required string Password { get; set; }
}
