namespace Models;

/// <summary>
/// Цена.
/// </summary>
public sealed record Price
{
    /// <summary>
    /// Значение цены.
    /// </summary>
    public decimal Value { get; }

    /// <summary>
    /// Создаёт объект <see cref="Price"/>
    /// </summary>
    /// <param name="value">
    /// Значение цены.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="value"/> меньше нуля.
    /// </exception>
    public Price(decimal value)
    {
        if (value < 0)
        {
            throw new ArgumentException("Цена не может быть отрицательной.");
        }

        Value = value;
    }
}
