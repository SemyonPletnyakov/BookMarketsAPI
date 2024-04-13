namespace Models;

/// <summary>
/// Должность.
/// </summary>
public sealed record JobTitle
{
    /// <summary>
    /// Значение должности.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Создаёт объект <see cref="JobTitle"/>
    /// </summary>
    /// <param name="value">
    /// Значение должности.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="value"/> равен <see cref="null"/>.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="value"/> пустой или состоит из пробелов.
    /// </exception>
    public JobTitle(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(value));

        Value = value;
    }
}
