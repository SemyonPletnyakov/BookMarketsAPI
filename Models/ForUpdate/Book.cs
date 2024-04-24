namespace Models.ForUpdate;

/// <summary>
/// Книга.
/// </summary>
public sealed class Book : FullEntities.Product
{
    /// <summary>
    /// Идентификатор автора.
    /// </summary>
    public Id<FullEntities.Author>? AuthorId { get; set; }

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
    /// <param name="authorId">
    /// Идентификатор автора.
    /// </param>
    public Book(
        Id<FullEntities.Product> productId,
        Name<FullEntities.Product> name,
        Description? description,
        Price price,
        ISet<string>? keyWords,
        Id<FullEntities.Author>? authorId)
        : base(
            productId,
            name,
            description,
            price,
            keyWords)
    {
        AuthorId = authorId;
    }
}
