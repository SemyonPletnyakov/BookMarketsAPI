using Models.FullEntities;

namespace Models.SimpleEntities;

/// <summary>
/// Сущность, показывающая информацию о типе товара в заказе.
/// </summary>
public sealed class ProductInfoInOrder : ProductCount
{
    /// <summary>
    /// Цена товара на момент заказа.
    /// </summary>
    public Price ActualPrice { get; }

    /// <summary>
    /// Создаёт объект <see cref="ProductCount"/>
    /// </summary>
    /// <param name="product">
    /// Товар.
    /// </param>
    /// <param name="count">
    /// Количество товара.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="actualPrice"/> равен <see langword="null"/>.
    /// </exception>
    public ProductInfoInOrder(
        Product product,
        Count count,
        Price actualPrice)
        : base(product, count)
    {
        ActualPrice = actualPrice
            ?? throw new ArgumentNullException(nameof(actualPrice));
    }
}
