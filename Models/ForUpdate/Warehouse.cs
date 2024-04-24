namespace Models.ForUpdate;

/// <summary>
/// Склад.
/// </summary>
public sealed class Warehouse
{
    /// <summary>
    /// Идентификатор склада.
    /// </summary>
    public Id<FullEntities.Warehouse> WarehouseId { get; set; }

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
    /// <param name="warehouseId">
    /// Идентификатор склада.
    /// </param>
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
    /// Если <paramref name="warehouseId"/> или <paramref name="addressId"/> 
    /// равен <see langword="null"/>.
    /// </exception>
    public Warehouse(
        Id<FullEntities.Warehouse> warehouseId,
        Name<FullEntities.Warehouse>? name,
        TimeOnly? openingTime,
        TimeOnly? closingTime,
        Id<FullEntities.Address> addressId)
    {
        WarehouseId = warehouseId
            ?? throw new ArgumentNullException(nameof(warehouseId));

        Name = name;
        OpeningTime = openingTime;
        ClosingTime = closingTime;
        AddressId = addressId ?? throw new ArgumentNullException(nameof(addressId));
    }
}
