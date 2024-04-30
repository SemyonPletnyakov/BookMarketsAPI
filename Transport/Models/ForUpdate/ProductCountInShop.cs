namespace Transport.Models.ForUpdate;

/// <summary>
/// Количество товара в магазине.
/// </summary>
public sealed class ProductCountInShop
{
    /// <summary>
    /// Идентификатор товара.
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Идентификатор магазин.
    /// </summary>
    public int ShopId { get; set; }

    /// <summary>
    /// Количество.
    /// </summary>
    public int Count { get; set; }
}
