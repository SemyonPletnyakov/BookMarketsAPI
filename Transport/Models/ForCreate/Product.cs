namespace Transport.Models.ForCreate;

/// <summary>
/// Книга.
/// </summary>
public sealed class Product
{
    /// <summary>
    /// Название.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Описание.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Цена.
    /// </summary>
    public required decimal Price { get; set; }

    /// <summary>
    /// Ключевые слова, применимые к товару.
    /// </summary>
    public ISet<string>? KeyWords { get; set; }
}
