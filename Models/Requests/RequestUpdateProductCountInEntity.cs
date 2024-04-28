using Models.FullEntities;

namespace Models.Requests;

/// <summary>
/// Запрос на обновление числа товаров в сущности.
/// </summary>
/// <typeparam name="T">
/// Тип обновляемой сущности.
/// </typeparam>
public sealed record class RequestUpdateProductCountInEntity<T> : RequestBase
    where T : class
{
    /// <summary>
    /// Идентификатор сущности.
    /// </summary>
    public Id<T> EntityId { get; }

    /// <summary>
    /// Идентификатор товара.
    /// </summary>
    public Id<Product> ProductId { get; }

    /// <summary>
    /// Количества товара.
    /// </summary>
    public Count Count { get; }

    /// <summary>
    /// Создаёт объект <see cref="RequestUpdateProductCountInEntity{T}"/>.
    /// </summary>
    /// <param name="entityId">
    /// Идентификатор сущности.
    /// </param>
    /// <param name="productId">
    /// Идентификатор товара.
    /// </param>
    /// <param name="count">
    /// Количества товара.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если один из параметров равен <see langword="null"/>.
    /// </exception>
    public RequestUpdateProductCountInEntity(
        Id<T> entityId, 
        Id<Product> productId, 
        Count count)
    {
        EntityId = entityId ?? throw new ArgumentNullException(nameof(entityId));
        ProductId = productId ?? throw new ArgumentNullException(nameof(productId));
        Count = count ?? throw new ArgumentNullException(nameof(count));
    }
}
