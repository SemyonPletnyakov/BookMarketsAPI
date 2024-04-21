using Models.FullEntities;

namespace Models.Requests;

/// <summary>
/// Запрос на перевод товара в категорию книги.
/// </summary>
public record RequestAddProductInBooks : RequestBase
{
    /// <summary>
    /// Идентификатор продукта.
    /// </summary>
    public Id<Product> ProductId { get; }

    /// <summary>
    /// Создаёт объект <see cref="RequestAddProductInBooks"/>.
    /// </summary>
    /// <param name="productId">
    /// Идентификатор продукта.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="productId"/> равен <see langword="null"/>.
    /// </exception>
    public RequestAddProductInBooks(Id<Product> productId)
    {
        ProductId = productId ?? throw new ArgumentNullException(nameof(productId));
    }
}
