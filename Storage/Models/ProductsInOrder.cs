namespace Storage.Models;

/// <summary>
/// Сущность, показывающая товары в заказе.
/// </summary>
public sealed class ProductsInOrder
{
    /// <summary>
    /// Идентификатор заказа.
    /// </summary>
    public int OrderId { get; set; }

    /// <summary>
    /// Идентификатор товара.
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Товар.
    /// </summary>
    public Product Product { get; set; }

    /// <summary>
    /// Количество товара в заказе.
    /// </summary>
    public int Count { get; set; }

    /// <summary>
    /// Цена товара на момент заказа.
    /// </summary>
    public decimal ActualPrice { get; set; }
}
