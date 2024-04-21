namespace Models;

/// <summary>
/// Телефон.
/// </summary>
public sealed record Phone
{
    /// <summary>
    /// Значение телефона.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Создаёт объект <see cref="Phone"/>
    /// </summary>
    /// <param name="value">
    /// Значение телефона.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="value"/> равен <see langword="null"/>.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="value"/> пустой или состоит из пробелов.
    /// </exception>
    public Phone(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(value));

        Value = value;
    }
}