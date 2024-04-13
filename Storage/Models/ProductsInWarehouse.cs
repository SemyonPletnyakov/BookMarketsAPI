namespace Storage.Models;

/// <summary>
/// Сущность, показывающая количество определённого товара 
/// на определённом складе.
/// </summary>
public sealed class ProductsInWarehouse
{
    /// <summary>
    /// Идентификатор склада.
    /// </summary>
    public int WarehouseId { get; set; }

    /// <summary>
    /// Склад, в котором располагается товар.
    /// </summary>
    public Warehouse Warehouse { get; set; }

    /// <summary>
    /// Идентификатор товара.
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Товар.
    /// </summary>
    public Product Product { get; set; }

    /// <summary>
    /// Количество товара на складе.
    /// </summary>
    public int Count { get; set; }
}
