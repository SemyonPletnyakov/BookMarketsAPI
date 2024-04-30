namespace Transport.Models.ForUpdate;

/// <summary>
/// Количество товара на складе.
/// </summary>
public sealed class ProductCountInWarehouse
{
    /// <summary>
    /// Идентификатор товара.
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Идентификатор склада.
    /// </summary>
    public int WarehouseId { get; set; }

    /// <summary>
    /// Количество.
    /// </summary>
    public int Count { get; set; }
}
