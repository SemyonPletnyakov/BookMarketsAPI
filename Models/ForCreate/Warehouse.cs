﻿namespace Models.ForCreate;

/// <summary>
/// Склад.
/// </summary>
public sealed class Warehouse
{
    /// <summary>
    /// Название склада.
    /// </summary>
    public Name<FullEntities.Warehouse>? Name { get; set; }

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
    public Id<FullEntities.Address> AddressId { get; set; }

    /// <summary>
    /// Создаёт объект <see cref="Warehouse"/>.
    /// </summary>
    /// <param name="name">
    /// Название склада.
    /// </param>
    /// <param name="openingTime">
    /// Время открытия.
    /// </param>
    /// <param name="closingTime">
    /// Время закрытия.
    /// </param>
    /// <param name="addressId">
    /// Идентификатор адреса.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="addressId"/> 
    /// равен <see cref="null"/>.
    /// </exception>
    public Warehouse(
        Name<FullEntities.Warehouse>? name,
        TimeOnly? openingTime,
        TimeOnly? closingTime,
        Id<FullEntities.Address> addressId)
    {
        Name = name;
        OpeningTime = openingTime;
        ClosingTime = closingTime;
        AddressId = addressId ?? throw new ArgumentNullException(nameof(addressId));
    }
}