using Models.FullEntities;

namespace Models.Requests;

/// <summary>
/// Запрос на перевод книги в категорию обычного товара.
/// </summary>
public record RequestRemoveProductFromBooks : RequestBase
{
    /// <summary>
    /// Идентификатор продукта.
    /// </summary>
    public Id<Product> ProductId { get; }

    /// <summary>
    /// Создаёт объект <see cref="RequestRemoveProductFromBooks"/>.
    /// </summary>
    /// <param name="productId">
    /// Идентификатор продукта.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="productId"/> равен <see langword="null"/>.
    /// </exception>
    public RequestRemoveProductFromBooks(Id<Product> productId)
    {
        ProductId = productId ?? throw new ArgumentNullException(nameof(productId));
    }
}
