
using Models.FullEntities;

namespace Models;

/// <summary>
/// Сущность, показывающая количество товаров.
/// </summary>
public class ProductCount
{
    /// <summary>
    /// Товар.
    /// </summary>
    public Product Product { get; }

    /// <summary>
    /// Количество товара.
    /// </summary>
    public Count Count { get; set; }

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
    /// Если один из парамтеров равен <see cref="null"/>.
    /// </exception>
    public ProductCount(Product product, Count count)
    {
        Product = product ?? throw new ArgumentNullException(nameof(product));
        Count = count ?? throw new ArgumentNullException(nameof(count));
    }
}
