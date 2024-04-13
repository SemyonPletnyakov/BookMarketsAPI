using Models;

namespace Storage.Models;

/// <summary>
/// Заказ.
/// </summary>
public sealed class Order
{
    /// <summary>
    /// Идентификатор заказа.
    /// </summary>
    public int OrderId { get; set; }

    /// <summary>
    /// Идентификатор покупателя.
    /// </summary>
    public int CustomerId { get; set; }

    /// <summary>
    /// Покупатель.
    /// </summary>
    public Customer Customer { get; set; }

    /// <summary>
    /// Индентификатор магазина.
    /// </summary>
    public int ShopId { get; set; }

    /// <summary>
    /// Магазин, в котором работает сотрудник.
    /// </summary>
    public Shop Shop { get; set; }

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
    public List<ProductsInOrder> ProductsInOrder { get; set; }
}
