namespace Models;

/// <summary>
/// Описание товара.
/// </summary>
public record Description
{
    /// <summary>
    /// Значение описания.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Создаёт объект <see cref="Description"/>
    /// </summary>
    /// <param name="value">
    /// Значение описания.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="value"/> равен <see langword="null"/>.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="value"/> пустой или состоит из пробелов.
    /// </exception>
    public Description(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(value));

        Value = value;
    }
}