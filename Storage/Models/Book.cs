namespace Storage.Models;

/// <summary>
/// Книга.
/// </summary>
public sealed class Book
{
    /// <summary>
    /// Идентификатор товара.
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Товар, который соотвествует книге. 
    /// </summary>
    public Product Product { get; set; }

    /// <summary>
    /// Идентификатор автора.
    /// </summary>
    public int? AuthorId { get; set; }

    /// <summary>
    /// Автор книги.
    /// </summary>
    public Author? Author { get; set; }
}
