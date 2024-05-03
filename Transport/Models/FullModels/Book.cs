namespace Transport.Models.FullModels;

/// <summary>
/// Книга.
/// </summary>
public sealed class Book
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
    /// Описание.
    /// </summary>
    public required string? Description { get; set; }

    /// <summary>
    /// Цена.
    /// </summary>
    public required decimal Price { get; set; }

    /// <summary>
    /// Ключевые слова, применимые к товару.
    /// </summary>
    public required ISet<string>? KeyWords { get; set; }
    
    /// <summary>
    /// Автор.
    /// </summary>
    public FullModels.Author? Author { get; set; }
}
