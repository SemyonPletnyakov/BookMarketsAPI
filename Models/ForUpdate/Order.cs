using Models.SimpleEntities;

namespace Models.ForUpdate;

/// <summary>
/// Заказ.
/// </summary>
public sealed class Order
{
    /// <summary>
    /// Идентификатор заказа.
    /// </summary>
    public Id<Order> OrderId { get; set; }

    /// <summary>
    /// Покупатель.
    /// </summary>
    public Customer Customer { get; set; }

    /// <summary>
    /// Магазин, в котором работает сотрудник.
    /// </summary>
    public FullEntities.Shop Shop { get; set; }

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
    public List<ProductInfoInOrder> ProductsInOrder { get; set; }

    /// <summary>
    /// Создаёт объект <see cref="Order"/>.
    /// </summary>
    /// <param name="orderId">
    /// Идентификатор заказа.
    /// </param>
    /// <param name="customer">
    /// Покупатель.
    /// </param>
    /// <param name="shop">
    /// Магазин, в котором работает сотрудник.
    /// </param>
    /// <param name="dateTime">
    /// Дата и время оформления заказа.
    /// </param>
    /// <param name="orderStatus">
    /// Статус заказа.
    /// </param>
    /// <param name="productsInOrder">
    /// Перечень товаров в заказе.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="orderId"/>, <paramref name="customer"/>, 
    /// <paramref name="shop"/>, <paramref name="shop"/> 
    /// или <paramref name="productsInOrder"/> равен <see langword="null"/>.
    /// </exception>
    public Order(
        Id<Order> orderId,
        Customer customer,
        FullEntities.hop shop,
        DateTimeOffset dateTime,
        OrderStatus orderStatus,
        List<ProductInfoInOrder> productsInOrder)
    {
        OrderId = orderId ?? throw new ArgumentNullException(nameof(orderId));
        Customer = customer ?? throw new ArgumentNullException(nameof(customer));
        Shop = shop ?? throw new ArgumentNullException(nameof(shop));
        DateTime = dateTime;
        OrderStatus = orderStatus;

        ProductsInOrder = productsInOrder
            ?? throw new ArgumentNullException(nameof(productsInOrder));
    }
}
