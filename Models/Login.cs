namespace Models;

/// <summary>
/// Логин.
/// </summary>
public sealed record Login
{
    /// <summary>
    /// Значение логина.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Создаёт объект <see cref="Login"/>
    /// </summary>
    /// <param name="value">
    /// Значение логина.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="value"/> равен <see langword="null"/>.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="value"/> пустой или состоит из пробелов.
    /// </exception>
    public Login(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(value));

        Value = value;
    }
}
