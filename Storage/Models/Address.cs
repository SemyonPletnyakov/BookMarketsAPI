namespace Storage.Models;

/// <summary>
/// Адрес.
/// </summary>
public sealed class Address
{
    /// <summary>
    /// Идентификатор адреса.
    /// </summary>
    public int AddressId { get; set; }

    /// <summary>
    /// Страна.
    /// </summary>
    public string Country { get; set; }

    /// <summary>
    /// Номер региона внутри страны.
    /// </summary>
    public int RegionNumber { get; set; }

    /// <summary>
    /// Название региона.
    /// </summary>
    public string RegionName { get; set; }

    /// <summary>
    /// Город.
    /// </summary>
    public string City { get; set; }

    /// <summary>
    /// Район.
    /// </summary>
    public string? District { get; set; }

    /// <summary>
    /// Улица.
    /// </summary>
    public string Street { get; set; }

    /// <summary>
    /// Дом.
    /// </summary>
    public string House { get; set; }

    /// <summary>
    /// Помещение внутри дома.
    /// </summary>
    public string Room { get; set; }
}
