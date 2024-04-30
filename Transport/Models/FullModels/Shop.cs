namespace Transport.Models.FullModels;

/// <summary>
/// Магазин.
/// </summary>
public sealed class Shop
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public int ShopId { get; set; }

    /// <summary>
    /// Название.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Время открытия.
    /// </summary>
    public TimeOnly? OpeningTime { get; set; }

    /// <summary>
    /// Время закрытия.
    /// </summary>
    public TimeOnly? ClosingTime { get; set; }

    /// <summary>
    /// Идентификатор адреса.
    /// </summary>
    public required Address Address { get; set; }
}
