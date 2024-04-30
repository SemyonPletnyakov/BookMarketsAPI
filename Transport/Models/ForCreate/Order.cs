namespace Transport.Models.ForCreate;

/// <summary>
/// Заказ.
/// </summary>
public sealed class Order
{
    /// <summary>
    /// Идентификатор покупателя.
    /// </summary>
    public int Customer { get; set; }

    /// <summary>
    /// Идентификатор магазина.
    /// </summary>
    public int ShopId { get; set; }

    /// <summary>
    /// Перечень товаров в заказе.
    /// </summary>
    public required List<SimpleModels.ProductCount> ProductsInOrder { get; set; }
}
