namespace Transport.Models.SimpleModels;

/// <summary>
/// Количество товара.
/// </summary>
public class ProductCount
{
    /// <summary>
    /// Идентификатор товара.
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Количество.
    /// </summary>
    public int Count { get; set; }
}
