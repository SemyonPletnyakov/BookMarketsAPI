namespace Models.SimpleEntities;

/// <summary>
/// Сущность, показывающая количество товаров.
/// </summary>
public class ProductCount
{
    /// <summary>
    /// Идентивикатор товара.
    /// </summary>
    public Id<Product> ProductId { get; }

    /// <summary>
    /// Количество товара.
    /// </summary>
    public Count Count { get; set; }

    /// <summary>
    /// Создаёт объект <see cref="ProductCount"/>
    /// </summary>
    /// <param name="productId">
    /// Товар.
    /// </param>
    /// <param name="count">
    /// Количество товара.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если один из парамтеров равен <see langword="null"/>.
    /// </exception>
    public ProductCount(Id<Product> productId, Count count)
    {
        ProductId = productId ?? throw new ArgumentNullException(nameof(productId));
        Count = count ?? throw new ArgumentNullException(nameof(count));
    }
}
