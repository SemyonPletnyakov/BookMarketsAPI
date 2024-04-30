namespace Transport.Models.SimpleModels;

/// <summary>
/// Количество товара в заказе.
/// </summary>
public class ProductInfoInOrder
{
    /// <summary>
    /// Идентификатор товара.
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Количество.
    /// </summary>
    public int Count { get; set; }

    /// <summary>
    /// Цена товара на момент заказа.
    /// </summary>
    public decimal ActualPrice { get; }
}