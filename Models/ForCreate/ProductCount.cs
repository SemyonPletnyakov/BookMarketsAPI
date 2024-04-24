namespace Models.ForCreate;

/// <summary>
/// Сущность, показывающая количество товаров.
/// </summary>
public class ProductCount
{
    /// <summary>
    /// Идентификатор товара.
    /// </summary>
    public Id<FullEntities.Product> ProductId { get; }

    /// <summary>
    /// Количество товара.
    /// </summary>
    public Count Count { get; set; }

    /// <summary>
    /// Создаёт объект <see cref="ProductCount"/>
    /// </summary>
    /// <param name="productId">
    /// Идентификатор товара.
    /// </param>
    /// <param name="count">
    /// Количество товара.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если один из парамтеров равен <see langword="null"/>.
    /// </exception>
    public ProductCount(Id<FullEntities.Product> productId, Count count)
    {
        ProductId = productId ?? throw new ArgumentNullException(nameof(productId));
        Count = count ?? throw new ArgumentNullException(nameof(count));
    }
}
