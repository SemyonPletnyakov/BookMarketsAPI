using Models;

namespace Transport.Models.ForUpdate;

/// <summary>
/// Заказ.
/// </summary>
public sealed class Order
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public int OrderId { get; set; }

    /// <summary>
    /// Покупатель.
    /// </summary>
    public required Customer Customer { get; set; }

    /// <summary>
    /// Магазин.
    /// </summary>
    public required FullModels.Shop Shop { get; set; }

    /// <summary>
    /// Дата и время оформления заказа.
    /// </summary>
    public DateTimeOffset DateTime { get; set; }

    /// <summary>
    /// Статус заказа.
    /// </summary>
    public OrderStatus OrderStatus { get; set; }

    /// <summary>
    /// Перечень товаров в заказе.
    /// </summary>
    public required List<SimpleModels.ProductInfoInOrder> ProductsInOrder { get; set; }
}
