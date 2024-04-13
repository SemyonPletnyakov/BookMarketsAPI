namespace Models;

/// <summary>
/// Пароль.
/// </summary>
public sealed record Password
{
    /// <summary>
    /// Значение пароля.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Создаёт объект <see cref="Password"/>
    /// </summary>
    /// <param name="value">
    /// Значение пароля.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="value"/> равен <see cref="null"/>.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="value"/> пустой или состоит из пробелов.
    /// </exception>
    public Password(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(value));

        Value = value;
    }
}
