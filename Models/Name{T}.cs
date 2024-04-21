namespace Models;

/// <summary>
/// Название для типа.
/// </summary>
/// <typeparam name="T">
/// Тип, для которого создаётся название.
/// </typeparam>
public sealed record Name<T>
{
    /// <summary>
    /// Значение названия.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Создаёт объект <see cref="Name{T}"/>
    /// </summary>
    /// <param name="value">
    /// Значение названия.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="value"/> равен <see langword="null"/>.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="value"/> пустой или состоит из пробелов.
    /// </exception>
    public Name(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(value));

        Value = value;
    }
}
