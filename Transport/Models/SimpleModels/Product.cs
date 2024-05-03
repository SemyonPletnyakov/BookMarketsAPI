namespace Transport.Models.SimpleModels;

/// <summary>
/// Товар.
/// </summary>
public sealed class Product
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Название.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Цена.
    /// </summary>
    public required decimal Price { get; set; }

    /// <summary>
    /// Ключевые слова, применимые к товару.
    /// </summary>
    public required ISet<string>? KeyWords { get; set; }
}
