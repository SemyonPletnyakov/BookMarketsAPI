namespace Models.ForCreate;

/// <summary>
/// Заказ.
/// </summary>
public sealed class Order
{
    /// <summary>
    /// Идентификатор покупателя.
    /// </summary>
    public Id<FullEntities.Employee> CustomerId { get; set; }

    /// <summary>
    /// Идентификатор магазина.
    /// </summary>
    public Id<FullEntities.Shop> ShopId { get; set; }

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
    public List<ProductCount> ProductsInOrder { get; set; }

    /// <summary>
    /// Создаёт объект <see cref="Order"/>.
    /// </summary>
    /// <param name="customerId">
    /// Покупатель.
    /// </param>
    /// <param name="shopId">
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
    /// Если <paramref name="customerId"/>, <paramref name="shopId"/>, 
    /// <paramref name="shopId"/> или <paramref name="productsInOrder"/> 
    /// равен <see langword="null"/>.
    /// </exception>
    public Order(
        Id<FullEntities.Employee> customerId,
        Id<FullEntities.Shop> shopId, 
        DateTimeOffset dateTime, 
        OrderStatus orderStatus, 
        List<ProductCount> productsInOrder)
    {
        CustomerId = customerId ?? throw new ArgumentNullException(nameof(customerId));
        ShopId = shopId ?? throw new ArgumentNullException(nameof(shopId));
        DateTime = dateTime;
        OrderStatus = orderStatus;

        ProductsInOrder = productsInOrder 
            ?? throw new ArgumentNullException(nameof(productsInOrder));
    }
}
