namespace Storage.Models;

/// <summary>
/// Магазин.
/// </summary>
public sealed class Shop
{
    /// <summary>
    /// Идентификатор магазина.
    /// </summary>
    public int ShopId { get; set; }

    /// <summary>
    /// Название магазина.
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
    public int AddressId { get; set; }

    /// <summary>
    /// Адрес.
    /// </summary>
    public Address Address { get; set; }
}
