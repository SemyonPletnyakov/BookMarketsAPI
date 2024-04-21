namespace Models;

/// <summary>
/// Фамилия.
/// </summary>
public record LastName
{
    /// <summary>
    /// Значение фамилии.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Создаёт объект <paramref name="value"/>.
    /// </summary>
    /// <param name="value">
    /// Значение фамилии.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Если <paramref name="value"/> пустой или состоит из пробелов.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="value"/> равен <see langword="null"/>.
    /// </exception>
    public LastName(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(value));

        Value = value;
    }
}
