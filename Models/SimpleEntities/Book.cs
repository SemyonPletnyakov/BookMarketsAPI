namespace Models.SimpleEntities;

/// <summary>
/// Книга.
/// </summary>
public sealed class Book : Product
{
    /// <summary>
    /// Автор.
    /// </summary>
    public FullEntities.Author? Author { get; set; }

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
        Id<FullEntities.Product> productId,
        Name<FullEntities.Product> name,
        Price price,
        ISet<string>? keyWords,
        FullEntities.Author? author)
        : base(
            productId,
            name,
            price,
            keyWords)
    {
        Author = author;
    }
}
