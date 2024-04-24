namespace Models.SimpleEntities;

/// <summary>
/// Товар.
/// </summary>
public class Product
{
    /// <summary>
    /// Идентификатор товара.
    /// </summary>
    public Id<FullEntities.Product> ProductId { get; }

    /// <summary>
    /// Название товара.
    /// </summary>
    public Name<FullEntities.Product> Name { get; set; }

    /// <summary>
    /// Цена.
    /// </summary>
    public Price Price { get; set; }

    /// <summary>
    /// Ключевые слова, применимые к товару.
    /// </summary>
    public ISet<string>? KeyWords { get; set; }

    /// <summary>
    /// Создаёт объект <see cref="Product"/>
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
    /// Ключевые слова, применимые к товару.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="productId"/>, <paramref name="name"/>
    /// или <paramref name="price"/> равен <see langword="null"/>.
    /// </exception>
    public Product(
        Id<FullEntities.Product> productId,
        Name<FullEntities.Product> name,
        Price price,
        ISet<string>? keyWords)
    {
        ProductId = productId ?? throw new ArgumentNullException(nameof(productId));
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Price = price ?? throw new ArgumentNullException(nameof(price));
        KeyWords = keyWords;
    }
}
