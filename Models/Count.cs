namespace Models;

/// <summary>
/// Количество.
/// </summary>
public sealed record Count
{
    /// <summary>
    /// Значение количества.
    /// </summary>
    public int Value { get; }

    /// <summary>
    /// Создаёт объект <see cref="Price"/>
    /// </summary>
    /// <param name="value">
    /// Значение количества.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="value"/> меньше нуля.
    /// </exception>
    public Count(int value)
    {
        if (value < 0)
        {
            throw new ArgumentException("Цена не может быть отрицательной.");
        }

        Value = value;
    }
}