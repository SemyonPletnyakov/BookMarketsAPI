namespace Models.FullEntities;

/// <summary>
/// Книга.
/// </summary>
public sealed class Book : Product
{
    /// <summary>
    /// Автор.
    /// </summary>
    public Author? Author { get; set; }

    /// <summary>
    /// Создаёт объект <see cref="Book"/>
    /// </summary>
    /// <param name="productId">
    /// Идентификатор товара.
    /// </param>
    /// <param name="name">
    /// Название товара.
    /// </param>
    /// <param name="description">
    /// Описание.
    /// </param>
    /// <param name="price">
    /// Цена.
    /// </param>
    /// <param name="keyWords">
    /// Ключевые слова, применимые к книге.
    /// </param>
    /// <param name="author">
    /// Автор.
    /// </param>
    public Book(
        Id<Product> productId,
        Name<Product> name,
        Description? description,
        Price price,
        ICollection<string>? keyWords,
        Author author)
        : base(
            productId,
            name,
            description,
            price,
            keyWords)
    {
        Author = author;
    }
}
