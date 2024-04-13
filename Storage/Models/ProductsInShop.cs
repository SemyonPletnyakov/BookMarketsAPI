namespace Storage.Models;

/// <summary>
/// Сущность, показывающая количество определённого товара 
/// в определённом магазине.
/// </summary>
public sealed class ProductsInShop
{
    /// <summary>
    /// Индентификатор магазина.
    /// </summary>
    public int ShopId { get; set; }

    /// <summary>
    /// Магазин, в котором располагается товар.
    /// </summary>
    public Shop Shop { get; set; }

    /// <summary>
    /// Идентификатор товара.
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Товар.
    /// </summary>
    public Product Product { get; set; }

    /// <summary>
    /// Количество товара в магазине.
    /// </summary>
    public int Count { get; set; }
}
