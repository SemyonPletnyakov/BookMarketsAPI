namespace Storage.Models;

/// <summary>
/// Товар.
/// </summary>
public sealed class Product
{
    /// <summary>
    /// Идентификатор товара.
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Название товара.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Описание товара.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Цена товара.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Ключевые слова, применимые к товару.
    /// </summary>
    public List<string>? KeyWords { get; set; }
}
