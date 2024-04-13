namespace Storage.Models;

/// <summary>
/// Склад.
/// </summary>
public sealed class Warehouse
{
    /// <summary>
    /// Идентификатор склада.
    /// </summary>
    public int WarehouseId { get; set; }

    /// <summary>
    /// Название склада.
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
