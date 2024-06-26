﻿namespace Transport.Models.ForCreate;

/// <summary>
/// Адрес.
/// </summary>
public sealed class Address
{
    /// <summary>
    /// Страна.
    /// </summary>
    public required string Country { get; set; }

    /// <summary>
    /// Номер региона внутри страны.
    /// </summary>
    public int RegionNumber { get; set; }

    /// <summary>
    /// Название региона.
    /// </summary>
    public required string RegionName { get; set; }

    /// <summary>
    /// Город.
    /// </summary>
    public required string City { get; set; }

    /// <summary>
    /// Район.
    /// </summary>
    public string? District { get; set; }

    /// <summary>
    /// Улица.
    /// </summary>
    public required string Street { get; set; }

    /// <summary>
    /// Дом.
    /// </summary>
    public required string House { get; set; }

    /// <summary>
    /// Помещение внутри дома.
    /// </summary>
    public required string Room { get; set; }
}
