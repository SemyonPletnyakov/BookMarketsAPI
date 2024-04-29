namespace Models.Autorization;

/// <summary>
/// JWT-токен.
/// </summary>
public sealed record JwtToken
{
    /// <summary>
    /// Значение токена.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Создаёт объект <paramref name="value"/>.
    /// </summary>
    /// <param name="value">
    /// Значение токена.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Если <paramref name="value"/> пустой или состоит из пробелов.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="value"/> равен <see langword="null"/>.
    /// </exception>
    public JwtToken(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(value));

        Value = value;
    }
}
